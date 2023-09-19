using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum CharacterDesire
{
    Food, Drink, Warmth, Luxury, Machinery, Weaponry, Special
}

[CreateAssetMenu(menuName = "CharacterCreator/Character", fileName = "New Character")]
public class BaseCharacter : ScriptableObject
{
    public Sprite charSprite;
    public string CustName;
    public CharacterDesire desire;
    public int foodDesire;
    public int drinkDesire;
    public int warmthDesire;
    public int weaponDesire;
    public int machineryDesire;
    public int luxuryDesire;
    public int specialDesire;
    public List<TextDialogue> introText = new List<TextDialogue>();
    public List<TextDialogue> introText2 = new List<TextDialogue>();
    public List<TextDialogue> introText3 = new List<TextDialogue>();
    public int patiece;
    public int desperation;
}