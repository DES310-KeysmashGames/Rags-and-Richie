using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<BaseItem> fullstock = new List<BaseItem>();
    [SerializeField] private List<BaseItem> inventory = new List<BaseItem>();
    [SerializeField] public List<BaseItem> soldItems = new List<BaseItem>();
    private bool itemExists;

    //generate 4 items from the list of items and adds them to the array of current items available in the store
    public void GenerateItemList()
    { 
        for( int i = 0; i < StaticInventory.intermediateList.Count; ++i )
        {
            inventory.Add( StaticInventory.intermediateList[i] );
        }
    }
    public void SoldItem(int no)
    {
        soldItems.Add(inventory[no]);
        inventory.RemoveAt(no);
    }

    public int RemainingItems()
    {
        return inventory.Count;
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
    public int GetWarmthValue(int itemNo)
    {
        return inventory[itemNo].warmthValue;
    }
    public void Reset()
    {
        inventory.RemoveRange(0, inventory.Count);
        StaticInventory.intermediateList.RemoveRange(0, StaticInventory.intermediateList.Count);
    }
}
