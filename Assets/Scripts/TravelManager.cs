using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TravelManager : MonoBehaviour
{
    //Button for next day
    [SerializeField] Button nextButton;

    private void Awake()
    {
        //Go to Item Selection scene on Click
        nextButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.ItemSelectScene);
        });
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
