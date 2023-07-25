using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Timers;
using Unity.VisualScripting;

public class TravelManager : MonoBehaviour
{
    RichieScript richie;
    AnimateText animateText;
    TypeWriterTextScript typewriter;

    //Richie Info interaction
    [Header("Richie Dialogue")]
    [SerializeField] private Image richieImage;
    [SerializeField] private TextMeshProUGUI richieText;
    [SerializeField] private Image richieTextBox;
    [SerializeField] private Image dimmer;

    //Button for next day
    [Header("Currency and Next Day")]
    [SerializeField] private Image currencyImage;
    [SerializeField] private TextMeshProUGUI currency;
    [SerializeField] private Button nextButton;


    //Truck & City
    [Header("Truck and City")]
    [SerializeField] private Button[] cityButton;
    [SerializeField] private Image truck;
    [SerializeField] private Sprite[] citySprites;
    [SerializeField] private Sprite[] citySelectedSprites;

    //RichieText
    [SerializeField] private Button continueTextButton;
    [SerializeField] private int tutorialCount;
    [SerializeField] bool readText;

    //Variables
    [SerializeField] private bool truckMoved, cityClicked, nextClicked;
 
    private int wallet;

    private int day;
    private bool isPlaying;

    //Animations
    [SerializeField] AnimationTrade travelTutorialText;
    [SerializeField] AnimationTrade CityPulse;
    [SerializeField] AnimationTrade truckSwipe;

    public AK.Wwise.Event richieDialogueEvent;
    public AK.Wwise.Event buttonPressEvent;
    public AK.Wwise.Event truckVroomEvent;

    private void Awake()
    {
        animateText = GetComponent<AnimateText>();
        richie = GetComponent<RichieScript>();
        typewriter = GetComponent<TypeWriterTextScript>();
        continueTextButton.onClick.AddListener(() =>
        {
            TextRead();
            buttonPressEvent.Post(gameObject);
        });
        //Go to Item Selection scene on Click
        nextButton.onClick.AddListener(() =>
        {
            //Next Day Button Audio
            buttonPressEvent.Post(gameObject);
            MoveTruck();
            nextClicked = true;
            
        });
        tutorialCount = 0;
        readText = false;
    }

