using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float pointsGainedPerKid;
    public float score;

    public bool gameOver;
    
    [SerializeField] private GameObject gameOverPanel;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        score = 0;
    }

    public void AddScore()
    {
        score += pointsGainedPerKid;
    }

    public void TriggerEndGame()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;    
        }
    }

    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Title");
    }
}
