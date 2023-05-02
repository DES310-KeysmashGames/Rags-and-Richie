// on musicplayer
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    
    public GameObject musicObject;
    private AudioSource audioSource;
    private float musicVolume = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        musicObject = GameObject.FindWithTag("GameMusic");
        audioSource = musicObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = musicVolume;
    }


    public void ChangeVolume(float volume){
        musicVolume = volume;
    }
}
