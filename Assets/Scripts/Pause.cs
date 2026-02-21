using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PauseGame() {
        print("pause");
        if (gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void LoadLevelsMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelsMenu");
    }
}
