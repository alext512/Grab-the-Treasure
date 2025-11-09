using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerPrefsManager : MonoBehaviour {
    const string MASTER_VOLUME_KEY = "master_volume";
    const string LEVEL_KEY = "level_unlocked_";
    const string HIGHSCORE_KEY = "level_set_highscore_";
    const string RESULTS_KEY = "results";

	// Use this for initialization
public static void SetMasterVolume (float volume)
    {
        if (volume > 0f && volume < 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume out of range");

        }
    }

    public static float GetMasterVolume() {
        return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
    }

    public static void SetHighScore(string levelName, int score) { //only this is useful..
        int currentHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY + levelName);
        if (score > currentHighScore)
        {
            print("yes");
            PlayerPrefs.SetInt(HIGHSCORE_KEY + levelName, score);
        }
    }

    public static int GetHighScore(string levelName)
    {
        return PlayerPrefs.GetInt(HIGHSCORE_KEY + levelName);

    }

    public static void SetResults(int score, string levelSet) //is this used?
    {
        PlayerPrefs.SetInt(RESULTS_KEY + "score", score);
        PlayerPrefs.SetString(RESULTS_KEY + "levelSet", levelSet);

    }

    public static int GetResultScore()
    {
        return PlayerPrefs.GetInt(RESULTS_KEY + "score");
    }
    public static string GetResultLevelSet()
    {
        return PlayerPrefs.GetString(RESULTS_KEY + "levelSet");
    }

    public void clearProgress()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    //    public static void UnlockLevel(int level) {
    //        if (level <= Application.levelCount - 1)
    //        {
    //        }
    //        else {
    //            Debug.LogError("Trying unlocksfgdsgf");
    //        }
    //    }

}
