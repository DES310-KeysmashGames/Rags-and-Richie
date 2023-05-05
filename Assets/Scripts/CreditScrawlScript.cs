using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScrawlScript : MonoBehaviour
{
    [SerializeField] private Image credits;
    [SerializeField] float timer;
    public AK.Wwise.Event creditEvent;

    // Start is called before the first frame update
    void Start()
    {
        timer = 19.0f;
        creditEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        credits.transform.position = new Vector3(credits.transform.position.x, (float)((double)credits.transform.position.y + 0.5), credits.transform.position.z);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            creditEvent.Stop(gameObject);
            Loader.Load(Loader.Scene.MainMenuScene);
        }
    }
}
