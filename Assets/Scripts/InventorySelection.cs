using System;
//using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;

public class InventorySelection : MonoBehaviour
{
    //list for items to be selected from
    //scavanged items
    [SerializeField] private List<Button> selectionButtons = new List<Button>();
    [SerializeField] private List<BaseItem> fullItemList = new List<BaseItem> ();
    [SerializeField] public List<BaseItem> scavengedItems = new List<BaseItem>();
    [SerializeField] private Button[] removeButtons;
    [SerializeField] private Button[] scavengedItemSprites;
    [SerializeField] private Sprite[] itemTagSprites;

    //list for items to be selected into
    //chosen items
    [SerializeField] public List<BaseItem> chosenInventory = new List<BaseItem>();
    [SerializeField] private Image[] chosenItemsprites;
    private bool reactivate;
    private int chosenIndexStart = 0;

    private int day;
    private bool lunchboxPicked;

    [SerializeField] private Image itemCard;
    [SerializeField] private Image itemCard2;
    [SerializeField] private Image itemOfTheDayImage;
    [SerializeField] private Image itemTrackerImage;
    [SerializeField] private TextMeshProUGUI[] itemTypeCount;
    [SerializeField] private Sprite[] continueSprite;
    [SerializeField] private int[] itemCount;
    [SerializeField] private string itemOfDay;

    //buttons
    [SerializeField] Button confirmButton;

    public AK.Wwise.Event buttonPressEvent;
    public AK.Wwise.Event confirmEvent;

    private void Awake(){
        confirmButton.onClick.AddListener(()=> {
            //Confirm Selection Button Audio
            
            //click action
            if(chosenInventory.Count == 4){
                for( int i =0; i < chosenInventory.Count; i++){
                    StaticInventory.intermediateList.Add(chosenInventory[i]);
                }
                Loader.Load(Loader.Scene.TradeScene);
                confirmEvent.Post(gameObject);
            }
        });

        itemCard.enabled = false;
        itemCard2.enabled = false;
        itemOfDay = StaticTravel.itemOfTheDay;

        for(int i = 0; i < chosenItemsprites.Length; ++i)
        {
            chosenItemsprites[i].enabled = false;
        }

        //Allow players to de-select/remove chosen items 
        for (int i = 0; i < removeButtons.Length; i++)
        {
            // Store the current index in a local variable for use in the listener
            int index = i; 

            removeButtons[i].onClick.AddListener(() =>
            {
                // Don't allow deselection of first item on day 1
                if (day == 1 && index == 0)
                {
                    //Potentially add some Richie Flavour text to tell player they can't remove that item
                }
                else
                {
                    switch (index)
                    {
                        case 0:
                            RemoveItemOne();
                            break;
                        case 1:
                            RemoveItemTwo();
                            break;
                        case 2:
                            RemoveItemThree();
                            break;
                        case 3:
                            RemoveItemFour();
                            break;
                    }
                }
            });
        }
    }

    private void Start(){

        //Get the day
        day = StaticTravel.dayCount;

        //Makes sure all items can be selected if it's not day 1
        if (day == 1)
        {
            lunchboxPicked = false;
        } else
        {
            lunchboxPicked = true;
        }

        //Run Tutorial item set on day one, else shuffle as regular
        if (day == 1)
        {
            TutorialShuffle();
        }
        else
        {
            ShuffleItems();
        }

        AssignSprites();

        //Set all removeButtons to Non-Active at start
        for (int j = 0; j < removeButtons.Length; ++j)
        {
            removeButtons[j].gameObject.SetActive(false);
        }

        for (int i = 0; i < selectionButtons.Count; i++)
        {
            int closureIndex = i; // Prevents the closure problem

            selectionButtons[closureIndex].onClick.AddListener(() => {
                TaskOnClick(closureIndex);
            });
        }

        //Pick a category to be the Item of the Day
        switch (itemOfDay)
        {
            case "Weapon":
                itemOfTheDayImage.sprite = itemTagSprites[0];
                break;
            case "Warmth":
                itemOfTheDayImage.sprite = itemTagSprites[1];
                break;
            case "Mechanical":
                itemOfTheDayImage.sprite = itemTagSprites[2];
                break;
            case "Food":
                itemOfTheDayImage.sprite = itemTagSprites[3];
                break;
            case "Drink":
                itemOfTheDayImage.sprite = itemTagSprites[4];
                break;
            case "Luxury":
                itemOfTheDayImage.sprite = itemTagSprites[5];
                break;
            case "Mystery":
                itemOfTheDayImage.sprite = itemTagSprites[6];
                break;
        }
    }

