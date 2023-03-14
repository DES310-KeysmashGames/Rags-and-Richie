using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum CharacterDesire
{
    Food, Drink, Warmth, Luxury, Machinery, Weaponary
}

[CreateAssetMenu(menuName = "CharacterCreator/Character", fileName = "New Character")]
public class BaseCharacter : ScriptableObject
{
    [SerializeField] public Sprite charSprite;
    [SerializeField] public CharacterDesire desire;
    [SerializeField] public int foodDesire;
    [SerializeField] public int drinkDesire;
    [SerializeField] public int warmthDesire;
    [SerializeField] public int weaponDesire;
    [SerializeField] public int machineryDesire;
    [SerializeField] public int luxuryDesire;
    [SerializeField] public List<TextDialogue> introText = new List<TextDialogue>();
    [SerializeField] public int patiece;
    [SerializeField] public int desperation;
}