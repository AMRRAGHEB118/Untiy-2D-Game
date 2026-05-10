using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;
    public GameObject winUI;
    
    private float difficultyTimer = 0f;
    public float difficultyScale = 1.1f;
    public float difficultyInterval = 30f; // Increase difficulty every 30 seconds

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreText();
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (winUI != null) winUI.SetActive(false);
    }

    void Update()
    {
        difficultyTimer += Time.deltaTime;
        if (difficultyTimer >= difficultyInterval)
        {
            IncreaseDifficulty();
            difficultyTimer = 0f;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void WinGame()
    {
        if (winUI != null) winUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            WinGame();
        }
    }

    void IncreaseDifficulty()
    {
        Debug.Log("Difficulty Increased!");
        // Logic to increase difficulty globally or notify enemies
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach (EnemyAI enemy in enemies)
        {
            enemy.speed *= difficultyScale;
            enemy.attackDamage = Mathf.RoundToInt(enemy.attackDamage * difficultyScale);
        }
    }
}