    private void Update(){
        if (chosenInventory.Count !=4){
            confirmButton.GetComponent<Image>().sprite = continueSprite[0];
        }else{
            confirmButton.GetComponent<Image>().sprite = continueSprite[1];
        }

        //Check if lunchbox has been selected
        if (day == 1 && !lunchboxPicked)
        {
            //Disable all buttons except Lunchbox
            for (int i = 1; i < selectionButtons.Count; i++)
            {
                selectionButtons[i].interactable = false;
            }

            //If Lunchbox has been selected then re-enable all other items
            if (removeButtons[0].gameObject.activeSelf)
            {
                for (int i = 1; i < selectionButtons.Count; i++)
                {
                    selectionButtons[i].interactable = true;
                }

                lunchboxPicked = true;
            }
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

            //Item Selection Button Audio
        }
        else
        {
            Debug.Log("Inventory is full");
        }
        UpdateChosenSprites(buttonIndex);

        //Set removeButtons to activate based on how many items are chosen
        for (int i = 0; i < chosenInventory.Count; ++i)
        {
            removeButtons[i].gameObject.SetActive(true);
        }

        UpdateItemTypeCount(buttonIndex);
        buttonPressEvent.Post(gameObject);
    }

    void ShuffleItems()
    {
        for (int i = 0; i < 6; ++i)
        {
            int index = UnityEngine.Random.Range(0, fullItemList.Count);
            scavengedItems.Add(fullItemList[index]);
        }
    }

    void TutorialShuffle()
    {
        scavengedItems.Add(fullItemList[7]); // Set "Mom's Lunchbox" as the first item

        // Create a list of available indices to prevent rerolls on the same index
        List<int> availableIndices = new List<int>();

        //Iterate through full item list to check for food items
        for (int i = 0; i < fullItemList.Count; i++)
        {
            //Add to index of available items if item ISN'T food
            if (i != 7 && !IsItemFood(fullItemList[i]))
            {
                availableIndices.Add(i);
            }
        }

        //IF WE DON'T WANT DUPLICATE ITEMS, UNCOMMENT THAT LINE BELOW
        // Randomly select 5 items from the available non-food items
        for (int i = 0; i < 5 && availableIndices.Count > 0; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int itemIndex = availableIndices[randomIndex];
            scavengedItems.Add(fullItemList[itemIndex]);
            //availableIndices.RemoveAt(randomIndex);
        }
    }

    //Check if item is tagged as Food, and return true/false
    bool IsItemFood(BaseItem item)
    {
        return item.primaryType == ItemType.Food || item.secondaryType == ItemType.Food;
    }

    public Sprite GetSprite(int i)
    {
        //return CurrentItems[itemNo].frontSprite;
        return scavengedItems[i].frontSprite;
    }
    private void AssignSprites(){
        for (int i = 0; i < scavengedItemSprites.Length; ++i)
        {
            scavengedItemSprites[i].gameObject.SetActive(true);
            scavengedItemSprites[i].image.sprite = GetSprite(i);
        }
    }

    private void UpdateChosenSprites(int index){
        chosenItemsprites[chosenIndexStart].sprite = GetSprite(index);
        chosenItemsprites[chosenIndexStart].enabled = true;
        chosenIndexStart++;
    }

