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
    private bool itemExists;

    //generate 4 items from the list of items and adds them to the array of current items available in the store
    public void GenerateItemList()
    {
        //for (int i = 0; i < 5; ++i)
        //{
        //    int index = UnityEngine.Random.Range(0, fullstock.Count);
        //    for (int j = 0; j < inventory.Count; ++j)
        //    {
        //        if (fullstock[index].name == inventory[j].name)
        //        {
        //            itemExists = true;
        //        }
        //    }
        //    if (itemExists)
        //    {
        //        i--;
        //    }
        //    else
        //    {
        //        inventory.Add(fullstock[index]);
        //    }
        //    itemExists = false;
        // } 
        for( int i = 0; i < StaticInventory.intermediateList.Count; ++i )
        {
            inventory.Add( StaticInventory.intermediateList[i] );
        }
    }

    public void SoldItem(int no)
    {
        inventory.RemoveAt(no);
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
        for(int i =0; i < inventory.Count; i++)
        {
            inventory.RemoveAt(i);
        }
    }
}
