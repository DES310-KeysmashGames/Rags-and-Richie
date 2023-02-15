using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] BaseCharacter[] character;
    private BaseCharacter currentChar;
    [SerializeField] TextDialogue[] intro;
    [SerializeField] TextDialogue tradeSpeech;
    [SerializeField] TextDialogue[] inTradeText;

    //generates a random customer from the available list of possible customers, with an intro text.
    public void generateCustomer()
    {
        int index = Random.Range(0, character.Length);
        currentChar = character[index];
        currentChar.introText = intro[index];
    }

    public string getIntro()
    {
        return currentChar.introText.lineOfDialogue;
    }

    public Sprite getSprite()
    {
        return currentChar.charSprite;
    }

    public string TradeSpeech()
    {
        return tradeSpeech.lineOfDialogue;
    }
    
    public string GenerateTradeText()
    {
        int index = Random.Range(0, inTradeText.Length);
        return inTradeText[index].lineOfDialogue;
    }
}
