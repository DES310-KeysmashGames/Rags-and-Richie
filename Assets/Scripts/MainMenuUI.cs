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
    //[SerializeField] Button closeButton;
    //[SerializeField] Button guideButton;
    //[SerializeField] Button traveltutButton;
    //[SerializeField] Button itemtutButton;
    //[SerializeField] Button customertutButton;
    //[SerializeField] Button reactionstutButton;
    //[SerializeField] Button daystutButton;
    //[SerializeField] Button close2Button;
    [SerializeField] AnimationTrade shutterClosing;
    public AK.Wwise.Event buttonClickEvent;
    private bool playing;
    private bool playGame;
    private float timer;
    private float sfx;
    private float music;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    private void Awake(){
        optionsCanvas.gameObject.SetActive(false);
        playButton.onClick.AddListener(()=> {
            //click action
            buttonClickEvent.Post(gameObject);
            playGame = true;
            shutterClosing.MainMenuTransition();
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
        timer = 3.0f;
        playGame = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playing = !playing;
        }

        if (playGame)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            Loader.Load(Loader.Scene.TravelScene);
            PlayerPrefs.SetInt("wallet", 0);
            StaticTravel.goal = 500;
            StaticTravel.dayCount = 1;
        }

    }

    //Main Menu Background Audio

    public void SetSFXVolume()
    {
        sfx = SFXSlider.value;
        AkSoundEngine.SetRTPCValue("SFXVolume", sfx);
    }

    public void SetMusicVolume()
    {
        music = musicSlider.value;
        AkSoundEngine.SetRTPCValue("MusicVolume", music);
    }
}
