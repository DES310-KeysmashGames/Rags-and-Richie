using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Weapon,
    Clothing,
    Drink,
    Machinery,
    Luxury,
}

[CreateAssetMenu(menuName = "Item System/Item", fileName = "New Item")]
public class BaseItem : ScriptableObject
{
    [SerializeField] public Sprite frontSprite;
    [SerializeField] public string itemName;
    [SerializeField] public ItemType type;
    [TextArea][SerializeField] public string Description;
    [SerializeField] public int foodValue;
    [SerializeField] public int drinkValue;
    [SerializeField] public int warmthValue;
    [SerializeField] public int weaponValue;
    [SerializeField] public int machineryValue;
    [SerializeField] public int luxuryValue;
}