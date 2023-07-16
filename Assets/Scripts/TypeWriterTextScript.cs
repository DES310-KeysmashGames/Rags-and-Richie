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

        skipDelay = new WaitForSeconds(1 / (textSpeed * skipSpeedup));
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(textBox.maxVisibleCharacters != textBox.textInfo.characterCount-1)
            {
                Skip();
            }
        }
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

            if(!skipping && (character == '?' || character == '.' || character == ',' || character == ':' || character == ';' || character == '!' || character == '-'))
            {
                yield return puncDelay;
            }
            else
            {
                yield return skipping ? skipDelay : charDelay;
            }

            ++characterIndex;
        }
    }

    public void Skip()
    {
        if(skipping)
        {
            return;
        }

        skipping = true;

        if(!quickSkip)
        {
            StartCoroutine(SkipSpeedupReset());
            return;
        }

        StopCoroutine(textCoroutine);
        textBox.maxVisibleCharacters = textBox.textInfo.characterCount;
    }

    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => textBox.maxVisibleCharacters == textBox.textInfo.characterCount - 1);
        skipping = false;
    }
}
