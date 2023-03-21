using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] BaseCharacter[] character;
    [SerializeField] private List<BaseCharacter> prevCustomer = new List<BaseCharacter>();
    public BaseCharacter currentChar;
    private bool custExists;
    [SerializeField] TextDialogue tradeSpeech;
    [SerializeField] TextDialogue[] inTradeText;
    [SerializeField] TextDialogue[] acceptTrade;
    [SerializeField] TextDialogue[] declineTrade;
    [SerializeField] TextDialogue[] zeroPatience;
    private int introTextNo;
    
    //generates a random customer from the available list of possible customers, with an intro text.
    public void GenerateCustomer()
    {
        int index = Random.Range(0, character.Length);
        //checks to see if the customer has already visited today.
        for (int i = 0; i < prevCustomer.Count; i++)
        {
            if (character[index].name == prevCustomer[i].name)
            {
                custExists = true;
            }
        }
        if (!custExists)
        {
            currentChar = character[index];
            for(int j = 0; j < character[index].introText.Count; j++)
            {
                currentChar.introText[j] = character[index].introText[j];
            }
            custExists = false;
        }
        else
        {
            custExists = false;
            GenerateCustomer();
        }
    }

    public void SaleOver()
    {
        prevCustomer.Add(currentChar);
        currentChar = null;
    }
    
    public string GetIntro(int introNo)
    {
        switch(introTextNo)
        {
            case 0:
                return currentChar.introText[introNo].lineOfDialogue;
            case 1:
                return currentChar.introText2[introNo].lineOfDialogue;
            case 2:
                return currentChar.introText3[introNo].lineOfDialogue;
            default:
                return currentChar.introText[introNo].lineOfDialogue;
        }    
    }

    public Sprite GetSprite()
    {
        return currentChar.charSprite;
    }

    public string GetCustName()
    {
        return currentChar.CustName;
    }

    public string TradeSpeech()
    {
        return tradeSpeech.lineOfDialogue;
    }

    public int GetIntroLength()
    {
        introTextNo = Random.Range(0, 2);
        switch(introTextNo)
        {
            case 0:
                return currentChar.introText.Count;
            case 1:
                return currentChar.introText2.Count;
            case 2:
                return currentChar.introText3.Count;
            default:
                return currentChar.introText.Count;
        }
    }

    public string GenerateTradeText(int no)
    {
        return inTradeText[no].lineOfDialogue;
    }

    public int GetFood()
    {
        return currentChar.foodDesire;
    }

    public int GetDrink()
    {
        return currentChar.drinkDesire;
    }

    public int GetWarmth()
    {
        return currentChar.warmthDesire;
    }

    public int GetLuxury()
    {
        return currentChar.luxuryDesire;
    }

    public int GetMachinery()
    {
        return currentChar.machineryDesire;
    }

    public int GetWeapon()
    {
        return currentChar.weaponDesire;
    }

    public int GetPatience()
    {
        return currentChar.patiece;
    }

    public int GetDesperation()
    {
        return currentChar.desperation;
    }

    public string NoPatience(int no)
    {
        return zeroPatience[no].lineOfDialogue;
    }

    public string AcceptDeal(int no)
    {
        return acceptTrade[no].lineOfDialogue;
    }

    public string DeclineDeal(int no)
    {
        return declineTrade[no].lineOfDialogue;
    }

    public void Reset()
    {
        for(int i = 0; i < prevCustomer.Count; ++i)
        {
            prevCustomer.RemoveAt(i);
        }
    }
}
