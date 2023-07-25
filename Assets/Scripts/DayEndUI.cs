using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayEndUI : MonoBehaviour
{
    AnimationHelper helper;

    [SerializeField] public List<BaseItem> soldItemsReviewList = new List<BaseItem>();
    [SerializeField] public List<Sprite> charSpriteList = new List<Sprite>();
    public List<int> soldPrice = new List<int>();
    public List<int> price = new List<int>();
    [SerializeField] private Image[] soldItemSprites;
    [SerializeField] private Image[] thumbSprites;
    [SerializeField] private Sprite[] thumb;
    [SerializeField] private TextMeshProUGUI expenses;
    [SerializeField] private TextMeshProUGUI[] itemCost;

    [SerializeField] private Image dailyGoalBar;

    [SerializeField] Button endButton;
    [SerializeField] private float goal;
    [SerializeField] private int sellAmount;
    [SerializeField] private bool test;
    [SerializeField] private int wallet;
    [SerializeField] private TextMeshProUGUI sellPriceText;

    //Audio
    public AK.Wwise.Event buttonEvent;

    private void Awake(){
        helper = GetComponentInChildren<AnimationHelper>();
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
                PlayerPrefs.SetInt("wallet", wallet);
                Loader.Load(Loader.Scene.EndingScene);
                ClearItems();
            });
        }

        PlayerPrefs.SetInt("wallet", (PlayerPrefs.GetInt("wallet") - StaticTravel.expenses));
        //if (PlayerPrefs.GetInt("wallet") < 0)
    

        goal = StaticTravel.goal;
        test = false;
        wallet = PlayerPrefs.GetInt("wallet");
    }

    private void Start(){
        for (int i=0; i < StaticInventory.soldItemsList.Count; i++){
            soldItemsReviewList.Add(StaticInventory.soldItemsList[i]);
            soldPrice.Add(StaticInventory.sellPrice[i]);
            price.Add(StaticInventory.basePrice[i]);

           charSpriteList.Add(StaticInventory.charac[i].charSprite);
        }
        AssignSprites();
        AssignThumb(); 
        AssignPrice();
        expenses.text = StaticTravel.expenses.ToString();
        for (int i = 0; i < soldPrice.Count; ++i)
        {
            sellAmount += soldPrice[i];
        }
        sellPriceText.text = sellAmount.ToString();
        if (wallet <= 0)
        {
            dailyGoalBar.fillAmount = 0.0f;
        }
        else
        {
            dailyGoalBar.fillAmount = ((float)wallet / goal);
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    test = true;   
        //}
        if(helper.GetBool())
        {
            if (sellAmount > 0)
            {
                wallet += 1;
                dailyGoalBar.fillAmount = (wallet / goal);
                sellAmount -= 1;
            }
        }
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
        for (int i =0; i < charSpriteList.Count; ++i)
        {
            thumbSprites[i].sprite = charSpriteList[i];
        }
    }

    private void AssignPrice()
    {
        for ( int i = 0; i < itemCost.Length; ++i)
        {
            itemCost[i].text = soldPrice[i].ToString();
        }
    }


    private void ClearItems()
    {
        StaticInventory.soldItemsList.Clear();
        StaticInventory.sellPrice.Clear();
        StaticInventory.basePrice.Clear();
        StaticInventory.charac.Clear();
    }

    public void Barfill()
    {
        test = true;
    }
}
