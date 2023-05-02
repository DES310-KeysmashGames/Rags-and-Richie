using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimateText : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] [TextArea] private string[] textToScroll;
    [SerializeField] private float textSpeed;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textBox;
    private int currentTextIndex = 0;

    public void GetText()
    {
        textToScroll[currentTextIndex] = textBox.text;
    }

    public void ActivateText()
    {
        //start coroutine
        StartCoroutine(AnimatedText());
    }

    IEnumerator AnimatedText()
    {
        for (int i = 0; i < textToScroll[currentTextIndex].Length + 1; ++i)
        {
            textBox.text = textToScroll[currentTextIndex].Substring(0,i);
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
