using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComedianController : MonoBehaviour
{
    private const float TimeBuffer = 0.05f;

    private static readonly List<float> MusicStateBreakpoints = new() { 0.75f, 0.5f, 0.25f, float.MinValue };

    [SerializeField] private Button jokeBookButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text dayOfWeekText;
    [SerializeField] private CrowdGenerator crowdGenerator;
    [SerializeField] private JokeBook jokeBook;
    [SerializeField] private int startCatCount = 3;
    [SerializeField] private float secondsForEachJoke = 60f;

    private int _currentCatCount;
    private DayOfWeek _currentDayOfWeek = DateTime.Today.DayOfWeek;
    private float _currentJokeProgress;
    private bool _isGameOver;

    private List<Cat> cats = new List<Cat>();

    private void Start()
    {
        exitButton.onClick.SetListener(BackToMainMenu);
        jokeBookButton.onClick.SetListener(jokeBook.OpenBook);
        StartNewDay(startCatCount);

        PurrfectAudioManager.Instance.StartLevelMusic();
    }

    private static void BackToMainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    private void StartNewDay(int catCount)
    {
        _currentCatCount = catCount;
        _currentDayOfWeek = (DayOfWeek)(((int)_currentDayOfWeek + 1) % 7);
        dayOfWeekText.text = _currentDayOfWeek.ToString();
        cats = crowdGenerator.GenerateCats(_currentCatCount, _currentDayOfWeek);
        _currentJokeProgress = 1f;
    }

    private void Update()
    {
        if (_isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            WinDay();
            return;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoseDay();
            return;
        }

        _currentJokeProgress -= Time.deltaTime / secondsForEachJoke;
        timeSlider.value = _currentJokeProgress;

        var musicStateIndex = MusicStateBreakpoints.FindIndex(x => _currentJokeProgress >= x);
        PurrfectAudioManager.Instance.FadeToState(1 + musicStateIndex);

        if (_currentJokeProgress < -TimeBuffer)
        {
            LoseDay();
        }
    }

    public void WinDay()
    {
        if (_currentCatCount >= crowdGenerator.MaxCatCount)
        {
            WinGame();
            return;
        }

        StartNewDay(_currentCatCount + 1);
    }

    public void LoseDay()
    {
        if (_currentCatCount <= 1)
        {
            LoseGame();
            return;
        }

        StartNewDay(_currentCatCount - 1);
    }

    private void WinGame()
    {
        for (int i = 0; i < cats.Count; i++)
        {
            StartCoroutine(cats[i].Laugh());
        }
        dayOfWeekText.text = "YOU WIN";
        _isGameOver = true;
    }

    private void LoseGame()
    {
        dayOfWeekText.text = "YOU LOSE";
        _isGameOver = true;
    }
}