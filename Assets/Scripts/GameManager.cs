using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    [SerializeField] private CharacterManager character;
    [SerializeField] private TextMeshProUGUI speechText;
    [SerializeField] private SpriteRenderer customer;
    [SerializeField] private SpriteRenderer[] items;
    [SerializeField] private TextMeshProUGUI[] itemText;
    [SerializeField] private float timer;
    [SerializeField] private bool trade;
    [SerializeField] private ItemManager itemManager;

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
        }
        trade = false;
        timer = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
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
            Debug.Log("This line is called");
        }
    }

    //generate a new customer.
    void NewCustomer()
    {
        character.GenerateCustomer();
        speechText.text = "" + character.GetIntro();
        customer.sprite = character.GetSprite();
        timer = 5.0f;
    }

    //displays the items available for sale.
    void ItemsForSale()
    {
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].enabled = true;
            items[i].sprite = itemManager.GetSprite(i);
        }
    }

    //disables showing the items.
    void DisableItems()
    {
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].enabled = false;
        }
    }
}
