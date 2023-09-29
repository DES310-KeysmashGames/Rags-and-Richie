using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TypeWriterTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;

    [SerializeField] private int characterIndex;
    private Coroutine textCoroutine;

    private WaitForSeconds charDelay;
    private WaitForSeconds puncDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float textSpeed = 80;
    [SerializeField] private float textDelay = 0.1f;

    //Event stuff
    private WaitForSeconds textboxFullEventDelay;
    [SerializeField] [Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;

    private void Awake()
    {
        charDelay = new WaitForSeconds(1 / textSpeed);
        puncDelay = new WaitForSeconds(textDelay);
        textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textBox.maxVisibleCharacters != textBox.textInfo.characterCount - 1)
            {
                Skip();
            }
        }
    }

    public void SetText(string text)
    {
        if (textCoroutine != null)
        {
            StopCoroutine(textCoroutine);
        }
        textBox.text = text;
        textBox.maxVisibleCharacters = 0;
        characterIndex = 0;

        textCoroutine = StartCoroutine(TypeWriter());
    }

    private IEnumerator TypeWriter()
    {
        TMP_TextInfo textInfo = textBox.textInfo;

        while (characterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount + 1;

            if (characterIndex == lastCharacterIndex)
            {
                ++textBox.maxVisibleCharacters;
                yield return textboxFullEventDelay;
                CompleteTextRevealed?.Invoke();
                yield break;
            }



            char character = textInfo.characterInfo[characterIndex].character;
            ++textBox.maxVisibleCharacters;

            if (character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-')
            {
                yield return puncDelay;
            }
            else
            {
                yield return charDelay;
            }

            CharacterRevealed?.Invoke(character);
            ++characterIndex;
        }
    }

    public void Skip()
    {
        Debug.Log("Skip called");

        StopCoroutine(textCoroutine);
        textBox.maxVisibleCharacters = textBox.textInfo.characterCount;

        CompleteTextRevealed?.Invoke();
    }
}
