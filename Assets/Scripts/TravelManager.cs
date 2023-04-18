using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Timers;
using Unity.VisualScripting;

public class TravelManager : MonoBehaviour
{
    //Richie Info interaction
    [Header("Richie Dialogue")]
    [SerializeField] private Image richieImage;
    [SerializeField] private TextMeshProUGUI richieText;
    [SerializeField] private Image richieTextBox;

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

    //Variables
    [SerializeField] private bool truckMoved, cityClicked, nextClicked;
    private int moveSpeed;
    private int wallet;
    private int expenses;
    private int day;

    private void Awake()
    {
        //Go to Item Selection scene on Click
        nextButton.onClick.AddListener(() =>
        {
            //Next Day Button Audio

            nextClicked = true;
        });
    }

    void Start()
    {
        day = StaticTravel.dayCount;
        //Setting Richie vars to false
        richieText.enabled = false;
        richieImage.enabled = false;
        richieTextBox.enabled = false;

        nextButton.gameObject.SetActive(false);
        truck.enabled = true;

        moveSpeed = 3;

        cityClicked = false;
        nextClicked = false;
        truckMoved = false;

        wallet = PlayerPrefs.GetInt("wallet");
        if (day == 1)
        {
            cityButton[1].image.sprite = citySprites[0];
            cityButton[1].gameObject.SetActive(true);
            cityButton[0].gameObject.SetActive(false);
            cityButton[2].gameObject.SetActive(false);
        }
        else if (day == 2)
        {
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
                cityButton[i].image.sprite = citySprites[i + 3];
                cityButton[i].gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        //Update Wallet 
        UpdateCurrency();

        //Enable Richie Dialogue
        RichieDialogue();

        //Enable Next button if city is clicked
        if (cityClicked)
        {
            nextButton.gameObject.SetActive(true);
        }

        //Move truck if next is clicked
        if (nextClicked)
        {
            MoveTruck();
        }

        //Load next scene when truck has finished moving
        if (truckMoved)
        {
            Loader.Load(Loader.Scene.ItemSelectScene);
        }
    }

    void RichieDialogue()
    {
        //Initiate Richie Dialogue
        richieImage.enabled = true;
        richieTextBox.enabled = true;
        richieText.enabled = true;

        //Richie Speaking Audio

        richieText.text = "What's up... The name's Richie";

        //richieText.text = "Another day on the road, looks like we're travelling to Toxic Towers today!";
        //richieText.text = "Anyways, lets get going!";
    }

    void UpdateCurrency()
    {
        currency.text = wallet.ToString("0");
    }
   
    //Function to move truck across screen
    void MoveTruck()
    { 
        truck.transform.position = Vector2.Lerp(truck.transform.position, new Vector2(Screen.width * 1.3f, truck.transform.position.y), Time.deltaTime * moveSpeed);

        if (truck.transform.position.x >= Screen.width)
        {
            truck.enabled = false;
            truckMoved = true;
        }
    }

    public void CityClicked1()
    { 
        cityClicked = true;
        switch (day)
        {
            case 2:
                StaticTravel.expenses = 30;
                StaticTravel.itemOfTheDay = "Drink";
                StaticTravel.location = "Burnington";
                cityButton[0].image.sprite = citySelectedSprites[1];
                cityButton[1].image.sprite = citySprites[2];
                cityButton[2].image.sprite = citySprites[3];
                break;
            case 3:
                StaticTravel.expenses = 40;
                StaticTravel.itemOfTheDay = "Mechanical";
                StaticTravel.location = "BrokenMetro";
                cityButton[0].image.sprite = citySelectedSprites[4];
                cityButton[1].image.sprite = citySprites[5];
                cityButton[2].image.sprite = citySprites[6];
                break;
        }

        //City Click Button Audio

    }

    public void CityClicked2()
    {
        cityClicked = true;
        switch (day)
        {
            case 1:
                StaticTravel.expenses = 20;
                StaticTravel.itemOfTheDay = "Weapon";
                StaticTravel.location = "ToxicTowers";
                cityButton[1].image.sprite = citySelectedSprites[0];
                break;
            case 3:
                StaticTravel.expenses = 20;
                StaticTravel.itemOfTheDay = "Mystery";
                StaticTravel.location = "Vacancy";
                cityButton[1].image.sprite = citySelectedSprites[2];
                cityButton[0].image.sprite = citySprites[1];
                cityButton[2].image.sprite = citySprites[3];
                break;
        }

        //City Click Button Audio

    }

    public void CityClicked3()
    {
        cityClicked = true;
        switch (day)
        {
            case 2:
                StaticTravel.expenses = 30;
                StaticTravel.itemOfTheDay = "Warmth";
                StaticTravel.location = "SkyHigh";
                cityButton[2].image.sprite = citySelectedSprites[3];
                cityButton[0].image.sprite = citySprites[1];
                cityButton[1].image.sprite = citySprites[2];
                break;
            case 3:
                StaticTravel.expenses = 40;
                StaticTravel.itemOfTheDay = "Luxury";
                StaticTravel.location = "LostAngeles";
                cityButton[2].image.sprite = citySelectedSprites[6];
                cityButton[0].image.sprite = citySprites[4];
                cityButton[1].image.sprite = citySprites[5];
                break;
        }

        //City Click Button Audio

    }

    //Travel Scene Background Audio
}