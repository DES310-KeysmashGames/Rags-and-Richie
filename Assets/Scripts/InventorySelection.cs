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
    [SerializeField] public List<BaseItem> scavengedItems = new List<BaseItem>();
    [SerializeField] private Image[] scavengedItemSprites;
    //list for items to be selected into
    //chosen items
    [SerializeField] public List<BaseItem> chosenInventory = new List<BaseItem>();
    [SerializeField] private Image[] chosenItemsprites;

    //buttons
    [SerializeField] Button confirmButton;

    private void Awake(){
        confirmButton.onClick.AddListener(()=> {
            //click action
            if(chosenInventory.Count == 4){
                Loader.Load(Loader.Scene.TradeScene);
            }
            
        });
    }
    private void Start(){
        ShuffleItems(scavengedItems);
        AssignSprites();
        for (int i = 0; i < selectionButtons.Count; i++){
        int closureIndex = i ; // Prevents the closure problem
        selectionButtons[closureIndex].onClick.AddListener( () => TaskOnClick( closureIndex ) );
        }
    }

    private void Update(){
  
    }

    private void TaskOnClick( int buttonIndex )
  {
      //Debug.Log("You have clicked the button #" + buttonIndex, selectionButtons[buttonIndex]);
      
      //if inventory is not full, add item
      if(chosenInventory.Count < 4){
        Debug.Log("This item is: " + scavengedItems[buttonIndex]);
        chosenInventory.Add(scavengedItems[buttonIndex]);
        Debug.Log("You have added "+chosenInventory.Count + " items to your inventory");
      }else{
        Debug.Log("Inventory is full");
      }
      

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
