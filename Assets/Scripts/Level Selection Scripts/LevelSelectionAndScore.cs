using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionAndScore : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] int maxScore = 10;

    TextMesh score;
    // Start is called before the first frame update
    void Start()
    {
        //set the score
        score = gameObject.GetComponentInChildren<TextMesh>(); //ATTENTION: We get the first TextMesh. This happens to be "score" right now.
        score.text = " " + PlayerPrefsManager.GetHighScore(levelName) + "/" + maxScore;
    }

    // Update is called once per frame

    void OnMouseDown()
    {
        SceneManager.LoadScene(levelName);
    }
}
