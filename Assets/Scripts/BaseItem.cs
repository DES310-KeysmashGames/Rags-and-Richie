using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Weapon,
    Warmth,
    Drink,
    Machinery,
    Luxury,
    Mystery,
}

[CreateAssetMenu(menuName = "Item System/Item", fileName = "New Item")]
public class BaseItem : ScriptableObject
{
    public Sprite frontSprite;
    public string itemName;
    public ItemType primaryType;
    public ItemType secondaryType;
    public ItemType tertiaryType;
    public Sprite itemDescription;
    [TextArea] public string Description;
    public int foodValue;
    public int drinkValue;
    public int warmthValue;
    public int weaponValue;
    public int machineryValue;
    public int luxuryValue;
}