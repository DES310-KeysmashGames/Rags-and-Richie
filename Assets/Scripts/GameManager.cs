using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public CharacterManager character;
    [SerializeField] public TextMeshProUGUI speechText;
    [SerializeField] public SpriteRenderer customer;
    [SerializeField] public SpriteRenderer[] items;
    private float timer;
    private bool trade;
    public ItemManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        NewCustomer();
        itemManager.GenerateItemList();
        for (int i = 0; i < items.Length; ++i)
        {
            items[i].enabled = false;
            items[i].sprite = itemManager.GetSprite(i);
        }
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
        character.generateCustomer();
        speechText.text = "" + character.getIntro();
        customer.sprite = character.getSprite();
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
