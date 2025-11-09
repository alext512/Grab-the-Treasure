using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {
    int highScore = 0;


    private void Awake()
    {
        int numHighScores = FindObjectsOfType<HighScore>().Length;
        if (numHighScores > 1)
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
		
	}

    // Update is called once per frame


    public void setHighScore(int score) {
        if (score > highScore) {
            highScore=score;
            print("hiscore"+highScore);
            print("score" + score);
        }
    }
    public int getHighScore() {
        return highScore;
    }

}
