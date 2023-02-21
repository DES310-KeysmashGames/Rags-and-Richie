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
    [SerializeField] private float timer;
    [SerializeField] private bool trade;
    [SerializeField] private int price;
    private int introCount;
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
        introCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //doing customer intro
        if (timer <= 0.0f && introCount == 1)
        {
            if (character.GetIntro(introCount) == null)
            {
                ItemsForSale();
                introCount = 0;
            }
            else
            {
                speechText.text = character.GetIntro(introCount);
                timer = 5.0f;
                introCount = 2;
            }
        }
        if (timer <= 0.0f && introCount == 2)
        {
            if (character.GetIntro(introCount) == null)
            {
                ItemsForSale();
                introCount = 0;
            }
            else
            {
                speechText.text = character.GetIntro(introCount);
                timer = 5.0f;
                introCount = 0;
            }
        }
        //timer for the intro dialogue
        if (timer <= 0.0f && trade == false)
        {
            speechText.text = "" + character.TradeSpeech();
            timer = 2.0f;
            trade = true;
        }
        //transitions to selling the stores wares.
        if (timer <= 0.0f && trade == true)
        {
            ItemsForSale();
        }
    }

    //generate a new customer.
    void NewCustomer()
    {
        character.GenerateCustomer();
        customer.sprite = character.GetSprite();
        speechText.text = "" + character.GetIntro(0);
        timer = 5.0f;
        introCount = 1;
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
}
