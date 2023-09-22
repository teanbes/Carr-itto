using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }
    [SerializeField] private InputManager inputManager;

    [Header("Button")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button returnToMenuButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Panel Components")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] public GameObject gameOverPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] public GameObject HUDPanel;

    [Header("Audio Components")]
    [SerializeField] private AudioSource backgroundMusic;

    [Header("Score Components")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI currentSpeedText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    private String scoreText;
    private String speedText;


    private bool isPaused;
    private bool isActive;
    [HideInInspector] public bool isDead;

    private void Start()
    {

        if (playButton)
        {
            playButton.onClick.AddListener(() => Invoke("StartGame", 2f));
            playButton.onClick.AddListener(() => AudioManager.Instance.Play("Select"));
        }

        if (instructionsButton)
            instructionsButton.onClick.AddListener(Instructions);

        if (creditsButton)
            creditsButton.onClick.AddListener(Credits);

        if (resumeButton)
            resumeButton.onClick.AddListener(PauseGame);

        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(GameQuit);

        if (settingsButton)
            settingsButton.onClick.AddListener(GameSettings);

        if (quitButton)
            quitButton.onClick.AddListener(GameQuit);

        if (backButton)
            backButton.onClick.AddListener(BackToPauseMenu);

        if (!backgroundMusic && SceneManager.GetActiveScene().buildIndex == 1)
            Debug.Log("Please set Background music file 1");

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            UpdateScoreDisplay();
            UpdateSpeedDisplay();
        }
        Time.timeScale = 1;
    }

     private void OnEnable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            inputManager.PauseEvent += PauseGame;
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            inputManager.PauseEvent -= PauseGame;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.Instance.Play("Select");
            PauseGame();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            UpdateScoreDisplay();
            UpdateSpeedDisplay();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }

    private void GameSettings()
    {
        AudioManager.Instance.Play("Select");
        settingsPanel.SetActive(true);
    }

    private void BackToPauseMenu()
    {
        AudioManager.Instance.Play("Select");
        instructionsPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.Play("Select");
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void GameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Credits()
    {
        AudioManager.Instance.Play("Select");
        creditsPanel.SetActive(true);
        isActive = true;
    }

    private void Instructions()
    {
        AudioManager.Instance.Play("Select");
        instructionsPanel.SetActive(true);
        isActive = true;
    }

    public void GoBack()
    {
        if (isActive)
        {
            AudioManager.Instance.Play("Select");
            if (instructionsPanel)
                instructionsPanel.SetActive(false);
            if (creditsPanel)
                creditsPanel.SetActive(false);
            isActive = false;
        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        HUDPanel.SetActive(false);

        if (isPaused)
        {
            Time.timeScale = 0;
            PauseBackgorundMusic();
        }

        else
        {
            AudioManager.Instance.Play("Select");
            Time.timeScale = 1;
            HUDPanel.SetActive(true);
            UnpauseBackgorundMusic();
        }
    }

    public void PauseBackgorundMusic()
    {
        backgroundMusic.Pause();
    }

    public void UnpauseBackgorundMusic()
    {
        backgroundMusic.UnPause();
    }

    public void SetInactive()
    {
        GoBack();
    }

    public void UpdateScoreDisplay()
    {
        scoreText = GameManager.instance.scoreText;
        currentScoreText.text = scoreText;
        totalScoreText.text = scoreText;
    }

    public void UpdateSpeedDisplay()
    {
       
        if (currentSpeedText != null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Speed:");
            sb.Append(((int)(GameManager.instance.currentSpeed)).ToString());
            sb.Append(" Kph");

            currentSpeedText.text = sb.ToString();
        }
        else if (currentSpeedText.text != "")
        {

        }

    }
}

