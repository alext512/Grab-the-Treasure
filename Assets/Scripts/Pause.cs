using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void PauseGame()
    {
        bool shouldPause = !gameObject.activeInHierarchy;

        gameObject.SetActive(shouldPause);
        Time.timeScale = shouldPause ? 0f : 1f;
    }

    public void LoadLevelsMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelsMenu");
    }
}
