using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public void Menu()
    {
        LoadSceneAndClearSelection(0);
    }

    public void StartOne()
    {
        LoadSceneAndClearSelection(1);
    }

    public void StartTwo()
    {
        SceneManager.LoadScene(2);
    }

    public void StartThree()
    {
        SceneManager.LoadScene(3);
    }

    public void StartFour()
    {
        SceneManager.LoadScene(4);
    }

    public void StartFive()
    {
        LoadSceneAndClearSelection(5);
    }

    public void StartSix()
    {
        SceneManager.LoadScene(6);
    }

    public void StartNine()
    {
        LoadSceneAndClearSelection(9);
    }

    public void disableButtonTest()
    {
        ClearCurrentSelection();
    }

    private void LoadSceneAndClearSelection(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
        ClearCurrentSelection();
    }

    private void ClearCurrentSelection()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
