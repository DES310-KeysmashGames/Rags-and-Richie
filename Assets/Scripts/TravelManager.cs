using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;
using System.Timers;

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
    [SerializeField] private Button cityButton;
    [SerializeField] private Image truck;

    //Variables
    private bool truckMoved, cityClicked, nextClicked;
    private int moveSpeed;
    private int wallet;
    private int expenses;

    private void Awake()
    {
        //Go to Item Selection scene on Click
        nextButton.onClick.AddListener(() =>
        {
            nextClicked = true;
        });

        //Make next day button visible on click
        cityButton.onClick.AddListener(() =>
        {
            cityClicked = true;
        });
    }

    void Start()
    {
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
        truck.transform.position = Vector2.Lerp(truck.transform.position, new Vector2(Screen.width * .9f, truck.transform.position.y), Time.deltaTime * moveSpeed);

        if (truck.transform.position.x >= Screen.width * .85f)
        {
            truck.enabled = false;
            truckMoved = true;
        }
    }
}