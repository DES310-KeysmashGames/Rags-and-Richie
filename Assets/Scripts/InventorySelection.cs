using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class InventorySelection : MonoBehaviour
{
    //list for items to be selected from
    [SerializeField] public List<BaseItem> scavengedItems = new List<BaseItem>();
    //list for items to be selected into
    [SerializeField] public List<BaseItem> chosenInventory = new List<BaseItem>();
}
