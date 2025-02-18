using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Level Settings")]
    public LevelData currentLevelData;  // ✅ Dữ liệu màn chơi

    [Header("Game Objects")]
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    [Header("Spawn Settings")]
    public float minInstantateValue;
    public float maxInstantateValue;
    public float enemyDestroytime = 10f;

    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("UI Panels")]
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject winScreen;

    [Header("UI Elements")]
    public Text scoreText;

    private int score = 0;
    private int winScore;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        winScreen.SetActive(false);

        // ✅ Lấy dữ liệu từ LevelData
        winScore = currentLevelData.scoreToWin;

        UpdateScoreUI();
        InvokeRepeating("InstantiateEnemy", 1f, 1f);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        int baseEnemyCount = currentLevelData.baseEnemyCount; // Lấy số quái ban đầu từ LevelData
        int bonusEnemies = score / 20; // Cứ mỗi 20 điểm, tăng thêm 1 quái
        int enemyCount = baseEnemyCount + bonusEnemies; // Tổng số quái mỗi lần spawn

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 enemyPos = new Vector3(Random.Range(minInstantateValue, maxInstantateValue), 7f);
            GameObject enemy = Instantiate(enemyPrefab, enemyPos, Quaternion.Euler(0f, 0f, 180f));

            // ✅ Gán máu quái theo LevelData
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.health = currentLevelData.enemyHealth;
            }

            Destroy(enemy, enemyDestroytime);
        }
    }


    public void SpawnPowerUp(Vector3 enemyPosition)
    {
        if (powerUpPrefab != null)
        {
            Vector3 spawnPos = enemyPosition + new Vector3(0, -0.5f, 0); // Spawn gần vị trí Enemy
            Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Spawned PowerUp at: " + spawnPos);
        }
    }


    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();

        if (score >= winScore)
        {
            WinGame();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score + "/" + winScore;
    }

    void WinGame()
    {
        Debug.Log("YOU WIN!");
        winScreen.SetActive(true);
        Time.timeScale = 0f;

        // ✅ Lưu màn chơi đã hoàn thành vào PlayerPrefs
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
        PlayerPrefs.Save();

        Invoke("LoadNextLevel", 3f);
    }


    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Đã hoàn thành tất cả màn chơi!");
        }
    }

    public void PauseGame(bool isPaused)
    {
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
