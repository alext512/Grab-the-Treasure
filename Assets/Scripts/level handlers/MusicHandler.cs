using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicHandler : MonoBehaviour
{
    AudioSource audioSource;
    string audioClipName;

    private void Awake()
    {
        int numMusicHandlers = FindObjectsOfType<MusicHandler>().Length;
        if (numMusicHandlers > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Some scenes may not have a chest; fail gracefully in those scenes.
        TryApplyMusicFromChest();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        TryApplyMusicFromChest();
    }

    private void TryApplyMusicFromChest()
    {
        GameObject chestObject = GameObject.Find("Chest");
        if (chestObject == null)
        {
            return;
        }

        Chest chest = chestObject.GetComponent<Chest>();
        if (chest == null || chest.levelMusic == null)
        {
            return;
        }

        AudioClip newMusic = chest.levelMusic;
        if (audioClipName == newMusic.name)
        {
            return;
        }

        audioSource.clip = newMusic;
        audioSource.loop = true;
        audioSource.Play();
        audioClipName = newMusic.name;
        LivesScore.musicName = audioClipName;
    }
}