    private void UpdateItemTypeCount(int buttonIndex)
    {
        string primary = scavengedItems[buttonIndex].primaryType.ToString();
        string secondary = scavengedItems[buttonIndex].secondaryType.ToString();
        string tertiary = scavengedItems[buttonIndex].tertiaryType.ToString();
        switch (primary)
        {
            case "Weapon":
                ++itemCount[0];
                break;
            case "Warmth":
                ++itemCount[1];
                break;
            case "Machinery":
                ++itemCount[2];
                break;
            case "Food":
                ++itemCount[3];
                break;
            case "Drink":
                ++itemCount[4];
                break;
            case "Luxury":
                ++itemCount[5];
                break;
        }
        switch (secondary)
        {
            case "Weapon":
                ++itemCount[0];
                break;
            case "Warmth":
                ++itemCount[1];
                break;
            case "Machinery":
                ++itemCount[2];
                break;
            case "Food":
                ++itemCount[3];
                break;
            case "Drink":
                ++itemCount[4];
                break;
            case "Luxury":
                ++itemCount[5];
                break;
        }
        switch (tertiary)
        {
            case "Weapon":
                ++itemCount[0];
                break;
            case "Warmth":
                ++itemCount[1];
                break;
            case "Machinery":
                ++itemCount[2];
                break;
            case "Food":
                ++itemCount[3];
                break;
            case "Drink":
                ++itemCount[4];
                break;
            case "Luxury":
                ++itemCount[5];
                break;
            case "Mystery":
                break;
        }
        for (int i =0; i < itemTypeCount.Length; ++i)
        {
            itemTypeCount[i].text = itemCount[i].ToString();
        }
    }

    private void removedItemCountUpdate(int no)
    {
        string primary = chosenInventory[no].primaryType.ToString();
        string secondary = chosenInventory[no].secondaryType.ToString();
        string tertiary = chosenInventory[no].tertiaryType.ToString();
        switch (primary)
        {
            case "Weapon":
                --itemCount[0];
                break;
            case "Warmth":
                --itemCount[1];
                break;
            case "Machinery":
                --itemCount[2];
                break;
            case "Food":
                --itemCount[3];
                break;
            case "Drink":
                --itemCount[4];
                break;
            case "Luxury":
                --itemCount[5];
                break;
        }
        switch (secondary)
        {
            case "Weapon":
                --itemCount[0];
                break;
            case "Warmth":
                --itemCount[1];
                break;
            case "Machinery":
                --itemCount[2];
                break;
            case "Food":
                --itemCount[3];
                break;
            case "Drink":
                --itemCount[4];
                break;
            case "Luxury":
                --itemCount[5];
                break;
        }
        switch (tertiary)
        {
            case "Weapon":
                --itemCount[0];
                break;
            case "Warmth":
                --itemCount[1];
                break;
            case "Machinery":
                --itemCount[2];
                break;
            case "Food":
                --itemCount[3];
                break;
            case "Drink":
                --itemCount[4];
                break;
            case "Luxury":
                --itemCount[5];
                break;
            case "Mystery":
                break;
        }
        for (int i = 0; i < itemTypeCount.Length; ++i)
        {
            itemTypeCount[i].text = itemCount[i].ToString();
        }
    }

    public void RemoveItemOne()
    {
        removedItemCountUpdate(0);
        Debug.Log("Button One is pressed");
        for(int i = 0; i < scavengedItems.Count; ++i)
        {
            if (!reactivate)
            {
                if (chosenInventory[0].name == scavengedItems[i].name && selectionButtons[i].interactable == false)
                {
                    selectionButtons[i].interactable = true;
                    reactivate = true;
                }
            }
        }
        chosenInventory.RemoveAt(0);
        for(int i = 0; i < chosenInventory.Count; ++i)
        {
            chosenItemsprites[i].sprite = chosenInventory[i].frontSprite;
        }
        chosenItemsprites[chosenInventory.Count].sprite = null;
        chosenItemsprites[chosenInventory.Count].enabled = false;
        removeButtons[chosenInventory.Count].gameObject.SetActive(false);
        chosenIndexStart--;
        reactivate = false;

        //Item Deselection Button Audio
        buttonPressEvent.Post(gameObject);
    }

