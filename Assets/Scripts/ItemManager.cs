using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<BaseItem> inventory = new List<BaseItem>();
    [SerializeField] private List<BaseItem> shopStock = new List<BaseItem>();
    public List<BaseItem> soldItems = new List<BaseItem>();
    public List<int> itemPrice = new List<int>();
    public List<int> sellPrice = new List<int>();

    private bool itemExists;

    //generate 4 items from the list of items and adds them to the array of current items available in the store
    public void GenerateItemList()
    { 
        for( int i = 0; i < StaticInventory.intermediateList.Count; ++i )
        {
            inventory.Add( StaticInventory.intermediateList[i] );
        }
    }

    public void GenerateItemStock(string itemCategory)
    {
        inventory.RemoveRange(0, inventory.Count);
        for (int i = 0; i < 4; ++i)
        {
            int index = UnityEngine.Random.Range(0, shopStock.Count);
            inventory.Add(shopStock[index]);
        }
    }


    private void TagMatch(string itemCat, int index)
    {
        if(shopStock[index].primaryType.ToString() == itemCat)
        {
            //itemTagMatch = true;
        }
    }

    public void FailedToSell(int no, int price, int sell)
    {
        soldItems.Add(inventory[no]);
        itemPrice.Add(price);
        sellPrice.Add(sell);
    }

    public void SoldItem(int no, int price, int sell)
    {
        soldItems.Add(inventory[no]);
        inventory.RemoveAt(no);
        itemPrice.Add(price);
        sellPrice.Add(sell);
    }

    public int RemainingItems()
    {
        return inventory.Count;
    }

    public Sprite GetSprite(int itemNo)
    { 
        return inventory[itemNo].frontSprite;
    }
    public string GetName(int itemNo)
    {
        return inventory[itemNo].name;
    }

    public Sprite GetItemDescription(int itemNo)
    {
        return inventory[itemNo].itemDescription;
    }

    public int GetWeaponValue(int itemNo)
    {
        return inventory[itemNo].weaponValue;
    }
    public int GetLuxuryValue(int itemNo)
    {
        return inventory[itemNo].luxuryValue;
    }
    public int GetDrinkValue(int itemNo)
    {
        return inventory[itemNo].drinkValue;
    }
    public int GetFoodValue(int itemNo)
    {
        return inventory[itemNo].foodValue;
    }
    public int GetMachineryValue(int itemNo)
    {
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
