using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventorySelection : MonoBehaviour
{
    //list for items to be selected from
    [SerializeField] public List<BaseItem> scavengedItems = new List<BaseItem>();
    //list for items to be selected into
    [SerializeField] public List<BaseItem> chosenInventory = new List<BaseItem>();
    [SerializeField] private Image[] scavengedItemSprites;
    [SerializeField] private Image[] sprites;

    private void Start(){
        AssignSprites();
    }

    public Sprite GetSprite(int i)
    {
        //return CurrentItems[itemNo].frontSprite;
        return scavengedItems[i].frontSprite;
    }
    private void AssignSprites(){
        for (int i = 0; i < scavengedItemSprites.Length; ++i)
        {
            scavengedItemSprites[i].enabled = true;
            scavengedItemSprites[i].sprite = GetSprite(i);
        }
    }
}
