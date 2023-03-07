using System;
//using System.Collections;
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
    [SerializeField] Button confirmButton;

    private void Awake(){
        confirmButton.onClick.AddListener(()=> {
            //click action
            Loader.Load(Loader.Scene.TradeScene);
        });
    }
    private void Start(){
        ShuffleItems(scavengedItems);
        AssignSprites();
    }

    void ShuffleItems<T>(List<T> inputList){
        for (int i=0;i<inputList.Count - 1 ;i++){
            T temp = inputList[i];
            int rand = UnityEngine.Random.Range(i,inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;

        }
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
