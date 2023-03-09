using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public enum IntroCategory
{
    Food, Drink, Machinery, Luxury, Warmth, Weapon, Trade, None
}

[CreateAssetMenu(menuName = "Dialogue System/Dialogue", fileName = "New Dialogue")]
public class TextDialogue : ScriptableObject
{
    [TextArea][SerializeField] public string lineOfDialogue;
    [SerializeField] public IntroCategory category;

}

public class Text : MonoBehaviour
{
    TextDialogue[] allText;
    private void Start()
    {
        allText = (TextDialogue[])Resources.FindObjectsOfTypeAll(typeof(TextDialogue));
        Debug.Log(allText[0]);
    }

    public ScriptableObject[] GetArray()
    {
        return allText;
    }
}