using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScreenScript : MonoBehaviour
{

    // the image you want to fade, assign in inspector
    public Image img;
    [SerializeField] private bool fadeAway;
    [SerializeField] float timer;
    [SerializeField] private bool transition;
    public AK.Wwise.Event bgmEvent;

    public void Start()
    {
        timer = 3.0f;
        fadeAway = true;
        StartCoroutine(FadeImage());
        bgmEvent.Post(gameObject);
    }

    public void Update()
    {
        timer -= Time.deltaTime;
        if (transition)
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        }
        else if (timer <= 0 && fadeAway == true)
        {
            fadeAway = false;
            timer = 2.0f;
            StartCoroutine(FadeImage());
        }
        else if (timer <= 0 && fadeAway == false)
        {
            transition = true;
        }
        
    }

    IEnumerator FadeImage()
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}