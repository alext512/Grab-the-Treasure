using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//GAME SESSION: Exists only at levels. Keeps track of score and lives. Loads menu if lives are 0.

public class GameSession : MonoBehaviour {

    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;
    int coinsPicked = 0;
    GameObject go;

    private void Awake()
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        print(numGameSessions);
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {
        go = gameObject;
        SceneManager.sceneLoaded += OnSceneLoaded;
        LivesScore.lives = playerLives;
        LivesScore.coins = coins;
        if (livesText != null && coinsText != null)
        {
            //print(LivesScore.coins);
            livesText.text = LivesScore.lives.ToString();// playerLives.ToString();
            coinsText.text = LivesScore.coins.ToString();// coins.ToString();
        }

    }

    // Update is called once per frame
    public void AddToCoins(int pointsToAdd) {
        LivesScore.coins += pointsToAdd;//coins += pointsToAdd;
        coinsPicked+= pointsToAdd;
        coinsText.text = LivesScore.coins.ToString();
    }

    public void ProcessPlayerDeath() {
        LivesScore.coins = LivesScore.coins - coinsPicked;
        coinsText.text = LivesScore.coins.ToString();
        coinsPicked = 0;

        if (playerLives > 1) {
            TakeLife();
        }
        else {
            SceneManager.LoadScene(Levels.startingScreen);
            ResetGameSession();
        }
    }

    private void TakeLife() {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    public void ResetGameSession() {
        LivesScore.coins = coins;
        LivesScore.lives = playerLives;
        coinsPicked = 0;
        coinsText.text = LivesScore.coins.ToString();
        livesText.text = LivesScore.lives.ToString();
    }

    public int GetCoins() {
        print(coins.ToString());
        return coins; //what
    }

    public void DestroyThis() {
        //Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) {
        coinsPicked = 0;
        if (scene.name == "StartingScreen" || scene.name == "LevelsMenu") {
            print("hi");
            Destroy(go);
            //Destroy(this);
        }
    }
}
