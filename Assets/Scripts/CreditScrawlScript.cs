using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScrawlScript : MonoBehaviour
{
    [SerializeField] private Image credits;
    public AK.Wwise.Event creditEvent;
    [SerializeField] private float posValue;
    [SerializeField] private Button skipCredits;

    private void Awake()
    {
        skipCredits.onClick.AddListener(() => 
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }

    // Start is called before the first frame update
    void Start()
    {
        creditEvent.Post(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        credits.transform.position = new Vector3(credits.transform.position.x, (float)((double)credits.transform.position.y + 2.0f), credits.transform.position.z);
        posValue = credits.transform.position.y;
  
        if(posValue >= 800)
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        }
    }
}
