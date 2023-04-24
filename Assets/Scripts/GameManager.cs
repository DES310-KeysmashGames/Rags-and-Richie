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
    private RichieScript richie;
    [SerializeField] private Image richiePicture;
    [SerializeField] Button richieTextAdvance;
    [SerializeField] private Sprite[] speechBubbles;
    [SerializeField] private Image speechBubbleImage;
    [SerializeField] private Sprite[] emoticons;
    [SerializeField] private Image charEmote;

    //images for the background
    [SerializeField] private Sprite[] backgroundImages;
    [SerializeField] private Image background;
    private string location;
    
    //ui elements
    //ui for the intro and item select
    [Header("UI elements for the intro and Item Select")] 
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private Image customer;
    [SerializeField] private TextMeshProUGUI custName;
    [SerializeField] private TextMeshProUGUI[] itemText;
    [SerializeField] private Button[] itemButtons;
    [SerializeField] private Button TextPrompt;

    //ui for selecting inital price
    [Header("UI elements for selecting the initial price")]
    [SerializeField] private Image bargainometer;
    [SerializeField] private Image dimmer;
    [SerializeField] private TextMeshProUGUI priceBox;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button ReselectItemButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private Button increaseByTen;
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button decreaseByTen;

    //ui for Bargaining phase
    [Header("UI elements for the the bargaining phase")]
    [SerializeField] private Sprite[] patienceMeters;
    [SerializeField] private Button increaseButton2;
    [SerializeField] private Button increaseByTen2;
    [SerializeField] private Button decreaseButton2;
    [SerializeField] private Button decreaseByTen2;
    [SerializeField] private Image priceAdjustment;
    [SerializeField] private Image patienceMeter;
    [SerializeField] private Button makeOfferButton;
    [SerializeField] private TextMeshProUGUI bargainSpeech;
    [SerializeField] private int tolerance;
    [SerializeField] private TextMeshProUGUI previousPriceText;
    [SerializeField] private float previousPrice;
    [SerializeField] private TextMeshProUGUI differenceText;
    [SerializeField] private float priceDifference;

    //UI for ending the game
    [Header("UI elements for ending the game")]
    [SerializeField] private Button endGameButton;
    [SerializeField] private Button nextCustomerButton;
    [SerializeField] private TextMeshProUGUI walletText;

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
    private int sellCount;
    private bool richieAdvanceText;

    //multipliers for specific locations
    private int foodMultiplier = 1;
    private int drinkMultiplier = 1;
    private int warmthMultiplier = 1;
    private int weaponMultiplier = 1;
    private int machineryMultiplier = 1;
    private int luxuryMultiplier = 1;

    //audio 
    public AK.Wwise.Event playerApproachEvent;
    public AK.Wwise.Event playerLeaveEvent;

    private void Awake()
    {
        character = GetComponent<CharacterManager>();
        itemManager = GetComponent<ItemManager>();
        patienceArrow = GetComponent<PatienceMeter>();
        richie = GetComponent<RichieScript>();

        endGameButton.onClick.AddListener(() => {
            //click action
            for (int i = 0; i < itemManager.soldItems.Count ; i++){
                StaticInventory.soldItemsList.Add(itemManager.soldItems[i]);
                Debug.Log("solditems: " + StaticInventory.soldItemsList[i]);
                StaticInventory.basePrice.Add(itemManager.itemPrice[i]);
                StaticInventory.sellPrice.Add(itemManager.sellPrice[i]);
                
            }
            Loader.Load(Loader.Scene.DayEndScene);
        });
        for (int i = 0; i < itemButtons.Length; ++i)
        {
            int index = i;
            itemButtons[index].onClick.AddListener(() => SelectItem(index));
        }
        ReselectItemButton.onClick.AddListener(() => ReselectItems());
        nextCustomerButton.onClick.AddListener(() => NextCustomer());
        confirmButton.onClick.AddListener(() => PriceConfirm());
        makeOfferButton.onClick.AddListener(() => OfferPrice());
        increaseButton.onClick.AddListener(() => IncreasePrice());
        increaseButton2.onClick.AddListener(() => IncreasePrice());
        decreaseButton.onClick.AddListener(() => DecreasePrice());
        decreaseButton2.onClick.AddListener(() => DecreasePrice());
        increaseByTen.onClick.AddListener(() => IncreaseTen());
        increaseByTen2.onClick.AddListener(() => IncreaseTen());
        decreaseByTen.onClick.AddListener(() => DecreaseTen());
        decreaseByTen2.onClick.AddListener(() => DecreaseTen());
        TextPrompt.onClick.AddListener(() => ProgressText());
        richieTextAdvance.onClick.AddListener(() => AdvanceRichieText());
        location = StaticTravel.location;
        switch (location)
        {
            case "ToxicTowers":
                background.sprite = backgroundImages[4];
                break;
            case "Burnington":
                background.sprite = backgroundImages[0];
                break;
            case "Vacancy":
                background.sprite = backgroundImages[5];
                break;
            case "SkyHigh":
                background.sprite = backgroundImages[3];
                break;
            case "BrokenMetro":
                background.sprite = backgroundImages[1];
                break;
            case "LostAngeles":
                background.sprite = backgroundImages[2];
                break;
        }
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
        InitialOfferSetInactive();
        MakeOfferPhaseSetInactive();
        endGameButton.gameObject.SetActive(false);
        TextPrompt.gameObject.SetActive(true);
        nextCustomerButton.gameObject.SetActive(false);
        patienceMeter.enabled = false;
        bargainSpeech.enabled = true;
        charEmote.enabled = false;
        patienceArrow.SetInactive();
        patienceDecrease = 0;
        turnCount = 0;
        customerCount = 1;
        sellCount = 0;
        richieAdvanceText = false;
        switch (StaticTravel.itemOfTheDay)
        {
            case "Food":
                foodMultiplier = 2;
                break;
            case "Drink":
                drinkMultiplier = 2;
                break;
            case "Mechanical":
                machineryMultiplier = 2;
                break;
            case "Warmth":
                warmthMultiplier = 2;
                break;
            case "Weapon":
                weaponMultiplier = 2;
                break;
            case "Luxury":
                luxuryMultiplier = 2;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ResetToMenu();
        walletText.text = PlayerPrefs.GetInt("wallet").ToString();
        //initial intro dialogue loop
        //if (!richieAdvanceText)
        //{
        //    richiePicture.enabled = true;
        //    richieTextAdvance.gameObject.SetActive(true);
        //    speechText.enabled = true;
        //}
        //else
        //{
        //    richieTextAdvance.gameObject.SetActive(false);
        //    richiePicture.enabled = false;
        //    speechText.enabled = false;
        //}
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
                MakeOfferPhaseSetActive();
                textProgression = false;
                TextPrompt.gameObject.SetActive(false);
            }
            priceDifference = previousPrice - setPrice;
            if (priceDifference < 0)
            {
                differenceText.SetText("+" + MathF.Abs(priceDifference).ToString());
            }
            else
            {
                differenceText.SetText("-" + priceDifference.ToString());
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
                bargainSpeech.text = "" + character.GetIntro(introCount);
                //speechText.text = "" + character.GetIntro(introCount);
                introCount = 3;
                textProgression = false;
            }
            else if (introCount == 1 && introCount < introLength)
            {
                bargainSpeech.text = "" + character.GetIntro(introCount);
                //speechText.text = "" + character.GetIntro(introCount);
                introCount = 2;
                textProgression = false;
            }
            else if (introCount == 3 || introCount >= introLength)
            {
                bargainSpeech.text = "" + character.GetTradeSpeech();
                //speechText.text = "" + character.TradeSpeech();
                trade = true;
                introCount = 0;
                textProgression = false;
            }
        }
    }
    //generate a new customer.
    void NewCustomer()
    {
        playerApproachEvent.Post(gameObject);
        richieAdvanceText = false;
        speechText.text = richie.GetCustEnter();
        character.GenerateCustomer();
        customer.sprite = character.GetSprite();
        bargainSpeech.text = "" + character.GetIntro(introCount);
        //speechText.text = "" + character.GetIntro(introCount);
        custName.text = "" + character.GetCustName();
        introCount = 1;
        patience = character.GetPatience();
        custDesperation = character.GetDesperation();
        introLength = character.GetIntroLength();
        speechBubbleImage.sprite = speechBubbles[2];
        patienceMeter.sprite = patienceMeters[0];
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
            TextPrompt.gameObject.SetActive(false);
            trade = false;
            itemsShown = true;
        }
    }

    void CalculatePrice()
    {
        tolerance = Mathf.RoundToInt(custDesperation * (patience / character.GetPatience()));
        basePrice = ((character.GetDrink() * itemManager.GetDrinkValue(selectedItem)) * drinkMultiplier) + ((character.GetFood() * itemManager.GetFoodValue(selectedItem)) * foodMultiplier) + ((character.GetLuxury() + itemManager.GetLuxuryValue(selectedItem)) * luxuryMultiplier)
            + ((character.GetWeapon() * itemManager.GetWeaponValue(selectedItem)) * weaponMultiplier) + ((character.GetWarmth() * itemManager.GetWarmthValue(selectedItem)) * warmthMultiplier) + ((character.GetMachinery() * itemManager.GetMachineryValue(selectedItem)) * machineryMultiplier);
        price = basePrice + tolerance;
        InitialOfferPhaseSetActive();
        priceBox.text = setPrice.ToString("00");
        TextPrompt.gameObject.SetActive(false);
    }

    private void PriceConfirm()
    {
        TextPrompt.gameObject.SetActive(true);
        InitialOfferSetInactive();
        TextPrompt.gameObject.SetActive(true);
        bargainSpeech.enabled = true;
        bargain = true;
        patienceMeter.enabled = true;
        patienceArrow.SetRotation(patience);
        charEmote.enabled = true;
        turnCount = 1;
        if (setPrice < price)
        {
            //bargainSpeech.text = character.GenerateTradeText(1);
            bargainSpeech.text = character.GetHappyText();
            speechBubbleImage.sprite = speechBubbles[1];
            charEmote.sprite = emoticons[1];
        }
        else if( setPrice > price)
        {
            bargainSpeech.text = character.GetAngryText();
            speechBubbleImage.sprite = speechBubbles[0];
            charEmote.sprite = emoticons[0];
            //bargainSpeech.text = character.GenerateTradeText(2);
        }
        else if (setPrice > basePrice)
        {
            bargainSpeech.text = character.GetOkayText();
            speechBubbleImage.sprite = speechBubbles[2];
            charEmote.sprite = emoticons[2];
            //bargainSpeech.text = character.GenerateTradeText(0);
        }
        customer.enabled = true;
        textProgression = false;
        previousPrice = setPrice;
        speechBubbleImage.enabled = true;
    }

    private void OfferPrice()
    {
        TextPrompt.gameObject.SetActive(true);
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
            patienceArrow.SetRotation(patience);
        }
        MakeOfferPhaseSetInactive();     
        previousPrice = setPrice;
        speechBubbleImage.enabled = true;
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
                bargainSpeech.text = character.GetAngryText();
                speechBubbleImage.sprite = speechBubbles[0];
                charEmote.sprite = emoticons[0];
                //bargainSpeech.text = character.GenerateTradeText(2);
            }
            else if (discrepancy > 1.5f)
            {
                patienceDecrease += 15;
                bargainSpeech.text = character.GetAngryText();
                speechBubbleImage.sprite = speechBubbles[0];
                charEmote.sprite = emoticons[0];
                //bargainSpeech.text = character.GenerateTradeText(2);
            }
            else if (discrepancy > 1.0f)
            {
                patienceDecrease += 10;
                bargainSpeech.text = character.GetOkayText();
                speechBubbleImage.sprite = speechBubbles[2];
                charEmote.sprite = emoticons[2];
                //bargainSpeech.text = character.GetTradeSpeech();
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
        int charPatience = character.GetPatience();
        if(patience < ((100/3)*2) && patience > (100/3))
        {
            patienceMeter.sprite = patienceMeters[1];
        }
        else if(patience < (charPatience/3) && patience > 0)
        {
            patienceMeter.sprite = patienceMeters[2];
        }
        else if (patience <= 0)
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

    private void SelectItem(int index)
    {
        for (int i = 0; i < itemButtons.Length; ++i)
        {
            if (i != index)
            {
                itemButtons[i].gameObject.SetActive(false);
                itemText[i].enabled = false;
            }
            else
            {
                itemButtons[i].interactable = false;
            }
        }
        selectedItem = index;
        bargainSpeech.enabled = false;
        speechBubbleImage.enabled = false;
        custName.enabled = false;
        CalculatePrice();
    }

    private void ReselectItems()
    {
        for(int i = 0; i < itemManager.RemainingItems(); ++i)
        {
            itemText[i].enabled = true;
            itemButtons[i].gameObject.SetActive(true);
            itemButtons[i].interactable = true;
        }
        InitialOfferSetInactive();
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
        Debug.Log("accept deal");
        dealOver = true;
        speechBubbleImage.sprite = speechBubbles[1];
        bargainSpeech.text = character.GetAcceptTrade();
        //speechText.enabled = true;
        //speechText.text = "You sold the item!";
        TextPrompt.gameObject.SetActive(false);
        itemManager.SoldItem(selectedItem, basePrice, (int)setPrice);
        character.SaleOver();
        charEmote.enabled = false;
        customer.enabled = false;
        bargain = false;
        int walletValue = PlayerPrefs.GetInt("wallet") + (int)setPrice;
        PlayerPrefs.SetInt("wallet", walletValue);
        TextPrompt.gameObject.SetActive(false);
        if (customerCount < 4)
        {
            nextCustomerButton.gameObject.SetActive(true);
        }
        else
        {
            ResetLevel();
        }
        setPrice = 0;
        ++sellCount;
        playerLeaveEvent.Post(gameObject);
    }

    void DeclineDeal()
    {
        dealOver = true;
        bargainSpeech.text = character.GetDeclineTrade();
        //speechText.enabled = true;
        TextPrompt.gameObject.SetActive(false);
        itemManager.FailedToSell(selectedItem, basePrice, 0);
        //speechText.text = "You failed to sell the item!";
        customer.enabled = false;
        charEmote.enabled = false;
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
        setPrice = 0;
        playerLeaveEvent.Post(gameObject);
    }

    void ResetLevel()
    {
        itemManager.Reset();
        character.Reset();
        sellCount = 0;
        endGameButton.gameObject.SetActive(true);
        foodMultiplier = 1;
        drinkMultiplier = 1;
        warmthMultiplier = 1;
        weaponMultiplier = 1;
        machineryMultiplier = 1;
        luxuryMultiplier = 1;
    }

    public void ProgressText()
    {
        textProgression = true;
    }

    private void NextCustomer()
    {
        Debug.Log("Calling the next customer");
        NewCustomer();
        itemsShown = false;
        trade = false;
        bargain = false;
        textProgression = false;
        customer.enabled = true;
        bargainSpeech.enabled = true;
        //speechText.enabled = true;
        custName.enabled = true;
        patienceDecrease = 0;
        turnCount = 0;
        dealOver = false;
        InitialOfferSetInactive();
        MakeOfferPhaseSetInactive();
        endGameButton.gameObject.SetActive(false);
        nextCustomerButton.gameObject.SetActive(false);
        TextPrompt.gameObject.SetActive(true);
        patienceMeter.enabled = false;
        //bargainSpeech.enabled = false;
        patienceArrow.SetInactive();
        customerCount += 1;
        itemButtons[selectedItem].interactable = true;
        itemButtons[selectedItem].gameObject.SetActive(false);
        itemText[selectedItem].enabled = false;
        switch (sellCount)
        {
            case 1:
                itemButtons[3].gameObject.SetActive(false);
                break;
            case 2:
                itemButtons[2].gameObject.SetActive(false);
                break;
            case 3:
                itemButtons[1].gameObject.SetActive(false);
                break;
        }
    }

    void MakeOfferPhaseSetActive()
    {
        makeOfferButton.gameObject.SetActive(true);
        increaseButton2.gameObject.SetActive(true);
        increaseByTen2.gameObject.SetActive(true);
        decreaseButton2.gameObject.SetActive(true);
        decreaseByTen2.gameObject.SetActive(true);
        priceAdjustment.enabled = true;
        previousPriceText.gameObject.SetActive(true);
        differenceText.gameObject.SetActive(true);
        previousPriceText.SetText(previousPrice.ToString());
    }

    void MakeOfferPhaseSetInactive()
    {
        makeOfferButton.gameObject.SetActive(false);
        increaseButton2.gameObject.SetActive(false);
        increaseByTen2.gameObject.SetActive(false);
        decreaseButton2.gameObject.SetActive(false);
        decreaseByTen2.gameObject.SetActive(false);
        priceAdjustment.enabled = false;
        previousPriceText.gameObject.SetActive(false);
        differenceText.gameObject.SetActive(false);
        bargainSpeech.enabled = true;
    }

    void InitialOfferPhaseSetActive()
    {
        bargainometer.enabled = true;
        dimmer.enabled = true;
        priceBox.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
        increaseButton.gameObject.SetActive(true);
        increaseByTen.gameObject.SetActive(true);
        decreaseButton.gameObject.SetActive(true);
        decreaseByTen.gameObject.SetActive(true);
        ReselectItemButton.gameObject.SetActive(true);
    }

    void InitialOfferSetInactive()
    {
        bargainometer.enabled = false;
        dimmer.enabled = false;
        priceBox.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        increaseButton.gameObject.SetActive(false);
        increaseByTen.gameObject.SetActive(false);
        decreaseButton.gameObject.SetActive(false);
        decreaseByTen.gameObject.SetActive(false);
        ReselectItemButton.gameObject.SetActive(false);
    }

   void ResetToMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Loader.Load(Loader.Scene.MainMenuScene);
            ResetLevel();
        }
    }

    void AdvanceRichieText()
    {
        richieAdvanceText = true; 
    }
}
