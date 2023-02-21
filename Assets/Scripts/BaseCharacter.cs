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
    [SerializeField] public TextDialogue[] introText;
    [SerializeField] public float patiece;
}

public class Script : MonoBehaviour
{
    BaseCharacter[] allChars;
    private void Start()
    {
        allChars = (BaseCharacter[])Resources.FindObjectsOfTypeAll(typeof(BaseCharacter));
        Debug.Log(allChars[0]);
    }

    public ScriptableObject[] getArray()
    {
        return allChars;
    }
}
