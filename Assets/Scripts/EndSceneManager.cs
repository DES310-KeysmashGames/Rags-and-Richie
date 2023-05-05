using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] Button finishButton;
    [SerializeField] Image[] endScreens;
    [SerializeField] private int value;
    [SerializeField] private TextMeshProUGUI walletText;

    //audio
    public AK.Wwise.Event buttonEvent;

    private void Awake(){
        finishButton.onClick.AddListener(()=> {
            //click action
            buttonEvent.Post(gameObject);
            Loader.Load(Loader.Scene.CreditScene);
        });
        //set all images to disabled
        for (int i =0; i < endScreens.Length; i++){
                endScreens[i].enabled = false;
        }
    }

    private void Start(){
        value = PlayerPrefs.GetInt("wallet");
        //display bad ending
        if (value < 200)
        {
            endScreens[0].enabled = true;
        }
        //display neutral ending

        if (value >= 200 & value < 400)
        {
            endScreens[1].enabled = true;
        }
        //display good ending
        if (value >= 400)
        {
            endScreens[2].enabled = true;
        };
        walletText.text = value.ToString();
    }

    public void SetScene(int sceneNo)
    {
        endScreens[sceneNo].enabled = true;
    }
}
