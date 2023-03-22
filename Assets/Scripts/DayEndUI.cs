using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEndUI : MonoBehaviour
{
    [SerializeField] public List<BaseItem> soldItemsReviewList = new List<BaseItem>();
    [SerializeField] private Image[] soldItemSprites;

    [SerializeField] Button endButton;


    private void Awake(){
        endButton.onClick.AddListener(()=> {
            Loader.Load(Loader.Scene.EndingScene);
        });
    }

    private void Start(){
        for (int i=0; i < StaticInventory.soldItemsList.Count; i++){
            soldItemsReviewList.Add(StaticInventory.soldItemsList[i]);
        }
        AssignSprites();
    }

    private Sprite GetSprite(int i)
    {
        //return CurrentItems[itemNo].frontSprite;
        return soldItemsReviewList[i].frontSprite;
    }
    private void AssignSprites(){
        for (int i = 0; i < soldItemSprites.Length; ++i)
        {
            soldItemSprites[i].enabled = true;
            soldItemSprites[i].sprite = GetSprite(i);
        }
    }
}