    void Start()
    {
        day = StaticTravel.dayCount;
        richieImage.enabled = true;
        richieText.enabled = true;

        nextButton.gameObject.SetActive(false);
        continueTextButton.gameObject.SetActive(false);
        truck.enabled = true;

        cityClicked = false;
        nextClicked = false;
        truckMoved = false;

        wallet = PlayerPrefs.GetInt("wallet");
        if (day == 1)
        {
            travelTutorialText.TutorialText();
            dimmer.enabled = true;
            richieText.text = richie.GetTutorial(tutorialCount);
            typewriter.SetText(richieText.text);
            richieDialogueEvent.Post(gameObject);
            cityButton[1].interactable = false;
            continueTextButton.gameObject.SetActive(true);
            ++tutorialCount;
            cityButton[1].image.sprite = citySprites[0];
            cityButton[1].gameObject.SetActive(true);
            cityButton[0].gameObject.SetActive(false);
            cityButton[2].gameObject.SetActive(false);
        }
        else if (day == 2)
        {
            dimmer.enabled = false;
            cityButton[0].image.sprite = citySprites[1];
            cityButton[0].gameObject.SetActive(true);
            cityButton[1].gameObject.SetActive(false);
            cityButton[2].image.sprite = citySprites[2];
            cityButton[2].gameObject.SetActive(true);
        }
        else if (day == 3)
        {
            for (int i = 0; i < cityButton.Length; ++i)
            {
                dimmer.enabled = false;
                cityButton[i].image.sprite = citySprites[i + 3];
                cityButton[i].gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        if(day == 1)
        {
            if (readText)
            {
                if( tutorialCount < (richie.GetTutorialLength()-1))
                {
                    richieText.text = richie.GetTutorial(tutorialCount);
                    typewriter.SetText(richieText.text);
                    continueTextButton.gameObject.SetActive(true);
                    ++tutorialCount;
                    readText = false;
                    richieDialogueEvent.Post(gameObject);
                }
                else
                {
                    richieText.text = richie.GetTutorial(tutorialCount);
                    typewriter.SetText(richieText.text);
                    cityButton[1].interactable = true;
                    tutorialCount = 0;
                    readText = false;
                    richieDialogueEvent.Post(gameObject);
                    dimmer.enabled = false;
                    travelTutorialText.NoneTutorialText();
                }
            }
        }
        //Update Wallet 
        UpdateCurrency();

        //Enable Next button if city is clicked
        if (cityClicked)
        {
            nextButton.gameObject.SetActive(true);
        }

        //Move truck if next is clicked
        if (nextClicked)
        {   
            if (!isPlaying)
            {
                truckVroomEvent.Post(gameObject);
                isPlaying = true;
                nextButton.gameObject.SetActive(false);
                cityClicked = false;
            }
        }

        //Load next scene when truck has finished moving
        if (truckMoved)
        {
            Loader.Load(Loader.Scene.TradeScene);
        }
    }

    void UpdateCurrency()
    {
        currency.text = wallet.ToString("0");
    }
   
    //Function to move truck across screen
    async void MoveTruck()
    {
        truckSwipe.TruckDrive();
        await Task.Delay(2000);
        TruckHasMoved();
        nextButton.gameObject.SetActive(false);
    }

    private void TruckHasMoved()
    {

        truckMoved = true;

    }

    public void CityClicked1()
    { 
        cityClicked = true;
        buttonPressEvent.Post(gameObject);
        switch (day)
        {
            case 2:
                richieText.text = richie.GetBurnington();
                typewriter.SetText(richieText.text);
                richieDialogueEvent.Post(gameObject);
                StaticTravel.expenses = 30;
                StaticTravel.itemOfTheDay = "Drink";
                StaticTravel.location = "Burnington";
                cityButton[0].image.sprite = citySelectedSprites[1];
                cityButton[2].image.sprite = citySprites[2];
                break;
            case 3:
                richieText.text = richie.GetBrokenMetro();
                typewriter.SetText(richieText.text);
                richieDialogueEvent.Post(gameObject);
                StaticTravel.expenses = 40;
                StaticTravel.itemOfTheDay = "Mechanical";
                StaticTravel.location = "BrokenMetro";
                cityButton[0].image.sprite = citySelectedSprites[3];
                cityButton[1].image.sprite = citySprites[4];
                cityButton[2].image.sprite = citySprites[5];
                break;
        }
    }

    public void CityClicked2()
    {
        cityClicked = true;
        buttonPressEvent.Post(gameObject);
        switch (day)
        {
            case 1:
                richieText.text = richie.GetToxicTowers();
                typewriter.SetText(richieText.text);
                richieDialogueEvent.Post(gameObject);
                StaticTravel.expenses = 20;
                StaticTravel.itemOfTheDay = "Weapon";
                StaticTravel.location = "ToxicTowers";
                cityButton[1].image.sprite = citySelectedSprites[0];
                break;
            case 3:
                richieText.text = richie.GetVacancy();
                typewriter.SetText(richieText.text);
                richieDialogueEvent.Post(gameObject);
                StaticTravel.expenses = 20;
                StaticTravel.itemOfTheDay = "Mystery";
                StaticTravel.location = "Vacancy";
                cityButton[1].image.sprite = citySelectedSprites[4];
                cityButton[0].image.sprite = citySprites[3];
                cityButton[2].image.sprite = citySprites[5];
                break;
        }
    }

    public void CityClicked3()
    {
        cityClicked = true;
        buttonPressEvent.Post(gameObject);
        switch (day)
        {
            case 2:
                richieText.text = richie.GetSkyHigh();
                typewriter.SetText(richieText.text);
                richieDialogueEvent.Post(gameObject);
                StaticTravel.expenses = 30;
                StaticTravel.itemOfTheDay = "Warmth";
                StaticTravel.location = "SkyHigh";
                cityButton[2].image.sprite = citySelectedSprites[2];
                cityButton[0].image.sprite = citySprites[1];
                break;
            case 3:
                richieText.text = richie.GetLostAngeles();
                typewriter.SetText(richieText.text);
                richieDialogueEvent.Post(gameObject);
                StaticTravel.expenses = 40;
                StaticTravel.itemOfTheDay = "Luxury";
                StaticTravel.location = "LostAngeles";
                cityButton[2].image.sprite = citySelectedSprites[5];
                cityButton[0].image.sprite = citySprites[3];
                cityButton[1].image.sprite = citySprites[4];
                break;
        }
    }
    
    private void TextRead()
    {
        readText = true;
        continueTextButton.gameObject.SetActive(false);
    }
}