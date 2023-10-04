using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewPauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    private bool optionsOn = false;
    private bool isTutorial = false;
    public float musicVol, sfxVol;
    private GameManager gameReset;

    [Header("Menu Panels")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Pause Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button menuButton;

    [Header("Options Menu Buttons")]
    [SerializeField] private Image optionsUI;
    [SerializeField] private Button backButton;
    [SerializeField] private Button guideButton;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;

    [Header("Tutorial Buttons")]
    [SerializeField] private GameObject tutorialMenu;
    [SerializeField] private Button[] tutorialButtons;
    [SerializeField] private GameObject[] tutorialScreens;

    //Wwise audio variables
    [Header("Audio Events")]
    public AK.Wwise.Event button;
    public AK.Wwise.Event slider;
    public AK.Wwise.Event tick;

    private void Awake()
    {
        //Get references to Scripts
        gameReset = FindObjectOfType<GameManager>();

        //Resume Game via button click
        resumeButton.onClick.AddListener(() =>
        {
            ResumeGame();

            //Play button sound
            button.Post(gameObject);
        });

        //Bring up Options menu
        optionsButton.onClick.AddListener(() =>
        {
            Settings();

            //Play button sound
            button.Post(gameObject);
        });

        //Reset Time, unpause game, return to main menu
        menuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gamePaused = false;

            //Play button sound
            button.Post(gameObject);

            //Return to main menu
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        //Open Tutorial Menus
        guideButton.onClick.AddListener(() =>
        {
            Guide();

            //Play button sound
            button.Post(gameObject);
        });

        //Return to Pause Menu
        backButton.onClick.AddListener(() =>
        {
            Back();

            //Play button sound
            button.Post(gameObject);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //Pause or Resume if Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsOn && isTutorial)
            {
                Settings();
            }
            else if (gamePaused && optionsOn)
            {
                Back();
            }
            else if (gamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //Pause time, trigger Singleton, activate Menu UI
    void PauseGame()
    {
        Time.timeScale = 0f;
        gamePaused = true;
        optionsOn = false;
        pauseMenu.gameObject.SetActive(true);
    }

    //Reset time, 
    void ResumeGame()
    {
        Time.timeScale = 1f;
        gamePaused = false;
        pauseMenu.gameObject.SetActive(false);
    }

    void Settings()
    {
        //Disable main pause menu buttons
        resumeButton.gameObject.SetActive(false);
        optionsButton.gameObject.SetActive(false);
        menuButton.gameObject.SetActive(false);

        //Disable tutorial buttons
        isTutorial = false;
        tutorialMenu.gameObject.SetActive(false);   

        //Enable Settings menu
        optionsOn = true;
        optionsMenu.gameObject.SetActive(true);
        music.gameObject.SetActive(true);
        sfx.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }
    void Guide()
    {
        //Disable Options Menu
        music.gameObject.SetActive(false);
        sfx.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);

        //Enable Tutorial Screens
        isTutorial = true;
        tutorialMenu.gameObject.SetActive(true);
        tutorialScreens[0].gameObject.SetActive(true);

        //Travel Button
        tutorialButtons[0].onClick.AddListener(() =>
        {
            tutorialScreens[0].gameObject.SetActive(true);
            tutorialScreens[1].gameObject.SetActive(false);
            tutorialScreens[2].gameObject.SetActive(false);
            tutorialScreens[3].gameObject.SetActive(false);
            tutorialScreens[4].gameObject.SetActive(false);

            //Play button sound
            button.Post(gameObject);
        });

        //Items Button
        tutorialButtons[1].onClick.AddListener(() =>
        {
            tutorialScreens[0].gameObject.SetActive(false);
            tutorialScreens[1].gameObject.SetActive(true);
            tutorialScreens[2].gameObject.SetActive(false);
            tutorialScreens[3].gameObject.SetActive(false);
            tutorialScreens[4].gameObject.SetActive(false);

            //Play button sound
            button.Post(gameObject);
        });

        //Customer Button
        tutorialButtons[2].onClick.AddListener(() =>
        {
            tutorialScreens[0].gameObject.SetActive(false);
            tutorialScreens[1].gameObject.SetActive(false);
            tutorialScreens[2].gameObject.SetActive(true);
            tutorialScreens[3].gameObject.SetActive(false);
            tutorialScreens[4].gameObject.SetActive(false);

            //Play button sound
            button.Post(gameObject);
        });

        //Customer Button
        tutorialButtons[3].onClick.AddListener(() =>
        {
            tutorialScreens[0].gameObject.SetActive(false);
            tutorialScreens[1].gameObject.SetActive(false);
            tutorialScreens[2].gameObject.SetActive(false);
            tutorialScreens[3].gameObject.SetActive(true);
            tutorialScreens[4].gameObject.SetActive(false);

            //Play button sound
            button.Post(gameObject);
        });

        //Customer Button
        tutorialButtons[4].onClick.AddListener(() =>
        {
            tutorialScreens[0].gameObject.SetActive(false);
            tutorialScreens[1].gameObject.SetActive(false);
            tutorialScreens[2].gameObject.SetActive(false);
            tutorialScreens[3].gameObject.SetActive(false);
            tutorialScreens[4].gameObject.SetActive(true);

            //Play button sound
            button.Post(gameObject);
        });

        //Close Button
        tutorialButtons[5].onClick.AddListener(() =>
        {
            tutorialScreens[0].gameObject.SetActive(false);
            tutorialScreens[1].gameObject.SetActive(false);
            tutorialScreens[2].gameObject.SetActive(false);
            tutorialScreens[3].gameObject.SetActive(false);
            tutorialScreens[4].gameObject.SetActive(false);

            //Play button sound
            button.Post(gameObject);

            Settings();
        });
    }

    void Back()
    {
        //Disable Settings Menu
        optionsOn = false;
        optionsMenu.gameObject.SetActive(false);

        //Re-enable main pause menu buttons 
        resumeButton.gameObject.SetActive(true);
        optionsButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
    }

    public void SetSFXVolume()
    {
        sfxVol = sfx.value;
        AkSoundEngine.SetRTPCValue("SFXVolume", sfxVol);
    }

    public void SetMusicVolume()
    {
        musicVol = music.value;
        AkSoundEngine.SetRTPCValue("MusicVolume", musicVol);
    }
}