    public void RemoveItemTwo()
    {
        removedItemCountUpdate(1);
        Debug.Log("Button Two is pressed");
        for (int i = 0; i < scavengedItems.Count; ++i)
        {
            if (!reactivate)
            {
                if (chosenInventory[1].name == scavengedItems[i].name && selectionButtons[i].interactable == false)
                {
                    selectionButtons[i].interactable = true;
                    reactivate = true;
                }
            }
        }
        chosenInventory.RemoveAt(1);
        for (int i = 0; i < chosenInventory.Count; ++i)
        {
            chosenItemsprites[i].sprite = chosenInventory[i].frontSprite;
        }
        chosenItemsprites[chosenInventory.Count].sprite = null;
        chosenItemsprites[chosenInventory.Count].enabled = false;
        removeButtons[chosenInventory.Count].gameObject.SetActive(false);
        chosenIndexStart--;
        reactivate = false;

        //Item Deselection Button Audio
        buttonPressEvent.Post(gameObject);
    }

    public void RemoveItemThree()
    {
        removedItemCountUpdate(2);
        Debug.Log("Button Three is pressed");
        for (int i = 0; i < scavengedItems.Count; ++i)
        {
            if (!reactivate)
            {
                if (chosenInventory[2].name == scavengedItems[i].name && selectionButtons[i].interactable == false)
                {
                    selectionButtons[i].interactable = true;
                    reactivate = true;
                }
            }
        }
        chosenInventory.RemoveAt(2);
        for (int i = 0; i < chosenInventory.Count; ++i)
        {
            chosenItemsprites[i].sprite = chosenInventory[i].frontSprite;
        }
        chosenItemsprites[chosenInventory.Count].sprite = null;
        chosenItemsprites[chosenInventory.Count].enabled = false;
        removeButtons[chosenInventory.Count].gameObject.SetActive(false);
        chosenIndexStart--;
        reactivate = false;

        //Item Deselection Button Audio
        buttonPressEvent.Post(gameObject);
    }

    public void RemoveItemFour()
    {
        removedItemCountUpdate(3);
        Debug.Log("Button Four is pressed");
        for (int i = 0; i < scavengedItems.Count; ++i)
        {
            if (!reactivate)
            {
                if (chosenInventory[3].name == scavengedItems[i].name && selectionButtons[i].interactable == false)
                {
                    selectionButtons[i].interactable = true;
                    reactivate = true;
                }
            }
        }
        chosenInventory.RemoveAt(3);
        for (int i = 0; i < chosenInventory.Count; ++i)
        {
            chosenItemsprites[i].sprite = chosenInventory[i].frontSprite;
        }
        chosenItemsprites[chosenInventory.Count].sprite = null;
        chosenItemsprites[chosenInventory.Count].enabled = false;
        removeButtons[chosenInventory.Count].gameObject.SetActive(false);
        chosenIndexStart--;
        reactivate = false;

        //Item Deselection Button Audio
        buttonPressEvent.Post(gameObject);
    }

    public void HoverEnterButtonOne()
    {
        itemCard2.enabled = true;
        itemCard2.sprite = scavengedItems[0].itemDescription;
    }
    public void HoverEnterButtonTwo()
    {
        itemCard2.enabled = true;
        itemCard2.sprite = scavengedItems[1].itemDescription;
    }
    public void HoverEnterButtonThree()
    {
        itemCard2.enabled = true;
        itemCard2.sprite = scavengedItems[2].itemDescription;
    }
    public void HoverEnterButtonFour()
    {
        itemCard.enabled = true;
        itemCard.sprite = scavengedItems[3].itemDescription;
    }
    public void HoverEnterButtonFive()
    {
        itemCard.enabled = true;
        itemCard.sprite = scavengedItems[4].itemDescription;
    }
    public void HoverEnterButtonSix()
    {
        itemCard.enabled = true;
        itemCard.sprite = scavengedItems[5].itemDescription;
    }
    public void HoverExitButton()
    {
        itemCard.enabled = false;
        itemCard2.enabled = false;
    }
}

