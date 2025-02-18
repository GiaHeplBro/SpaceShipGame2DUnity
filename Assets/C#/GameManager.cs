using UnityEngine;
using UnityEngine.SceneManagement; // Import để load lại scene

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject enemyPrefab;
    public float minInstantateValue;
    public float maxInstantateValue;
    public float enemyDestroytime = 10f;


    [Header("Particle Effects")]
    public GameObject explosion;
    public GameObject muzzleFlash;

    [Header("Panels")]
    public GameObject gameOverMenu;
    public GameObject pauseMenu;



    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);
        InvokeRepeating("InstantiateEnemy", 1f, 1f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(true);
        }
    }

    void InstantiateEnemy()
    {
        Vector3 enemypos = new Vector3(Random.Range(minInstantateValue, maxInstantateValue), 7f);

       GameObject enemy = Instantiate(enemyPrefab, enemypos, Quaternion.Euler(0f,0f,180f));
        Destroy(enemy, enemyDestroytime);
    }

    public void GameOverButton()
    {
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
    }    

    public void PauseGame(bool isPaused)
    {
        if(isPaused == true)
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
        Time.timeScale = 1f; // Reset tốc độ game về bình thường
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Load lại scene hiện tại
    }


}
