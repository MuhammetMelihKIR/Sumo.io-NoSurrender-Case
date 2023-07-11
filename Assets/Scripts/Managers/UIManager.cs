using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("CANVAS PANELS")]

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _inGamePanel;
    [SerializeField] private GameObject _loseGamePanel;
    [SerializeField] private GameObject _winGamePanel;
    [Space]

    [Header("START PANEL ELEMENTS")]
    [SerializeField] private TextMeshProUGUI _tapToStartText;
    [SerializeField] private Button _startButton;

    [Space]

    [Header("IN GAME PANEL ELEMENTS")]
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private TextMeshProUGUI _goText;
    [SerializeField] private TextMeshProUGUI _startTimerText;
    [SerializeField] private GameObject _100Text;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Sprite _pauseSprite;
    [SerializeField] private Sprite _resumeSprite;
    [SerializeField] private float _timerValue;
    [SerializeField] private int _enemyValue;
    int _startTimerCount = 3;

    private int _seconds;
    private int _scoreValue;

    public bool _timeScale = true;

    [Space]

    [Header("LOSE GAME PANEL")]
    [SerializeField] private TextMeshProUGUI _gameOverText;
    [SerializeField] private Button _restartButton;
    [Space]

    [Header("WIN GAME PANEL")]
    [SerializeField] private TextMeshProUGUI _winGameText;
    [SerializeField] private Button _nextButton;

    public bool startGame;
    


    private void OnEnable()
    {
        EventManager.OnGameOverEvent += LoseGamePanelActive;
        EventManager.OnGameStartedEvent += StartPanelActive;
    }

    private void OnDisable()
    {
        EventManager.OnGameOverEvent -= LoseGamePanelActive;
        EventManager.OnGameStartedEvent -= StartPanelActive;

    }
    private void Awake()
    {
        Instance = this;
        startGame = false;
    }


  
    private void Update()
    {
        Timer();
    }

    

    #region CANVAS PANELS
    private void StartPanelActive()
    {
        _startPanel.SetActive(true);
        _inGamePanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        Time.timeScale = 0;
    }

    private void InGamePanelActiive()
    {
        _inGamePanel.SetActive(true);
        _startPanel.SetActive(false);
        _loseGamePanel.SetActive(false);
        _winGamePanel.SetActive(false);
        Time.timeScale = 1;
    }
    private void LoseGamePanelActive()
    {
        _loseGamePanel.SetActive(true);
        _inGamePanel.SetActive(false);
        _startPanel.SetActive(false);
        _winGamePanel.SetActive(false);
        Time.timeScale = 0;
    }

    private void WinPanelActive()
    {
        _loseGamePanel.SetActive(false);
        _inGamePanel.SetActive(false);
        _startPanel.SetActive(false);
        _winGamePanel.SetActive(true);
        Time.timeScale = 0;
    }
    #endregion

    #region BUTTONS
    public void StartButton()
    {
        _tapToStartText.gameObject.SetActive(false);
        StartTimerSequence();
        StartCoroutine(WaitForStart());
    }
    public void PauseButton()
    {
        if (_timeScale)
        {
            Time.timeScale = 0;
            _pauseButton.image.sprite = _resumeSprite;
            _timeScale = false;
        }
        else if (!_timeScale)
        {
            Time.timeScale = 1;
            _pauseButton.image.sprite = _pauseSprite;
            _timeScale = true;
        }
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    #region GAME START TIMER
    void StartTimerSequence()
    {
        _startTimerText.gameObject.SetActive(true);
        _startTimerText.text = _startTimerCount.ToString();


        if (_startTimerCount > 0)
            _startTimerText.transform.DOScale(new Vector3(15, 15, 15), 1f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                _startTimerText.transform.localScale = Vector3.one;
                _startTimerCount--;
                StartTimer();
            });
        else
        {
            _startTimerText.gameObject.SetActive(false);
            _startTimerText.transform.localScale = Vector3.one;
            _startTimerCount = 3;

            ShowGoText();


        }
    }

    private void StartTimer()
    {
        StartTimerSequence();
    }

    private void ShowGoText()
    {
        _goText.gameObject.SetActive(true);

        _goText.transform.DOScale(new Vector3(12, 12, 12), 1f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _goText.gameObject.SetActive(false);
            _goText.transform.localScale = Vector3.one;


        });
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(4f);
        InGamePanelActiive();
        startGame = true;
    }
    #endregion
    private void Timer()
    {
        _timerValue -= Time.deltaTime;
        _seconds = Convert.ToInt32(_timerValue % 99);
        _timerText.text = _seconds.ToString();
        if (_timerValue <= 0)
        {
            
            EventManager.OnGameOver();
        }
    }


    public void ScoreIncrease()
    {
        _scoreValue += 100;
        _scoreText.text = _scoreValue.ToString();

    }

    public void EnemyValue()
    {
        _enemyValue--;
        _enemyCountText.text = _enemyValue.ToString();
        if (_enemyValue <= 0)
        {
            WinPanelActive();
        }
    }


    public void PlayerScoreAnim()
    {
        _100Text.SetActive(true);
        _100Text.transform.DOScale(new Vector3(5, 5, 5), 1f).SetEase(Ease.OutSine).OnComplete(() =>
        {
            _100Text.transform.localScale = Vector3.one;
            _100Text.SetActive(false);
        });
    }

    

}