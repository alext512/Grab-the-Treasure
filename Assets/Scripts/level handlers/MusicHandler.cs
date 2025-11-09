using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour {
    //[SerializeField] AudioClip[] LevelMusics;
    //[SerializeField] AudioClip levelClip;

    private AudioSource audioSource;
    private string audioClipName;

    private void Awake()

    {
        int numGameSessions = FindObjectsOfType<MusicHandler>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Use this for initialization
    void Start () {

        audioSource = GetComponent<AudioSource>();
        AudioClip newMusic = GameObject.Find("Chest").GetComponent<Chest>().levelMusic;//levelClip;//gameObject.GetComponent<LevelManager>().currentLevelMusic;//GameObject.Find("LevelManager").GetComponent<LevelManager>().currentLevelMusic;
        audioSource.clip = newMusic;
        //if (newMusic.name == LivesScore.musicName) { }
        //else
        //{
            audioSource.loop = true;
            audioSource.Play();
            audioClipName = audioSource.clip.name;
            LivesScore.musicName = audioClipName;
        //}


    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (GameObject.Find("Chest") != null)
        {
            GameObject levelManager = GameObject.Find("Chest");
            AudioClip newMusic = GameObject.Find("Chest").GetComponent<Chest>().levelMusic;
            audioSource = GetComponent<AudioSource>();
            //print(audioSource.clip.name);
            //print(newMusic.name);
            if (audioClipName != newMusic.name)
            {
                audioSource.clip = newMusic;
                audioSource.Play();
                audioClipName = newMusic.name;
                //print(audioClipName);


            }
        }
    }
    
}
