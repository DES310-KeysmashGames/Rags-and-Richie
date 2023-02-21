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

    private CharacterManager character;
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private SpriteRenderer customer;
    [SerializeField] private SpriteRenderer[] items;
    [SerializeField] private TextMeshProUGUI[] itemText;
    [SerializeField] private Button[] itemButtons;
    private float timer;
    private bool trade;
    [SerializeField] private int price;
    private bool itemsShown;
    //private int itemNo;
    private ItemManager itemManager;

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
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!itemsShown)
        {
            InitialTrade();
        }
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
        Debug.Log("This runs 1");
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
        Debug.Log("This runs 2");
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
        Debug.Log("This runs 3");
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
        Debug.Log("This runs 4");
    }
}
