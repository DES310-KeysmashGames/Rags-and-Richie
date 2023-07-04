using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gamePaused = false;
    public GameObject pauseMenu;

    [Header("Menu Buttons")]
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        //Resume Game via button click
        resumeButton.onClick.AddListener(() =>
        {
            //Play button sound

            ResumeGame();
        });

        //Bring up settings/options menu
        settingsButton.onClick.AddListener(() =>
        {
            //Play button sound

            Settings();
        });

        //Reset Time, unpause game, return to main menu
        menuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            gamePaused = false;
            Loader.Load(Loader.Scene.MainMenuScene);

            //Potentially pause background music
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                ResumeGame();
            } else
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
        Debug.Log("Imagine that theres a full working settings menu here...");

        //Various button sounds
    }
}
