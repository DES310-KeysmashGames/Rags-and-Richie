using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayEndUI : MonoBehaviour
{
    [SerializeField] public List<BaseItem> soldItemsReviewList = new List<BaseItem>();
    public List<int> soldPrice = new List<int>();
    public List<int> price = new List<int>();
    [SerializeField] private Image[] soldItemSprites;
    [SerializeField] private Image[] thumbSprites;
    [SerializeField] private Sprite[] thumb;
    [SerializeField] private TextMeshProUGUI expenses;

    [SerializeField] Button endButton;

    //Audio
    public AK.Wwise.Event buttonEvent;

    private void Awake(){
        if (StaticTravel.dayCount < 3)
        {
            //Continue to next day
            endButton.onClick.AddListener(() =>
            {
                buttonEvent.Post(gameObject);
                Loader.Load(Loader.Scene.TravelScene);
                StaticTravel.dayCount++;
                ClearItems();
            });
        }
        else
        {
            //Show end screen
            endButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.EndingScene);
                ClearItems();
            });
        }

        PlayerPrefs.SetInt("wallet", (PlayerPrefs.GetInt("wallet") - StaticTravel.expenses));
        if (PlayerPrefs.GetInt("wallet") < 0)
        {
            Loader.Load(Loader.Scene.EndingScene);
        }
    }

    private void Start(){
        for (int i=0; i < StaticInventory.soldItemsList.Count; i++){
            soldItemsReviewList.Add(StaticInventory.soldItemsList[i]);
            soldPrice.Add(StaticInventory.sellPrice[i]);
            price.Add(StaticInventory.basePrice[i]);
        }
        AssignSprites();
        AssignThumb();
        expenses.text = StaticTravel.expenses.ToString();
    }

    private Sprite GetSprite(int i)
    {
        return soldItemsReviewList[i].frontSprite;
    }
    private void AssignSprites(){
        for (int i = 0; i < soldItemSprites.Length; ++i)
        {
            soldItemSprites[i].enabled = true;
            soldItemSprites[i].sprite = GetSprite(i);
        }
    }

    private void AssignThumb()
    {
        for (int i =0; i < soldItemSprites.Length; ++i)
        {
            if(soldPrice[i] < price[i])
            {
                thumbSprites[i].sprite = thumb[0];
            }
            else
            {
                thumbSprites[i].sprite = thumb[1];
            }
        }
    }

    private void ClearItems()
    {
        StaticInventory.soldItemsList.Clear();
        StaticInventory.sellPrice.Clear();
        StaticInventory.basePrice.Clear();
    }
}
