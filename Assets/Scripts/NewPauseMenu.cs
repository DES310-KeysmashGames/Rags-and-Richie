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
    private GameManager gameReset;

    [Header("Menu Panels")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    [Header("Pause Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button menuButton;

    [Header("Options Menu Buttons")]
    [SerializeField] private Button backButton;

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

            //Check what scene the pause menu is being used in
            //if (SceneManager.GetActiveScene().name == "TradeScene")
            //{
            //    gameReset.ResetLevel();
            //}

            //Return to main menu
            Loader.Load(Loader.Scene.MainMenuScene);

            //Potentially pause background music
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
            if (gamePaused && optionsOn)
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

        //Enable Settings menu
        optionsOn = true;
        optionsMenu.gameObject.SetActive(true);
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
}