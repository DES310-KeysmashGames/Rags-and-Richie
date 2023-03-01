using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;

    private void Awake(){
        //optionsCanvas = GetComponent<Canvas>();
       // optionsCanvas.enabled = false;
       optionsCanvas.gameObject.SetActive(false);

        playButton.onClick.AddListener(()=> {
            //click action
            Loader.Load(Loader.Scene.ItemSelectScene);
        });
        optionsButton.onClick.AddListener(()=> {
            //click action

        });

        quitButton.onClick.AddListener(()=> {
            //click action
            Application.Quit();
        });
    }

    public void SetSFXVolume(float SFXvolume){
        Debug.Log("sfx volume = " + SFXvolume);
    }

    public void SetMusicVolume(float musicVolume){
        Debug.Log("music volume = " + musicVolume);
    }

}
