using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] Button finishButton;
    [SerializeField] Image[] endScreens;
    [SerializeField] private int value;
    


    private void Awake(){
        finishButton.onClick.AddListener(()=> {
            //click action
            Loader.Load(Loader.Scene.MainMenuScene);
            StaticTravel.dayCount = 2;
        });
        //set all images to disabled
        for (int i =0; i < endScreens.Length; i++){
                endScreens[i].enabled = false;
        }
    }

    private void Start(){
        value = PlayerPrefs.GetInt("wallet");
        //display bad ending
        if (value < 50)
        {
            endScreens[0].enabled = true;
        }
        //display neutral ending

        if (value >= 50 & value < 80)
        {
            endScreens[1].enabled = true;
        }
        //display good ending
        if (value >= 80)
        {
            endScreens[2].enabled = true;
        };
    }

    public void SetScene(int sceneNo)
    {
        endScreens[sceneNo].enabled = true;
    }
}
