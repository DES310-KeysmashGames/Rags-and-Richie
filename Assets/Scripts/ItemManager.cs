using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] public List<BaseItem> fullstock = new List<BaseItem>();
    [SerializeField] public List<BaseItem> inventory = new List<BaseItem>();

    //generate 4 items from the list of items and adds them to the array of current items available in the store
    public void GenerateItemList()
    {
        for (int i = 0; i < 5; ++i)
        {
            //int index = UnityEngine.Random.Range(0, itemList.Length);
            //Debug.Log("Index number is " + index);
            //Debug.Log("Item is " + itemList[index].name);
            //Debug.Log("Added to current items " + CurrentItems[i].name);
            //CurrentItems[i] = itemList[index];
            int index = UnityEngine.Random.Range(0, fullstock.Count);
            inventory.Add(fullstock[index]);
            Debug.Log(inventory[i].name);
         } 
    }

    public Sprite GetSprite(int itemNo)
    {
        //return CurrentItems[itemNo].frontSprite;
        return inventory[itemNo].frontSprite;
    }

    public string GetName(int itemNo)
    {
        return inventory[itemNo].name;
    }

    public int GetWeaponValue(int itemNo)
    {
        //return CurrentItems[itemNo].weaponValue;
        return inventory[itemNo].weaponValue;
    }
    public int GetLuxuryValue(int itemNo)
    {
        //return CurrentItems[itemNo].luxuryValue;
        return inventory[itemNo].luxuryValue;
    }
    public int GetDrinkValue(int itemNo)
    {
        //return CurrentItems[itemNo].drinkValue;
        return inventory[itemNo].drinkValue;
    }
    public int GetFoodValue(int itemNo)
    {
        // return CurrentItems[itemNo].foodValue;
        return inventory[itemNo].foodValue;
    }
    public int GetMachineryValue(int itemNo)
    {
        // return CurrentItems[itemNo].machineryValue;
        return inventory[itemNo].machineryValue;
    }
}
