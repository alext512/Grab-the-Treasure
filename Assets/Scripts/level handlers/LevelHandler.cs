using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelHandler : MonoBehaviour {

    public void Menu()
    {
        SceneManager.LoadScene(0);
        EventSystem.current.SetSelectedGameObject(null); //for butons, but didn't work
    }
    public void StartOne()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(1);

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
        SceneManager.LoadScene(5);
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void StartSix()
    {
        SceneManager.LoadScene(6);
    }
    public void StartNine()
    {

        SceneManager.LoadScene(9);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void disableButtonTest() {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
