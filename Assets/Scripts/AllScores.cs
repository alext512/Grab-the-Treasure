using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllScores : MonoBehaviour
{
    [SerializeField] Text[] highscores;
    // Start is called before the first frame update
    void Start()
    {
        int count = 1;
        foreach (Text x in highscores)
        {
            //print(count);
            x.text = " " + PlayerPrefsManager.GetHighScore(Levels.levelSets[count]).ToString() + "/10";//"score: ";// + PlayerPrefsManager.GetHighScore(Levels.levelSets[count]).ToString();// "lj";//Levels.levelSets[count];
            //TODO: better string
            count++;
        }
    }




}
