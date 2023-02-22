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

    //generates a random customer from the available list of possible customers, with an intro text.
    public void GenerateCustomer()
    {
        int index = Random.Range(0, character.Length);
        //checks to see if the customer has already visited today.
        for (int i = 0; i < prevCustomer.Count; i++)
        {
            if (character[index] == prevCustomer[i])
            {
                custExists = true;
            }
        }
        if (!custExists)
        {
            currentChar = character[index];
            for(int j = 0; j < character[index].introText.Length; j++)
            {
                currentChar.introText[j] = character[index].introText[j];
            }
            custExists = false;
        }
    }

    public string GetIntro(int introNo)
    {
        return currentChar.introText[introNo].lineOfDialogue;
    }

    public Sprite GetSprite()
    {
        return currentChar.charSprite;
    }

    public string TradeSpeech()
    {
        return tradeSpeech.lineOfDialogue;
    }

    public int GetIntroLength()
    {
        return currentChar.introText.Length;
    }

    public string GenerateTradeText()
    {
        int index = Random.Range(0, inTradeText.Length);
        return inTradeText[index].lineOfDialogue;
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
}
