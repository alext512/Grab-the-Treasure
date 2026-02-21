using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// GAME SESSION: Exists only in gameplay levels.
// Keeps track of score/lives and persists between level scenes.
public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coins = 0;

    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;

    int coinsPicked = 0;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        LivesScore.lives = playerLives;
        LivesScore.coins = coins;
        RefreshHud();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AddToCoins(int pointsToAdd)
    {
        LivesScore.coins += pointsToAdd;
        coinsPicked += pointsToAdd;
        RefreshHud();
    }

    public void ProcessPlayerDeath()
    {
        LivesScore.coins -= coinsPicked;
        coinsPicked = 0;
        RefreshHud();

        if (playerLives > 1)
        {
            TakeLife();
            return;
        }

        SceneManager.LoadScene(Levels.startingScreen);
        ResetGameSession();
    }

    private void TakeLife()
    {
        playerLives--;
        LivesScore.lives = playerLives;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        RefreshHud();
    }

    public void ResetGameSession()
    {
        LivesScore.coins = coins;
        LivesScore.lives = playerLives;
        coinsPicked = 0;
        RefreshHud();
    }

    public int GetCoins()
    {
        return coins;
    }

    // Kept for compatibility with existing button/event wiring.
    public void DestroyThis()
    {
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        coinsPicked = 0;

        if (scene.name == "StartingScreen" || scene.name == "LevelsMenu")
        {
            Destroy(gameObject);
        }
    }

    private void RefreshHud()
    {
        if (coinsText != null)
        {
            coinsText.text = LivesScore.coins.ToString();
        }

        if (livesText != null)
        {
            livesText.text = playerLives.ToString();
        }
    }
}
