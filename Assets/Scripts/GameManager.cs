using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //managers for item and character
    private ItemManager itemManager;
    private CharacterManager character;
    private PatienceMeter patienceArrow;

    //ui elements
    //ui for the intro and item select
    [Header("UI elements for the intro and Item Select")] 
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private Image customer;
    [SerializeField] private TextMeshProUGUI[] itemText;
    [SerializeField] private Button[] itemButtons;
    [SerializeField] private Button TextPrompt;

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

    //UI for ending the game
    [Header("UI elements for ending the game")]
    [SerializeField] private Button endGameButton;
    [SerializeField] private Button nextCustomerButton;

    //miscellaneous values
    [Header("Private game attributes")]
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
    private bool textProgression;
    private bool dealOver;
    [SerializeField] private int customerCount;

    private void Awake()
    {
        character = GetComponent<CharacterManager>();
        itemManager = GetComponent<ItemManager>();
        patienceArrow = GetComponent<PatienceMeter>();

        endGameButton.onClick.AddListener(() => {
            //click action
            Loader.Load(Loader.Scene.EndingScene);
        });
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
        textProgression = false;
        dealOver = false;
        bargainometer.enabled = false;
        priceBox.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
        confirmButton2.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(false);
        nextCustomerButton.gameObject.SetActive(false);
        patienceMeter.enabled = false;
        bargainSpeech.enabled = false;
        patienceArrow.setInactive();
        patienceDecrease = 0;
        turnCount = 0;
        customerCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //initial intro dialogue loop
        if (!itemsShown)
        {
            InitialTrade();
        }
        priceBox.text = setPrice.ToString("00");
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
            if (textProgression)
            {
                bargainometer.enabled = true;
                priceBox.gameObject.SetActive(true);
                priceBox.text = setPrice.ToString("00");
                increaseButton.gameObject.SetActive(true);
                decreaseButton.gameObject.SetActive(true);
                increaseByTen.gameObject.SetActive(true);
                decreaseByTen.gameObject.SetActive(true);
                confirmButton2.gameObject.SetActive(true);
                bargainSpeech.enabled = false;
                customer.enabled = false;
                textProgression = false;
            }
        }
    }

    void InitialTrade()
    {
        TradeSpeech();
        //transitions to selling the stores wares.
        if (trade == true)
        {
            ItemsForSale();
        }
    }

    void TradeSpeech()
    {
        if (textProgression)
        {
            if (introCount == 2 && introCount < introLength)
            {
                speechText.text = "" + character.GetIntro(introCount);
                introCount = 3;
                textProgression = false;
            }
            else if (introCount == 1 && introCount < introLength)
            {
                speechText.text = "" + character.GetIntro(introCount);
                introCount = 2;
                textProgression = false;
            }
            else if (introCount == 3 || introCount >= introLength)
            {
                speechText.text = "" + character.TradeSpeech();
                trade = true;
                introCount = 0;
                textProgression = false;
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
        introLength = character.GetIntroLength();
    }

    //displays the items available for sale.
    void ItemsForSale()
    {
        for (int i = 0; i < itemManager.RemainingItems(); ++i)
        {
            itemButtons[i].image.sprite = itemManager.GetSprite(i);
            itemText[i].text = itemManager.GetName(i);
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
        priceBox.text = setPrice.ToString("00");
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
        patienceArrow.SetRotation(patience, character.GetPatience());
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
        customer.enabled = true;
        textProgression = false;
    }

    public void OfferPrice()
    {
        bargainSpeech.enabled = true;
        customer.enabled = true;
        turnCount++;
        patienceDecrease += 5;
        PriceCheck();
        patience -= patienceDecrease;
        if(patience < 0)
        {
            patience = 0;
        }
        if (!dealOver)
        {
            Desperation();
            ReCalculate();
            patienceArrow.SetRotation(patience, character.GetPatience());
        }
        bargainometer.enabled = false;
        priceBox.gameObject.SetActive(false);
        confirmButton2.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
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
        tolerance = Mathf.RoundToInt(custDesperation * value);
        price = basePrice + tolerance;
    }

    void PatienceCheck()
    {
        if (patience <= 0)
        {
            DeclineDeal();
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
        dealOver = true;
        bargainSpeech.text = character.AcceptDeal(0);
        speechText.enabled = true;
        speechText.text = "You sold the item!";
        itemManager.SoldItem(selectedItem);
        character.SaleOver();
        customer.enabled = false;
        bargain = false;
        int wallet = PlayerPrefs.GetInt("wallet") + (int)setPrice;
        PlayerPrefs.SetInt("wallet", wallet);
        TextPrompt.gameObject.SetActive(false);
        if (customerCount < 4)
        {
            Debug.Log("It gets here");
            nextCustomerButton.gameObject.SetActive(true);
        }
        else
        {
            ResetLevel();
        }
    }

    void DeclineDeal()
    {
        dealOver = true;
        bargainSpeech.text = character.DeclineDeal(0);
        speechText.enabled = true;
        speechText.text = "You failed to sell the item!";
        customer.enabled = false;
        character.SaleOver();
        bargain = false;
        TextPrompt.gameObject.SetActive(false);
        if (customerCount < 4)
        {
            nextCustomerButton.gameObject.SetActive(true);
        }
        else
        {
            ResetLevel();
        }
    }

    void ResetLevel()
    {
        itemManager.Reset();
        character.Reset();
        endGameButton.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        Loader.Load(Loader.Scene.EndingScene);
    }

    public void ProgressText()
    {
        textProgression = true;
    }

    public void NextCustomer()
    {
        NewCustomer();
        itemsShown = false;
        trade = false;
        bargain = false;
        textProgression = false;
        bargainometer.enabled = false;
        customer.enabled = true;
        speechText.enabled = true;
        priceBox.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
        confirmButton2.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(false);
        nextCustomerButton.gameObject.SetActive(false);
        TextPrompt.gameObject.SetActive(true);
        patienceMeter.enabled = false;
        bargainSpeech.enabled = false;
        patienceArrow.setInactive();
        customerCount += 1;
        itemButtons[selectedItem].interactable = true;
        itemButtons[selectedItem].gameObject.SetActive(false);
        itemText[selectedItem].enabled = false;
        switch (customerCount)
        {
            case 2:
                itemButtons[3].gameObject.SetActive(false);
                break;
            case 3:
                itemButtons[2].gameObject.SetActive(false);
                break;
            case 4:
                itemButtons[1].gameObject.SetActive(false);
                break;

        }
    }
}
