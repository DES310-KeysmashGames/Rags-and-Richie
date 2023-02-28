using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;

    private void Awake(){
        playButton.onClick.AddListener(()=> {
            //click action

        });
        optionsButton.onClick.AddListener(()=> {
            //click action

        });

        quitButton.onClick.AddListener(()=> {
            //click action
            Application.Quit();
        });
    }

}
