using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterTextScript : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;

    [Header("Test String")]
    [SerializeField] private string testText;

    [SerializeField] private int characterIndex;
    private Coroutine textCoroutine;

    private WaitForSeconds charDelay;
    private WaitForSeconds puncDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float textSpeed = 20;
    [SerializeField] private float textDelay = 0.5f;

    //text skipping
    public bool skipping { get; private set; }
    private WaitForSeconds skipDelay;

    [Header("Skip Options")]
    [SerializeField] private bool quickSkip;
    [SerializeField] [Min(1)] private int skipSpeedup = 5;

    private void Awake()
    {
        charDelay = new WaitForSeconds(1 / textSpeed);
        puncDelay = new WaitForSeconds(textDelay);

        skipDelay = new WaitForSeconds(1 / textSpeed * skipSpeedup);
    }

    private void Start()
    {
        //SetText(testText);
    }

    public void SetText(string text)
    {
        if(textCoroutine != null)
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
            char character = textInfo.characterInfo[characterIndex].character;
            ++textBox.maxVisibleCharacters;

            if(character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-')
            {
                yield return puncDelay;
            }
            else
            {
                yield return charDelay;
            }

            ++characterIndex;
        }
    }
}
