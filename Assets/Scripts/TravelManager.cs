using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

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

    //Truck 
    [SerializeField] private Image truck;

    //Variables
    private bool truckMoved;
    private int moveSpeed;
    private int wallet;
    private int expenses;

    private void Awake()
    {
        //Go to Item Selection scene on Click
        nextButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.ItemSelectScene);
        });
       
    }

    void Start()
    {
        //Setting Variables' status
        richieText.enabled = false;
        richieImage.enabled = false;
        richieTextBox.enabled = false;

        nextButton.gameObject.SetActive(false);
        truck.enabled = true;

        moveSpeed = 2;
        truckMoved = false;

        wallet = PlayerPrefs.GetInt("wallet");

    }

    void Update()
    {
        MoveTruck();
        UpdateCurrency();
        RichieDialogue();
    }

    void RichieDialogue()
    {
        //Initiate Richie Dialogue
        richieImage.enabled = true;
        richieTextBox.enabled = true;
        richieText.gameObject.SetActive(true);

        richieText.text = "What's up... The name's Richie";
    }

    void UpdateCurrency()
    {
        currency.text = wallet.ToString("0");
    }
   
    //Function to move truck across screen
    void MoveTruck()
    {

        if (Input.GetKey(KeyCode.G))
        {
            truck.transform.position = Vector2.Lerp(truck.transform.position, new Vector2(1000, truck.transform.position.y), Time.deltaTime * moveSpeed);
            truckMoved = true;
        }

        //If truck has moved, allow user to go to next scene
        if (truckMoved)
        {
            nextButton.gameObject.SetActive(true);
        }
    }
}
