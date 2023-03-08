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
        });
        //set all images to disabled
        for (int i =0; i < endScreens.Length; i++){
                endScreens[i].enabled = false;
        }
    }

    private void Start(){
        //display bad ending
        //if (value<10){
        //    endScreens[0].enabled = true;
        //}
        ////display neutral ending

        //if (value >= 10 & value <50)
        //{
        //    endScreens[1].enabled = true;
        //}
        ////display good ending
        //if (value >= 50){
        //    endScreens[2].enabled = true;
        //}
        value = PlayerPrefs.GetInt("wallet");
        endScreens[value].enabled = true;
    }
    
    public void SetScene(int sceneNo)
    {
        endScreens[sceneNo].enabled = true;
    }
}
