using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{        
    public static SoundManager instance = null;


    public AudioClip Menu;
    public AudioClip Level1;

    void Awake()
    {
        //Check if there is already an instance of SoundManager
        if (instance == null)
            //if not, set it to this.
            instance = this;
        //If instance already exists:
        else if (instance != this)
            //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
            Destroy(gameObject);

        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        DontDestroyOnLoad(gameObject);
    }
    /*
    public void MenuMusic(){
        currentMusic = fullAudio[0];

        currentMusic.Play();
    }
    */

    public void MakeMenuSound()
    {
        MakeSound(Menu);
    }
    public void MakeLevel1Sound()
    {
        MakeSound(Level1);
    }


    private void MakeSound(AudioClip originalClip)
    {
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
