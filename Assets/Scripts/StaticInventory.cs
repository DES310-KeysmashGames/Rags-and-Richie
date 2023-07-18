using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticInventory 
{
    public static List<BaseItem> intermediateList = new List<BaseItem>();
    public static List<BaseItem> soldItemsList = new List<BaseItem>();

    public static List<int> basePrice = new List<int>();
    public static List<int> sellPrice = new List<int>();

    public static List<Sprite> itemSprites = new List<Sprite>();
    public static List<BaseCharacter> charac = new List<BaseCharacter>();
}
