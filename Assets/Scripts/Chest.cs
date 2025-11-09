using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Chest : MonoBehaviour {

    [SerializeField] GameObject CloseChest;
    [SerializeField] GameObject OpenChest;
    [SerializeField] string nextLevel;
    [SerializeField] float waitAfterWin;
    [SerializeField] bool lastLevelOfSet;
    [SerializeField] int currentLevelSet; //used to set the highscore -IMPORTANT.
    public AudioClip levelMusic; 

    AudioSource openChest;
    bool chestIsOpent = false;
    GameObject player;
    GameObject gameSession;
    int score; //score when level set is cleared

    void Start () {
        GameObject.FindGameObjectWithTag("scoreTag").GetComponent<Text>().text = "0";

        openChest = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("player");
        gameSession = GameObject.FindWithTag("gameSession");
        
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (chestIsOpent == false && player.GetComponent<Player>().GotChest())
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = OpenChest.GetComponent<SpriteRenderer>().sprite;
            openChest.Play();
            chestIsOpent = true;
            Scene scene = SceneManager.GetActiveScene();
            PlayerPrefsManager.SetHighScore(scene.name, score);
            if (false) {//(lastLevelOfSet) {
                score = LivesScore.coins;//gameSession.GetComponent<GameSession>().GetCoins();
                print(Levels.levelSets[currentLevelSet]);
                PlayerPrefsManager.SetHighScore(Levels.levelSets[currentLevelSet], score);//SetHighScore(Levels.levelSets[currentLevelSet], endScore);
                print(PlayerPrefsManager.GetHighScore(Levels.levelSets[currentLevelSet]));
                
                //LivesScore.
            }
            StartCoroutine(LoadNextLevel("LevelsMenu")); //changed
            //if (lastLevelOfSet) {
            //    GameObject.Find("PersistentHandler").GetComponent<GameSession>().DestroyThis();
            //}

        }
        
    }
    void Update () {
		
	}

    IEnumerator LoadNextLevel(string nextLevel)
    {

        yield return new WaitForSecondsRealtime(waitAfterWin);

        //TODO: make it better


        SceneManager.LoadScene(nextLevel);

    }

    public void addScore(int value)
    {
        score = score + value;
        GameObject.FindGameObjectWithTag("scoreTag").GetComponent<Text>().text = score.ToString();
    }
}
