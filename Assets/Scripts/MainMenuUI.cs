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
    public AK.Wwise.Event playambientMusic;
    private bool playing;

    private void Awake(){
        //optionsCanvas = GetComponent<Canvas>();
       // optionsCanvas.enabled = false;
       optionsCanvas.gameObject.SetActive(false);

        playButton.onClick.AddListener(()=> {
            //click action
            Loader.Load(Loader.Scene.TravelScene);
            PlayerPrefs.SetInt("wallet", 0);
            StaticTravel.dayCount = 1;

            //Main Menu Button Audio

        });
        optionsButton.onClick.AddListener(()=> {
            //click action

            //Options Button Audio

        });

        quitButton.onClick.AddListener(()=> {
            //Main Menu Button Audio

            //click action
            Application.Quit();
        });
        //playambientMusic.Post(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playing = !playing;
        }

        if (playing)
        {
            playambientMusic.Post(gameObject);
        }
        else
        {
            {
                playambientMusic.Stop(gameObject);
            }
        }
    }

    //Main Menu Background Audio

    //Basic Audio by Leslie
    public void SetSFXVolume(float SFXvolume){
        Debug.Log("sfx volume = " + SFXvolume);
        audioMixer.SetFloat("mixVolume", SFXvolume);
    }

    public void SetMusicVolume(float musicVolume){
        Debug.Log("music volume = " + musicVolume);
    }
}
