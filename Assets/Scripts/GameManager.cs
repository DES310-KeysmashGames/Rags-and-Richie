using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using UnityEditor.UIElements;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    //managers for item and character
    private ItemManager itemManager;
    private CharacterManager character;

    //ui elements
    //ui for the intro and item select
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private SpriteRenderer customer;
    [SerializeField] private SpriteRenderer[] items;
    [SerializeField] private TextMeshProUGUI[] itemText;
    [SerializeField] private Button[] itemButtons;

    //ui for selecting inital price
    [SerializeField] private Image bargainometer;
    [SerializeField] private Slider priceSlider;
    [SerializeField] private TextMeshProUGUI priceBox;
    [SerializeField] private Button confirmButton;

    //ui for Bargaining phase
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Image patienceMeter;

    private float timer;
    private bool trade;
    [SerializeField] private int price;
    [SerializeField] private int setPrice;
    [SerializeField] private int patience;
    [SerializeField] private int patienceDecrease;
    private int selectedItem;
    private bool itemsShown;
    private bool bargain;
    //private int itemNo;
  

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
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].enabled = false;
            items[i].sprite = itemManager.GetSprite(i);
            itemText[i].text = itemManager.GetName(i);
            itemText[i].enabled = false;
            itemButtons[i].gameObject.SetActive(false);
        }
        trade = false;
        bargain = false;
        timer = 5.0f;
        bargainometer.enabled = false;
        priceSlider.gameObject.SetActive(false);
        priceBox.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        patienceMeter.enabled = false;
        patienceDecrease = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //initial intro dialogue loop
        if (!itemsShown)
        {
            InitialTrade();
        }
        priceBox.text = Mathf.Round(priceSlider.value).ToString();
        if (bargain)
        {
            
        }
    }

    void CalculatePrice()
    {
        price = (character.GetDrink() * itemManager.GetDrinkValue(selectedItem)) + (character.GetFood() * itemManager.GetFoodValue(selectedItem)) + (character.GetLuxury() + itemManager.GetLuxuryValue(selectedItem))
            + (character.GetWeapon() * itemManager.GetWeaponValue(selectedItem)) + (character.GetWarmth() * itemManager.GetWarmthValue(selectedItem)) + (character.GetMachinery() * itemManager.GetMachineryValue(selectedItem));
        bargainometer.enabled = true;
        priceSlider.gameObject.SetActive(true);
        priceSlider.minValue = (float)(price * 0.7);
        priceSlider.maxValue = (float)(price * 1.3);
        priceSlider.value = (float)(price);
        priceBox.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }

    public void PriceConfirm()
    {
        setPrice = (int)(priceSlider.normalizedValue * price);
        confirmButton.gameObject.SetActive(false);
        priceSlider.gameObject.SetActive(false);
        priceBox.gameObject.SetActive(false);
        bargainometer.enabled = false;
        increaseButton.gameObject.SetActive(true);
        decreaseButton.gameObject.SetActive(true);
        bargain = true;
        patienceMeter.enabled = true;
    }

    public void IncreasePrice()
    {
        setPrice += 1;
        patienceMeter.fillAmount = (float)patience / character.GetPatience();
    }

    public void DecreasePrice()
    {
        setPrice -= 1;
        patienceMeter.fillAmount = (float)patience / character.GetPatience();
    }

    public void OfferPrice()
    {
        patience -= patienceDecrease;
        patienceDecrease += 10;
        patienceMeter.fillAmount = (float)patience / character.GetPatience();
    }

    void InitialTrade()
    {
        timer -= Time.deltaTime;
        TradeSpeech(timer);
        //transitions to selling the stores wares.
        if (timer <= 0.0f && trade == true)
        {
            ItemsForSale();
        }
    }

    void TradeSpeech(float timer)
    {
        if (timer <= 0.0f && trade == false)
        {
            speechText.text = "" + character.TradeSpeech();
            timer = 2.0f;
            trade = true;
        }
    }
    //generate a new customer.
    void NewCustomer()
    {
        character.GenerateCustomer();
        customer.sprite = character.GetSprite();
        speechText.text = "" + character.GetIntro(0);
        patience = character.GetPatience();
        timer = 5.0f;
    }

    //displays the items available for sale.
    void ItemsForSale()
    {
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].enabled = true;
            items[i].sprite = itemManager.GetSprite(i);
            itemText[i].enabled = true;
            itemButtons[i].gameObject.SetActive(true);
            trade = false;
            itemsShown = true;
        }
    }

    //disables showing the items.
    public void DisableAllItems()
    {
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].enabled = false;
        }
    }

    public void SelectItem1()
    {
        items[1].enabled = false;
        items[2].enabled = false;
        items[3].enabled = false;
        itemText[1].enabled = false;
        itemText[2].enabled = false;
        itemText[3].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].gameObject.SetActive(false);
        selectedItem = 0;
        //bargain = true;
        CalculatePrice();
    }

    public void SelectItem2()
    {
        items[0].enabled = false;
        items[2].enabled = false;
        items[3].enabled = false;
        itemText[0].enabled = false;
        itemText[2].enabled = false;
        itemText[3].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].gameObject.SetActive(false);
        selectedItem = 1;
        //bargain = true;
        CalculatePrice();
    }

    public void SelectItem3()
    {
        items[0].enabled = false;
        items[1].enabled = false;
        items[3].enabled = false;
        itemText[0].enabled = false;
        itemText[1].enabled = false;
        itemText[3].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].gameObject.SetActive(false);
        selectedItem = 2;
        //bargain = true;
        CalculatePrice();
    }

    public void SelectItem4()
    {
        items[0].enabled = false;
        items[1].enabled = false;
        items[2].enabled = false;
        itemText[0].enabled = false;
        itemText[1].enabled = false;
        itemText[2].enabled = false;
        itemButtons[0].gameObject.SetActive(false);
        itemButtons[1].gameObject.SetActive(false);
        itemButtons[2].gameObject.SetActive(false);
        itemButtons[3].gameObject.SetActive(false);
        selectedItem = 3;
       // bargain = true;
        CalculatePrice();
    }
}
