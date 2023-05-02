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
    public AK.Wwise.Event buttonClickEvent;
    private bool playing;
    private bool playGame;
    private float timer;

    private void Awake(){
       optionsCanvas.gameObject.SetActive(false);

        playButton.onClick.AddListener(()=> {
            //click action
            buttonClickEvent.Post(gameObject);
            playGame = true;
            //Main Menu Button Audio

        });
        optionsButton.onClick.AddListener(()=> {
            //click action

            //Options Button Audio
            buttonClickEvent.Post(gameObject);
        });

        quitButton.onClick.AddListener(()=> {
            //Main Menu Button Audio
            buttonClickEvent.Post(gameObject);
            //click action

            Application.Quit();
        });
        playambientMusic.Post(gameObject);
        timer = 1.0f;
        playGame = false;
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

        if (playGame)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            Loader.Load(Loader.Scene.TravelScene);
            PlayerPrefs.SetInt("wallet", 0);
            StaticTravel.dayCount = 1;
        }

    }

    //Main Menu Background Audio

    ////Basic Audio by Leslie
    public void SetSFXVolume(float SFXvolume){
        Debug.Log("sfx volume = " + SFXvolume);
        audioMixer.SetFloat("mixVolume", SFXvolume);
    }

    public void SetMusicVolume(float musicVolume){
        Debug.Log("music volume = " + musicVolume);
    }
}
