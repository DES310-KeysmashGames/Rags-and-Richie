using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //managers for item and character
    private ItemManager itemManager;
    private CharacterManager character;
    private TypeWriterTextScript typewriter;

    // Animations
    [SerializeField] AnimationTrade initialPrice;
    [SerializeField] AnimationTrade PlayerUserInterface;
    [SerializeField] AnimationTrade customerAnimations;
    [SerializeField] AnimationTrade speechBubble;
    [SerializeField] AnimationTrade blinkingMoney;
    [SerializeField] AnimationTrade blinkingEmoticon;
    [SerializeField] AnimationTrade shelfLock;
    [SerializeField] AnimationTrade openingDayStart;
    [SerializeField] AnimationTrade closingDayEnd;
    [SerializeField] AnimationTrade richieReacting;
    [SerializeField] AnimationTrade itemHovers;




    [SerializeField] private Sprite[] speechBubbles;
    [SerializeField] private Image speechBubbleImage;
    [SerializeField] private Sprite[] emoticons;     
    [SerializeField] private Image charEmote;
    private string itemOfTheDay;

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
    [SerializeField] private Image itemCard;
    [SerializeField] private Button itemReshuffleButton;
    [SerializeField] private int shuffleCount;
    [SerializeField] private TextMeshProUGUI reshuffleText;

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
    [SerializeField] private Button increaseButton2;
    [SerializeField] private Button increaseByTen2;
    [SerializeField] private Button decreaseButton2;
    [SerializeField] private Button decreaseByTen2;
    [SerializeField] private Image priceAdjustment;
    [SerializeField] private Button makeOfferButton;
    [SerializeField] private TextMeshProUGUI bargainSpeech;
    private int tolerance;
    [SerializeField] private TextMeshProUGUI previousPriceText;
    private float previousPrice;
    [SerializeField] private TextMeshProUGUI differenceText;
    private float priceDifference;
    [SerializeField] private int dupecount;
    private float initialOffer;
    private float followUpOffer;
    [SerializeField] private bool offerAccept;
    [SerializeField] private int wallet;
    [SerializeField] private int tipBonus;
    [SerializeField] private int dupeBonus;

    //ui elements for turn count
    [SerializeField] private TextMeshProUGUI turnsRemainingText;

    //UI for ending the game
    [Header("UI elements for ending the game")]
    [SerializeField] private Button endGameButton;
    [SerializeField] private Button nextCustomerButton;
    [SerializeField] private TextMeshProUGUI walletText;

    //miscellaneous values
    [Header("Private game attributes")]
    private bool trade;
    private int price;
    [SerializeField] private int basePrice;
    private float setPrice;
    [SerializeField] private float patience;
    private int turnCount;
    private int selectedItem;
    private bool itemsShown;
    private bool bargain;
    private int introLength;
    private int tutorialLength;
    private int introCount;
    private bool textProgression;
    private bool dealOver;
    [SerializeField] private int customerCount;
    private int sellCount;
    private float textTimer;
    private bool ending;
    private float endingTimer;
    private bool wooshBool;
    private bool endingBool;
    private int emotionTracker;
    private int day;

    //audio 
    public AK.Wwise.Event playerApproachEvent;
    public AK.Wwise.Event playerLeaveEvent;
    public AK.Wwise.Event priceUpEvent;
    public AK.Wwise.Event priceDownEvent;
    public AK.Wwise.Event custEnragedEvent;
    public AK.Wwise.Event custAngryEvent;
    public AK.Wwise.Event custSweatEvent;
    public AK.Wwise.Event custDiamondEvent;
    public AK.Wwise.Event custSurprisedEvent;
    public AK.Wwise.Event custAstonishedEvent;
    public AK.Wwise.Event custBaffledEvent;
    public AK.Wwise.Event custDialogueEvent;
    public AK.Wwise.Event buttonPressEvent;
    public AK.Wwise.Event saleSuccess;
    public AK.Wwise.Event saleFailure;
    public AK.Wwise.Event wooshingUIevent;
    public AK.Wwise.Event reshuffleEvent;

    private void Awake()
    {
        character = GetComponent<CharacterManager>();
        itemManager = GetComponent<ItemManager>();
        typewriter = GetComponent<TypeWriterTextScript>();

        endGameButton.onClick.AddListener(() => {
            //click action
            for (int i = 0; i < itemManager.soldItems.Count ; i++){
                StaticInventory.soldItemsList.Add(itemManager.soldItems[i]);
                Debug.Log("solditems: " + StaticInventory.soldItemsList[i]);
                StaticInventory.basePrice.Add(itemManager.itemPrice[i]);
                StaticInventory.sellPrice.Add(itemManager.sellPrice[i]); 
                buttonPressEvent.Post(gameObject);
            }
            itemManager.Reset();
            character.Reset();
            Loader.Load(Loader.Scene.DayEndScene);
        });
        for (int i = 0; i < itemButtons.Length; ++i)
        {
            int index = i;
            itemButtons[index].onClick.AddListener(() => SelectItem(index));
        }
        ReselectItemButton.onClick.AddListener(() =>
        {
            ReselectItems();
            buttonPressEvent.Post(gameObject);
        });
        itemReshuffleButton.onClick.AddListener(() =>
        {
            ItemReshuffle();
        });
        nextCustomerButton.onClick.AddListener(() =>
        {
            NextCustomer();
            buttonPressEvent.Post(gameObject);
        });
        confirmButton.onClick.AddListener(() =>
        {
            PriceConfirmAsync();
            Desperation();
            buttonPressEvent.Post(gameObject);
        });
        makeOfferButton.onClick.AddListener(() =>
        {
            OfferPrice();
            buttonPressEvent.Post(gameObject);
        });
        increaseButton.onClick.AddListener(() =>
        {
            IncreasePrice();
            priceUpEvent.Post(gameObject);
        });
        increaseButton2.onClick.AddListener(() =>
        {
            IncreasePrice();
            priceUpEvent.Post(gameObject);
        });
        decreaseButton.onClick.AddListener(() =>
        {
            DecreasePrice();
            priceDownEvent.Post(gameObject);
        });
        decreaseButton2.onClick.AddListener(() =>
        {
            DecreasePrice();
            priceDownEvent.Post(gameObject);
        });
        increaseByTen.onClick.AddListener(() =>
        {
            IncreaseTen();
            priceUpEvent.Post(gameObject);
        });
        increaseByTen2.onClick.AddListener(() =>
        {
            IncreaseTen();
            priceUpEvent.Post(gameObject);
        });
        decreaseByTen.onClick.AddListener(() =>
        {
            DecreaseTen();
            priceDownEvent.Post(gameObject);
        });
        decreaseByTen2.onClick.AddListener(() =>
        {
            DecreaseTen();
            priceDownEvent.Post(gameObject);
        });
        TextPrompt.onClick.AddListener(() => ProgressText());
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
        wallet = PlayerPrefs.GetInt("wallet");
        endingBool = false;
        reshuffleText.text = "Free";
    }

    // Start is called before the first frame update
    void Start()
    {
        openingDayStart.DayStarting();
        ending = false;
        endingTimer = 5.0f;
        day = StaticTravel.dayCount;

        //Run Tutorial Customer on Day 1, otherwise run New Customer
        if (day == 1)
        {
            TutorialCustomer();
        }
        else
        {
            NewCustomer();
        }

        IconTextSort();
        trade = false;
        bargain = false;
        textProgression = false;
        dealOver = false;
        InitialOfferSetInactive(true);
        MakeOfferPhaseSetInactive();
        itemReshuffleButton.gameObject.SetActive(false);
        endGameButton.gameObject.SetActive(false);
        nextCustomerButton.gameObject.SetActive(false);
        itemCard.enabled = false;
        bargainSpeech.enabled = true;
        charEmote.enabled = false;
        turnCount = 0;
        customerCount = 1;
        sellCount = 0;
        textTimer = 2.0f;
        switch (StaticTravel.itemOfTheDay)
        {
            case "Food":
                itemOfTheDay = "Food";
                break;
            case "Drink":
                itemOfTheDay = "Drink";
                break;
            case "Mechanical":
                itemOfTheDay = "Machinery";
                break;
            case "Warmth":
                itemOfTheDay = "Warmth";
                break;
            case "Weapon":
                itemOfTheDay = "Weapon";
                break;
            case "Luxury":
                itemOfTheDay = "Luxury";
                break;
        }
        TypeWriterTextScript.CompleteTextRevealed += ButtonActivate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ending)
        {
           if(!endingBool)
            {
                closingDayEnd.DayEnding();
                endingBool = true;
            }
            endingTimer -= Time.deltaTime;
            if (endingTimer <= 0.0f)
            {
                for (int i = 0; i < itemManager.soldItems.Count; i++)
                {
                    StaticInventory.soldItemsList.Add(itemManager.soldItems[i]);
                    Debug.Log("solditems: " + StaticInventory.soldItemsList[i]);
                    StaticInventory.basePrice.Add(itemManager.itemPrice[i]);
                    StaticInventory.sellPrice.Add(itemManager.sellPrice[i]);
                    StaticInventory.charac.Add(character.prevCustomer[i]);
                    buttonPressEvent.Post(gameObject);
                }
                //PlayerPrefs.SetInt("wallet", (-shuffleCount * 5));
                Loader.Load(Loader.Scene.DayEndScene);
            }
        }
        ResetToMenu();
        walletText.text = wallet.ToString();
        if (!itemsShown)
        {
            InitialTrade();
        }
        priceBox.text = setPrice.ToString("00");
        if (bargain)
        {
            textTimer -= Time.deltaTime;
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

    void ButtonActivate()
    {
        textProgression = true;
    }

    void TradeSpeech()
    {
        if (textProgression)
        {
            if (introCount == 2 && introCount < introLength)
            {
                bargainSpeech.text = "" + character.GetIntro(introCount);
                typewriter.SetText(bargainSpeech.text);
                custDialogueEvent.Post(gameObject);
                introCount = 3;
                textProgression = false;
                customerAnimations.CustomerSpeakingActive();
            }
            else if (introCount == 1 && introCount < introLength)
            {
                bargainSpeech.text = "" + character.GetIntro(introCount);
                typewriter.SetText(bargainSpeech.text);
                custDialogueEvent.Post(gameObject);
                introCount = 2;
                textProgression = false;
                customerAnimations.CustomerSpeakingActive();
            }
            else if (introCount == 3 || introCount >= introLength)
            {
                //custDialogueEvent.Post(gameObject);
                trade = true;
                introCount = 0;
                textProgression = false;
                customerAnimations.CustomerSpeakingActive();
            }
        }
    }
    //generate a new customer.
    void NewCustomer()
    {
        customerAnimations.CustomerSpeakingArrive();
        playerApproachEvent.Post(gameObject);
        character.GenerateCustomer();
        customer.sprite = character.GetSprite();
        introLength = character.GetIntroLength();
        bargainSpeech.text = "" + character.GetIntro(introCount);
        typewriter.SetText(bargainSpeech.text);
        custDialogueEvent.Post(gameObject);
        custName.text = "" + character.GetCustName();
        introCount = 1;
        speechBubbleImage.sprite = speechBubbles[2];
        itemManager.GenerateItemStock(character.GetPrimaryDesire());
        print(character.GetPrimaryDesire());
        IconTextSort();
        turnsRemainingText.text = "3";
    }

    //Tutorial Customer to show Users how to play
    void TutorialCustomer()
    {
        customerAnimations.CustomerSpeakingArrive();            // Customer arriving animation
        playerApproachEvent.Post(gameObject);                   // Customer approach sound
                                                                // 
        character.GenerateTutorialCustomer();                   // Generates Tutorial Character
        customer.sprite = character.GetSprite();                // Generates Character Sprite
      
        tutorialLength = character.GetTutorialLength();         // Get Tutorial Text Nodes
        bargainSpeech.text = "" + character.GetTutorialIntro(); // Play Tutorial Text Dialogue
        typewriter.SetText(bargainSpeech.text);                 // Run Text Animation
        custDialogueEvent.Post(gameObject);                     // Customer Speech sound
        turnsRemainingText.text = "3";
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
            shelfLock.ShelfOpen();
        }
        itemReshuffleButton.gameObject.SetActive(true);
    }

    void CalculatePrice()
    {
        dupecount = itemManager.DupeCheck(selectedItem);
        basePrice = ((character.GetDrink() * itemManager.GetDrinkValue(selectedItem)) /** drinkMultiplier*/) + ((character.GetFood() * itemManager.GetFoodValue(selectedItem)) /** foodMultiplier*/) + ((character.GetLuxury() + itemManager.GetLuxuryValue(selectedItem)) /** luxuryMultiplier*/)
            + ((character.GetWeapon() * itemManager.GetWeaponValue(selectedItem)) /** weaponMultiplier*/) + ((character.GetWarmth() * itemManager.GetWarmthValue(selectedItem)) /** warmthMultiplier*/) + ((character.GetMachinery() * itemManager.GetMachineryValue(selectedItem)) /** machineryMultiplier*/);
        InitialOfferPhaseSetActive();
        priceBox.text = setPrice.ToString("00");
        TextPrompt.gameObject.SetActive(false);
        switch(dupecount)
        {
            case 1:
                dupeBonus = 0;
                break;
            case 2:
                dupeBonus = UnityEngine.Random.Range(8, 13);
                break;
            case 3:
                dupeBonus = UnityEngine.Random.Range(13,21);
                break;
            case 4:
                dupeBonus = UnityEngine.Random.Range(21,34);
                break;
        }
    }

    private async Task PriceConfirmAsync()
    {
        //TextPrompt.gameObject.SetActive(true);
        InitialOfferSetInactive(true);
        await Task.Delay(1000);
        bargainSpeech.enabled = true;
        bargain = true;
        charEmote.enabled = true;
        turnCount = 1;
        blinkingEmoticon.BlinkingEmoticonActive();
        ToleranceCall();
        Desperation();
        customer.enabled = true;
        textProgression = true;
        previousPrice = setPrice;
        initialOffer = setPrice;
        speechBubbleImage.enabled = true;
        PlayerUserInterface.DropUI();
        if (itemManager.GetPrimaryTag(selectedItem) == itemOfTheDay || itemManager.GetSecondaryTag(selectedItem) == itemOfTheDay)
        {
            tipBonus = UnityEngine.Random.Range(5, 13);
        }
        else
        {
            tipBonus = 0;
        }
    }

    IEnumerator AnimDelay(){
        yield return new WaitForSeconds(10.0f);
    }

    private void OfferPrice()
    {
        bargainSpeech.enabled = true;
        customer.enabled = true;
        turnCount++;
        Desperation();
        ToleranceCall();
        MakeOfferPhaseSetInactive();     
        previousPrice = setPrice;
        speechBubbleImage.enabled = true;
        speechBubble.SpeechBubble();
        blinkingEmoticon.BlinkingEmoticonActive();   
        if(turnCount == 3)
        {
            if (patience > 0)
            {
                AcceptDeal();
            }
            else if (patience == 0)
            {
                if (offerAccept)
                {
                    AcceptDeal();

                }
                else
                {
                    DeclineDeal();
                }
            }
            else
            {
                DeclineDeal();
            }
        }
        previousPriceText.SetText(previousPrice.ToString());
    }

    void PriceAnalysis(float based, float offer)
    {
        if(offer >= (based + 26))
        {
            patience -= 4;
            //replace later
            //richieReacting.RichieBad();
            bargainSpeech.text = character.GetEnragedText();
            typewriter.SetText(bargainSpeech.text);
            speechBubbleImage.sprite = speechBubbles[0];
           // charEmote.sprite = emoticons[0];
            //custEnragedEvent.Post(gameObject);
            blinkingEmoticon.BlinkingEmoticonActive();
            speechBubble.SpeechBubble();
            offerAccept = false;
            emotionTracker = 0;
        }
        else if(offer >= (based + 14) && offer <= (based + 25))
        {
            patience -= 2;

            //replace later
            //richieReacting.RichieBad();
            bargainSpeech.text = character.GetAngryText();
            typewriter.SetText(bargainSpeech.text);
            speechBubbleImage.sprite = speechBubbles[0];
           // charEmote.sprite = emoticons[1];
            //custAngryEvent.Post(gameObject);
            blinkingEmoticon.BlinkingEmoticonActive();
            speechBubble.SpeechBubble();
            offerAccept = false;
            emotionTracker = 1;
        }
        else if (offer >= (based + 4) && offer <= (based +13))
        {
            patience -= 1;

            //replace later
            //richieReacting.RichieBad();
            bargainSpeech.text = character.GetSweatText();;
            typewriter.SetText(bargainSpeech.text);
            speechBubbleImage.sprite = speechBubbles[0];
           // charEmote.sprite = emoticons[2];
            //custSweatEvent.Post(gameObject);
            blinkingEmoticon.BlinkingEmoticonActive();
            speechBubble.SpeechBubble();
            offerAccept = false;
            emotionTracker = 2;
        }
        else if (offer >= (based - 3) && offer <= (based + 3))
        {
            //animation

            //replace later
            //richieReacting.RichieGood();
            bargainSpeech.text = character.GetDiamondText();
            typewriter.SetText(bargainSpeech.text);
            //custDiamondEvent.Post(gameObject);
            speechBubbleImage.sprite = speechBubbles[1];
            //charEmote.sprite = emoticons[3];
            customerAnimations.CustomerSpeakingActive();
            offerAccept = true;
            emotionTracker = 3;
        }
        else if (offer >= (based - 13) && offer <= (based - 4))
        {
            patience += 1;
            //replace late
            //richieReacting.RichieGood();
            bargainSpeech.text = character.GetSurprisedText();
            typewriter.SetText(bargainSpeech.text);
            //custSurprisedEvent.Post(gameObject);
            speechBubbleImage.sprite = speechBubbles[2];
            //charEmote.sprite = emoticons[4];
            customerAnimations.CustomerSpeakingActive();
            offerAccept = true;
            emotionTracker = 4;
        }
        else if (offer >= (based -25) && offer <= (based - 14))
        {
            patience += 2;
            //replace later
            //richieReacting.RichieGood();
            bargainSpeech.text = character.GetAstonishedText();
            typewriter.SetText(bargainSpeech.text);
            //custAstonishedEvent.Post(gameObject);
            speechBubbleImage.sprite = speechBubbles[2];
            //charEmote.sprite = emoticons[5];
            customerAnimations.CustomerSpeakingActive();
            offerAccept = true;
            emotionTracker = 5;
        }
        else if(offer  <= (based - 26))
        {
            patience += 4;
            //replace later
            //richieReacting.RichieGood();
            bargainSpeech.text = character.GetBaffleeText();
            typewriter.SetText(bargainSpeech.text);
            //custBaffledEvent.Post(gameObject);
            speechBubbleImage.sprite = speechBubbles[2];
            //charEmote.sprite = emoticons[6];
            customerAnimations.CustomerSpeakingActive();
            offerAccept = true;
            emotionTracker = 6;
        }
    }

    void ToleranceCall()
    {
        switch(turnCount)
        {
            case 1:
                PriceAnalysis(basePrice, setPrice);
                break;
            case 2:
                PriceAnalysis(initialOffer, setPrice);
                PriceAnalysis(basePrice, setPrice);
                break;
            case 3:
                PriceAnalysis(previousPrice, setPrice);
                PriceAnalysis(basePrice, setPrice);
                break;
        }
        switch(emotionTracker)
        {
            case 0:
                richieReacting.RichieBad();
                custEnragedEvent.Post(gameObject);
                charEmote.sprite = emoticons[0];
                break;
            case 1:
                richieReacting.RichieBad();
                custAngryEvent.Post(gameObject);
                charEmote.sprite = emoticons[1];
                break;
            case 2:
                richieReacting.RichieBad();
                custSweatEvent.Post(gameObject);
                charEmote.sprite = emoticons[2];
                break;
            case 3:
                richieReacting.RichieGood();
                custDiamondEvent.Post(gameObject);
                charEmote.sprite = emoticons[3];
                break;
            case 4:
                richieReacting.RichieGood();
                custSurprisedEvent.Post(gameObject);
                charEmote.sprite = emoticons[4];
                break;
            case 5:
                richieReacting.RichieGood();
                custAstonishedEvent.Post(gameObject);
                charEmote.sprite = emoticons[5];
                break;
            case 6:
                richieReacting.RichieGood();
                custBaffledEvent.Post(gameObject);
                charEmote.sprite = emoticons[6];
                break;
        }
    }

    void Desperation()
    {
        switch (turnCount)
        {
            case 1:
                turnsRemainingText.text = "2";
                break;
            case 2:
                turnsRemainingText.text = "1";
                break;
            case 3:
                turnsRemainingText.text = "0";
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
        itemReshuffleButton.gameObject.SetActive(false);
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
        itemReshuffleButton.gameObject.SetActive(true);
        InitialOfferSetInactive(false);
        bargainSpeech.enabled = true;
        speechBubbleImage.enabled = true;
    }

    public void IncreasePrice()
    {
        setPrice += 1;
        if (setPrice > 99)
        {
            setPrice = 99;
        }
    }

    public void DecreasePrice()
    {
        setPrice -= 1;
        if (setPrice < 0)
        {
            setPrice = 0;
        }
    }

    public void IncreaseTen()
    {
        setPrice += 10;
        if (setPrice > 99)
        {
            setPrice = 99;
        }
    }

    public void DecreaseTen()
    {
        setPrice -= 10;
        if (setPrice < 0)
        {
            setPrice = 0;
        }
    }

    void AcceptDeal()
    {
        blinkingMoney.BlinkingCurrencyActive();
        richieReacting.RichiePerfect();
        saleSuccess.Post(gameObject);
        Debug.Log("accept deal");
        //custDiamondEvent.Post(gameObject);
        dealOver = true;
        speechBubbleImage.sprite = speechBubbles[1];
        bargainSpeech.text = character.GetAcceptTrade();
        typewriter.SetText(bargainSpeech.text);
        TextPrompt.gameObject.SetActive(false);
        itemManager.SoldItem(selectedItem, basePrice, ((int)setPrice + tipBonus + dupeBonus));
        character.SaleOver();
        charEmote.enabled = false;
        customer.enabled = true;
        bargain = false;
        //int walletValue = PlayerPrefs.GetInt("wallet") + (int)setPrice;
        //PlayerPrefs.SetInt("wallet", walletValue);
        wallet += (int)setPrice;
        wallet += tipBonus;
        wallet += dupeBonus;
        if (customerCount < 4)
        {
            nextCustomerButton.gameObject.SetActive(true);
        }
        else
        {
            ResetLevel();
        }
        setPrice = 0;
        tipBonus = 0;
        dupeBonus = 0;
        ++sellCount;
        playerLeaveEvent.Post(gameObject);
        PlayerUserInterface.RaiseUI();
        customerAnimations.CustomerSpeakingLeave();

    }

    void DeclineDeal()
    {
        dealOver = true;
        richieReacting.RichieBad();
        saleFailure.Post(gameObject);
        custAngryEvent.Post(gameObject);
        bargainSpeech.text = character.GetDeclineTrade();
        typewriter.SetText(bargainSpeech.text);
        TextPrompt.gameObject.SetActive(false);
        itemManager.FailedToSell(selectedItem, basePrice, 0);
        customer.enabled = true;
        charEmote.enabled = false;
        character.SaleOver();
        bargain = false;
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
        PlayerUserInterface.RaiseUI();
        customerAnimations.CustomerSpeakingLeave();

    }

    void ResetLevel()
    {
        sellCount = 0;
        ending = true;
    }

    public void ProgressText()
    {
        customerAnimations.CustomerSpeakingActive();
        speechBubble.SpeechBubble();
    }

    private void NextCustomer()
    {
        NewCustomer();
        richieReacting.RichieIdle();
        itemsShown = false;
        trade = false;
        bargain = false;
        textProgression = false;
        customer.enabled = true;
        bargainSpeech.enabled = true;
        custName.enabled = true;
        turnCount = 0;
        dealOver = false;
        InitialOfferSetInactive(true);
        MakeOfferPhaseSetInactive();
        endGameButton.gameObject.SetActive(false);
        nextCustomerButton.gameObject.SetActive(false);
        customerCount += 1;
        itemButtons[selectedItem].interactable = true;
        itemButtons[selectedItem].gameObject.SetActive(false);
        itemText[selectedItem].enabled = false;
    }

    void MakeOfferPhaseSetActive()
    {
        if (wooshBool)
        {
            wooshingUIevent.Post(gameObject);
            wooshBool = false;
        }

        previousPriceText.SetText(previousPrice.ToString());
    }

    void MakeOfferPhaseSetInactive()
    {
        if (!wooshBool)
        {
            wooshingUIevent.Post(gameObject);
            wooshBool = true;
        }
        bargainSpeech.enabled = true;

    }

    void InitialOfferPhaseSetActive()
    {
        if (wooshBool)
        {
            wooshingUIevent.Post(gameObject);
            wooshBool = false;
        }
        bargainometer.enabled = true;
        initialPrice.BargainPhaseSetActive();
        dimmer.enabled = true;
    }

    void InitialOfferSetInactive(bool buttonCheck)
    {
        if (!wooshBool)
        {
            wooshingUIevent.Post(gameObject);
            wooshBool = true;
        }
        initialPrice.BargainPhaseSetInactive();
        dimmer.enabled = false;
        if (buttonCheck )
        {
            shelfLock.ShelfClose();    
        }
        
    }

   public void HoverItem1()
    {
        itemCard.enabled = true;
        itemCard.sprite = itemManager.GetItemDescription(0);
        itemHovers.ItemHoverEnter();
    }

    public void HoverItem2()
    {
        itemCard.enabled = true;
        itemCard.sprite = itemManager.GetItemDescription(1);
        itemHovers.ItemHoverEnter();
    }

    public void HoverItem3()
    {
        itemCard.enabled = true;
        itemCard.sprite = itemManager.GetItemDescription(2);
        itemHovers.ItemHoverEnter();
    }

    public void HoverItem4()
    {
        itemCard.enabled = true;
        itemCard.sprite = itemManager.GetItemDescription(3);
        itemHovers.ItemHoverEnter();
    }

    public void HoverExit()
    {
        itemCard.enabled = false;

    }

    private void IconTextSort()
    {
        for (int i = 0; i < itemManager.RemainingItems(); ++i)
        {
            itemButtons[i].image.sprite = itemManager.GetSprite(i);
            itemText[i].text = itemManager.GetName(i);
            itemText[i].enabled = true;
        }
    }

    public void ItemReshuffle()
    {
        int cost = 5 * (shuffleCount);
        if(wallet >= cost || shuffleCount == 0)
        {
            reshuffleEvent.Post(gameObject);
            if(shuffleCount > 0)
            {
                wallet -= cost;
            }
            //shelfLock.ReshuffleItems(); ANIMATION DISABLED FOR NOW
            ++shuffleCount;
            itemManager.GenerateItemStock(character.GetPrimaryDesire());
            IconTextSort();
            StaticTravel.shuffleCosts += cost;
            reshuffleText.text = "-" + (5 * shuffleCount).ToString();
        }
    }
}
