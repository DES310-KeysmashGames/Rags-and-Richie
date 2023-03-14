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
    //scavanged items
    [SerializeField] private List<Button> selectionButtons = new List<Button>();
    [SerializeField] private List<BaseItem> fullItemList = new List<BaseItem> ();
    [SerializeField] public List<BaseItem> scavengedItems = new List<BaseItem>();
    [SerializeField] private Image[] scavengedItemSprites;
    //list for items to be selected into
    //chosen items
    [SerializeField] public List<BaseItem> chosenInventory = new List<BaseItem>();
    [SerializeField] private Image[] chosenItemsprites;
    private bool reactivate;
    private int chosenIndexStart = 0;

    bool itemExists;

    //buttons
    [SerializeField] Button confirmButton;
    [SerializeField] private Button removeLastItemButton;

    private void Awake(){
        confirmButton.onClick.AddListener(()=> {
            //click action
            if(chosenInventory.Count == 4){
                for( int i =0; i < chosenInventory.Count; i++){
                    StaticInventory.intermediateList.Add(chosenInventory[i]);
                }
                
                Loader.Load(Loader.Scene.TradeScene);
            }
            
        });
    }
    private void Start(){
        ShuffleItems(/*scavengedItems*/);
        AssignSprites();
        for (int i = 0; i < selectionButtons.Count; i++){
        int closureIndex = i ; // Prevents the closure problem
        selectionButtons[closureIndex].onClick.AddListener( () => TaskOnClick( closureIndex ) );
        }
    }

    private void Update(){
        if (chosenInventory.Count !=4){
            confirmButton.GetComponent<Image>().color = Color.grey;
        }else{
            confirmButton.GetComponent<Image>().color = Color.green;
        }
        if(chosenInventory.Count > 0)
        {
            removeLastItemButton.gameObject.SetActive(true);
        }
        else
        {
            removeLastItemButton.gameObject.SetActive(false);
        }
    }

    private void TaskOnClick(int buttonIndex)
    {
        //Debug.Log("You have clicked the button #" + buttonIndex, selectionButtons[buttonIndex]);

        //if inventory is not full, add item
        if (chosenInventory.Count < 4) {
            Debug.Log("This item is: " + scavengedItems[buttonIndex]);
            chosenInventory.Add(scavengedItems[buttonIndex]);
            Debug.Log("You have added " + chosenInventory.Count + " items to your inventory");
            selectionButtons[buttonIndex].interactable = false;
        }
        else
        {
            Debug.Log("Inventory is full");
        }
        UpdateChosenSprites(buttonIndex);
    }

    //void ShuffleItems<T>(List<T> inputList)
    void ShuffleItems()
    {
        //for (int i=0;i<inputList.Count - 1 ;i++)
        //{
        //    T temp = inputList[i];
        //    int rand = UnityEngine.Random.Range(i,inputList.Count);
        //    inputList[i] = inputList[rand];
        //    inputList[rand] = temp;
        //}
        for (int i = 0; i < 7; ++i)
        {
            int index = UnityEngine.Random.Range(0, fullItemList.Count);
            scavengedItems.Add(fullItemList[index]);
        }
        //code that should work for randomly selecting 6 "scavenged" items from the full list, but seems to cause an infinite loop?
        //for (int i = 0; i < 7; ++i)
        //{
        //    int index = UnityEngine.Random.Range(0, fullItemList.Count);
        //    if (i > 0)
        //    {
        //        for (int j = 0; j < scavengedItems.Count; ++j)
        //        {
        //            if (fullItemList[index].name == scavengedItems[j].name)
        //            {
        //                itemExists = true;
        //            }
        //        }
        //        if (itemExists)
        //        {
        //            i--;
        //        }
        //        else
        //        {
        //            scavengedItems.Add(fullItemList[index]);
        //        }
        //    }
        //    else
        //    {
        //        scavengedItems.Add(fullItemList[index]);
        //    }
        //    itemExists = false;
        //}
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

    private void UpdateChosenSprites(int index){
        chosenItemsprites[chosenIndexStart].sprite = GetSprite(index);
        chosenIndexStart++;
    }

    public void RemoveLastItem()
    {
        for(int i = 0; i < scavengedItems.Count; ++i)
        {
            if (!reactivate)
            {
                if (chosenInventory[chosenIndexStart - 1].name == scavengedItems[i].name && selectionButtons[i].interactable == false)
                {
                    selectionButtons[i].interactable = true;
                    reactivate = true;
                }
            }
        }
        chosenInventory.RemoveAt(chosenInventory.Count - 1);
        chosenItemsprites[chosenIndexStart - 1].sprite = null;
        chosenIndexStart--;
        reactivate = false;
    }
}
