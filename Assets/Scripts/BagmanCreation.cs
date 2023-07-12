using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

[CreateAssetMenu(menuName = "MysteryCharacter/Bagman", fileName = "New Character")]

public class BagmanCreation : ScriptableObject
{
    public Sprite charSprite;
    public string charName;
    public List<TextDialogue> introText = new List<TextDialogue>();
}
