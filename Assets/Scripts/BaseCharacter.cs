using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum CharacterPersonality
{
    happy, sad, calm, mysterious, angry, neutral, excited, timid, scared
}

[CreateAssetMenu(menuName = "CharacterCreator/Character", fileName = "New Character")]
public class BaseCharacter : ScriptableObject
{
    [SerializeField] public Sprite charSprite;
    [SerializeField] public CharacterPersonality personality;
    [SerializeField] public int foodDesire;
    [SerializeField] public int drinkDesire;
    [SerializeField] public int weaponDesire;
    [SerializeField] public int machineryDesire;
    [SerializeField] public int luxuryDesire;
    [SerializeField] public TextDialogue introText { get; set; }
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
