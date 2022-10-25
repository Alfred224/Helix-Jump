using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool levelCompleted = true; // Even though no level has been completed yet, this is required to initialize difficulty mode
    public static bool isGameStarted;
    public static bool mute = false;

    public GameObject gameOverPanel;
    public GameObject levelCompletedPanel;
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public GameObject gameModeButton;

    public static int currentLevelIndex;
    public int numberOfRings;
    public Slider gameProgressSlider;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public ScoreManager scoreManager;

    public static int numberOfPassedRings;
    public static int score = 0;
    public static float difficulty = 1;

    private void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Game mode will be active only after completing 1 level
        gameModeButton.SetActive(PlayerPrefs.GetInt("currentLevelIndex", 0) == 0 ? false : levelCompleted);

        isGameStarted = gameOver = levelCompleted  = false;
        Time.timeScale = 1;
        numberOfPassedRings = 0;
        highScoreText.text = "Best Score\n" + PlayerPrefs.GetInt("HighScore", 0);
        numberOfRings = FindObjectOfType<HelixManager>().numberOfRings;

        AdManager.instance.RequestInterstitial();
    }

    // Difficulty manager
    public void GameMode(float difficultyIndex)
    {
        difficulty = difficultyIndex;
    }

    // Update is called once per frame
    void Update()
    {
        // Setting time relative to difficulty
        Time.timeScale = difficulty;

        // UI update
        currentLevelText.text = currentLevelIndex.ToString();
        nextLevelText.text = (currentLevelIndex + 1).ToString();

        int progress = numberOfPassedRings * 100 / numberOfRings;
        gameProgressSlider.value = progress;

        scoreText.text = score.ToString();

        // For touch: if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isGameStarted)
        if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            // For touch: if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            isGameStarted = true;
            gamePlayPanel.SetActive(isGameStarted);
            startMenuPanel.SetActive(!isGameStarted);
        }

        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                if (score > PlayerPrefs.GetInt("HighScore", 0))
                {
                    PlayerPrefs.SetInt("HighScore", score);
                    scoreManager.AddScore(new Score(score));
                }
                score = 0;
                SceneManager.LoadScene("Level");
            }

            if (Random.Range(0, 3) == 2)
            {
                AdManager.instance.ShowInterstitial();
            }
        }
        
        if (levelCompleted)
        {
            levelCompletedPanel.SetActive(true);

            if (Input.GetButtonDown("Fire1"))
            {
                currentLevelIndex++;
                PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex);
                SceneManager.LoadScene("Level");
            }
        }
    }
}
