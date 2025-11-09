using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelHandler : MonoBehaviour {
    [SerializeField] string nextLevel="writeNextLevelHere";

    public void LoadNextSceneNumber() {
        SceneManager.LoadScene(nextLevel);
    }

    public void LoadSceneNumber(int scene) {
        SceneManager.LoadScene(scene);
    }

    public void LoadSceneName(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadStartingScreen() {
        SceneManager.LoadScene("StartingScreen");
    }
    public void LoadLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
}
