using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject optionsCanvas;
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;
    public AudioMixer audioMixer;

    private void Awake(){
        //optionsCanvas = GetComponent<Canvas>();
       // optionsCanvas.enabled = false;
       optionsCanvas.gameObject.SetActive(false);

        playButton.onClick.AddListener(()=> {
            //click action
            Loader.Load(Loader.Scene.TravelScene);
            PlayerPrefs.SetInt("wallet", 0);
            StaticTravel.dayCount = 1;
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
        audioMixer.SetFloat("mixVolume", SFXvolume);
    }

    public void SetMusicVolume(float musicVolume){
        Debug.Log("music volume = " + musicVolume);
    }

}
