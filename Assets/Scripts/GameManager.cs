using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEditor.UIElements;
using Unity.VisualScripting;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    //managers for item and character
    private ItemManager itemManager;
    private CharacterManager character;

    //ui elements
    //ui for the intro and item select
    [Header("UI elements for the intro and Item Select")] 
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private Image customer;
    [SerializeField] private TextMeshProUGUI[] itemText;
    [SerializeField] private Button[] itemButtons;

    //ui for selecting inital price
    [Header("UI elements for selecting the initial price")]
    [SerializeField] private Image bargainometer;
    [SerializeField] private TextMeshProUGUI priceBox;
    [SerializeField] private Button confirmButton;

    //ui for Bargaining phase
    [Header("UI elements for the the bargaining phase")]
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button increaseByTen;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button decreaseByTen;
    [SerializeField] private Image patienceMeter;
    [SerializeField] private Button confirmButton2;
    [SerializeField] private TextMeshProUGUI bargainSpeech;
    [SerializeField] private int tolerance;

    private float timer;
    private bool trade;
    [SerializeField] private int price;
    [SerializeField] private int basePrice;
    [SerializeField] private float setPrice;
    [SerializeField] private float patience;
    [SerializeField] private int patienceDecrease;
    [SerializeField] private int custDesperation;
    [SerializeField] private int turnCount;
    private int selectedItem;
    private bool itemsShown;
    private bool bargain;
    private int introLength;
    private int introCount;

    private void Awake()
    {
        character = GetComponent<CharacterManager>();
        itemManager = GetComponent<ItemManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        NewCustomer();
        itemManager.GenerateItemList();
        for (int i = 0; i < itemButtons.Length; ++i)
        {
            itemText[i].text = itemManager.GetName(i);
            itemText[i].enabled = false;
            itemButtons[i].gameObject.SetActive(false);
        }
        trade = false;
        bargain = false;
        timer = 5.0f;
        bargainometer.enabled = false;
        priceBox.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
        confirmButton2.gameObject.SetActive(false);
        patienceMeter.enabled = false;
        bargainSpeech.enabled = false;
        patienceDecrease = 0;
        turnCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //initial intro dialogue loop
        if (!itemsShown)
        {
            InitialTrade(timer);
        }
        priceBox.text = setPrice.ToString();
        if(setPrice > 99)
        {
            setPrice = 99;
        }
        if (setPrice < 0)
        {
            setPrice = 0;
        }
        if (bargain)
        {
            PatienceCheck();
            if (timer <= 0.0f)
            {
                bargainometer.enabled = true;
                priceBox.gameObject.SetActive(true);
                priceBox.text = setPrice.ToString();
                increaseButton.gameObject.SetActive(true);
                decreaseButton.gameObject.SetActive(true);
                increaseByTen.gameObject.SetActive(true);
                decreaseByTen.gameObject.SetActive(true);
                confirmButton2.gameObject.SetActive(true);
                bargainSpeech.enabled = false;
            }
        }
    }

    void InitialTrade(float time)
    {
        time -= Time.deltaTime;
        TradeSpeech(time);
        //transitions to selling the stores wares.
        if (timer <= 0.0f && trade == true)
        {
            ItemsForSale();
        }
    }

    void TradeSpeech(float time)
    {
        if (time <= 0.0f)
        {
            if (introCount == 2 && introCount < introLength)
            {
                speechText.text = "" + character.GetIntro(introCount);
                timer = 3.0f;
                introCount = 3;
            }
            else if (introCount == 1 && introCount < introLength)
            {
                speechText.text = "" + character.GetIntro(introCount);
                timer = 3.0f;
                introCount = 2;
            }
            else if (introCount == 3 || introCount >= introLength)
            {
                speechText.text = "" + character.TradeSpeech();
                timer = 3.0f;
                trade = true;
                introCount = 0;
            }

        }
    }
    //generate a new customer.
    void NewCustomer()
    {
        character.GenerateCustomer();
        customer.sprite = character.GetSprite();
        speechText.text = "" + character.GetIntro(introCount);
        introCount = 1;
        patience = character.GetPatience();
        custDesperation = character.GetDesperation();
        timer = 5.0f;
        introLength = character.GetIntroLength();
    }

    //displays the items available for sale.
    void ItemsForSale()
    {
        for (int i = 0; i < itemButtons.Length; ++i)
        {
            itemButtons[i].image.sprite = itemManager.GetSprite(i);
            itemText[i].enabled = true;
            itemButtons[i].gameObject.SetActive(true);
            trade = false;
            itemsShown = true;
        }
    }

    void CalculatePrice()
    {
        tolerance = Mathf.RoundToInt(custDesperation * (patience / character.GetPatience()));
        basePrice = (character.GetDrink() * itemManager.GetDrinkValue(selectedItem)) + (character.GetFood() * itemManager.GetFoodValue(selectedItem)) + (character.GetLuxury() + itemManager.GetLuxuryValue(selectedItem))
            + (character.GetWeapon() * itemManager.GetWeaponValue(selectedItem)) + (character.GetWarmth() * itemManager.GetWarmthValue(selectedItem)) + (character.GetMachinery() * itemManager.GetMachineryValue(selectedItem));
        price = basePrice + tolerance;
        bargainometer.enabled = true;
        priceBox.gameObject.SetActive(true);
        priceBox.text = setPrice.ToString();
        increaseButton.gameObject.SetActive(true);
        decreaseButton.gameObject.SetActive(true);
        increaseByTen.gameObject.SetActive(true);
        decreaseByTen.gameObject.SetActive(true);
        priceBox.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }

    public void PriceConfirm()
    {
        confirmButton.gameObject.SetActive(false);
        priceBox.gameObject.SetActive(false);
        confirmButton2.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
        bargainSpeech.enabled = true;
        bargainometer.enabled = false;
        bargain = true;
        patienceMeter.enabled = true;
        timer = 5.0f;
        turnCount = 1;
        if (setPrice < price)
        {
            bargainSpeech.text = character.GenerateTradeText(1);
        }
        else if( setPrice > price)
        {
            bargainSpeech.text = character.GenerateTradeText(2);
        }
        else if (setPrice > basePrice)
        {
            bargainSpeech.text = character.GenerateTradeText(0);
        }
        setPrice = 0;
    }

    public void OfferPrice()
    {
        bargainSpeech.enabled = true;
        turnCount++;
        patienceDecrease += 5;
        PriceCheck();
        patience -= patienceDecrease;
        Desperation();
        ReCalculate();
        patienceMeter.fillAmount = (float)patience / character.GetPatience();
        bargainometer.enabled = false;
        priceBox.gameObject.SetActive(false);
        confirmButton2.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
        timer = 5.0f;
    }

    void PriceCheck()
    {
        float discrepancy = (setPrice / basePrice);
        if (turnCount < 5)
        {
            if (setPrice <= price)
            {
                AcceptDeal();
            }
            else if (discrepancy > 2.0f)
            {
                patienceDecrease += 20;
                bargainSpeech.text = character.GenerateTradeText(2);
            }
            else if (discrepancy > 1.5f)
            {
                patienceDecrease += 15;
                bargainSpeech.text = character.GenerateTradeText(2);
            }
            else if (discrepancy > 1.0f)
            {
                patienceDecrease += 10;
                bargainSpeech.text = character.GenerateTradeText(0);
            }
            else if (discrepancy <= 1.0f)
            {
                AcceptDeal();
            }
        }
        if (turnCount == 5)
        {
            if (discrepancy <= 1.0f)
            {
                AcceptDeal();
            }
            else
            {
                DeclineDeal();
            }
        }
    }

    void ReCalculate()
    {
        float value = (patience / character.GetPatience());
        Debug.Log("" + value);
        tolerance = Mathf.RoundToInt(custDesperation * value);
        price = basePrice + tolerance;
    }

    void PatienceCheck()
    {
        if (patience <= 0)
        {
            bargainSpeech.text = character.NoPatience(0);
            speechText.enabled = true;
            speechText.text = "You failed to sell the item!";
            customer.enabled = false;
            bargain = false;
        }
    }

    void Desperation()
    {
        switch (turnCount)
        {
            case 1:
                custDesperation += 1;
                break;
            case 2:
                custDesperation += 5;
                break;
            case 3:
                custDesperation += 10;
                break;
            case 4:
                custDesperation += 15;
                break;
            case 5:
                custDesperation += 20;
                break;
            case 6:
                DeclineDeal();
                break;
        };
    }

    public void SelectItem1()
    {
        itemText[1].enabled = false;
        itemText[2].enabled = false;
        itemText[3].enabled = false;
        itemButtons[0].interactable = false;
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].gameObject.SetActive(false);
        speechText.enabled = false;
        selectedItem = 0;
        CalculatePrice();
    }

    public void SelectItem2()
    {
        itemText[0].enabled = false;
        itemText[2].enabled = false;
        itemText[3].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].interactable = false;
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].gameObject.SetActive(false);
        selectedItem = 1;
        speechText.enabled = false;
        CalculatePrice();
    }

    public void SelectItem3()
    {
        itemText[0].enabled = false;
        itemText[1].enabled = false;
        itemText[3].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].interactable = false;
        itemButtons[3].gameObject.SetActive(false);
        selectedItem = 2;
        speechText.enabled = false;
        CalculatePrice();
    }

    public void SelectItem4()
    {
        itemText[0].enabled = false;
        itemText[1].enabled = false;
        itemText[2].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].interactable = false;
        speechText.enabled = false;
        selectedItem = 3;
        CalculatePrice();
    }

    public void IncreasePrice()
    {
        setPrice += 1;
    }

    public void DecreasePrice()
    {
        setPrice -= 1;
    }

    public void IncreaseTen()
    {
        setPrice += 10;
    }

    public void DecreaseTen()
    {
        setPrice -= 10;
    }

    void AcceptDeal()
    {
        bargainSpeech.text = character.AcceptDeal(0);
        speechText.enabled = true;
        speechText.text = "You sold the item!";
        itemManager.SoldItem(selectedItem);
        customer.enabled = false;
        bargain = false;
    }

    void DeclineDeal()
    {
        bargainSpeech.text = character.DeclineDeal(0);
        speechText.enabled = true;
        speechText.text = "You failed to sell the item!";
        customer.enabled = false;
        bargain = false;
    }
}
