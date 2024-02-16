using RuleSystem;
using System;
using System.Collections;
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
    [SerializeField] private TMP_Text dayOfWeekText;
    [SerializeField] private CrowdGenerator crowdGenerator;
    [SerializeField] private JokeBook jokeBook;
    [SerializeField] private int startCatCount = 3;
    [SerializeField] private float baseSecondsForJoke = 60f;
    [SerializeField] private float secondsForEachJoke = 60f;
    [SerializeField] private float secondsPerCat = 5f;
    [SerializeField] private GameObject dayEndHolder;
    [SerializeField] private TMP_Text dayEndText;
    [SerializeField] private Button gameEndButton;
    [SerializeField] private Image fill;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Animation blinds;

    private int _currentCatCount;
    private DayOfWeek _currentDayOfWeek = DateTime.Today.DayOfWeek;
    private float _currentJokeProgress;
    private bool _isGameOver;

    private List<AudienceCat> cats = new List<AudienceCat>();

    private void Start()
    {
        exitButton.onClick.SetListener(BackToMainMenu);
        gameEndButton.onClick.SetListener(BackToMainMenu);
        jokeBookButton.onClick.SetListener(jokeBook.OpenBook);
        StartNewDay(startCatCount);

        PurrfectAudioManager.Instance.StartLevelMusic();
    }

    private static void BackToMainMenu()
    {
        PurrfectSceneManager.LoadScene(SceneName.MainMenuScene);
    }

    private void StartNewDay(int catCount)
    {
        _currentCatCount = catCount;
        secondsForEachJoke = baseSecondsForJoke + catCount * secondsPerCat;
        _currentDayOfWeek = (DayOfWeek)(((int)_currentDayOfWeek + 1) % 7);
        dayOfWeekText.text = _currentDayOfWeek.ToString().Substring(0, 3).ToUpper();
        cats = crowdGenerator.GenerateCats(_currentCatCount, _currentDayOfWeek);
        _currentJokeProgress = 1f;
        fill.fillAmount = _currentJokeProgress;
        fill.color = gradient.Evaluate(_currentJokeProgress);
        _isGameOver = false;
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
        fill.fillAmount = _currentJokeProgress;
        fill.color = gradient.Evaluate(_currentJokeProgress);

        var musicStateIndex = MusicStateBreakpoints.FindIndex(x => _currentJokeProgress >= x);
        PurrfectAudioManager.Instance.FadeToState(1 + musicStateIndex);

        if (_currentJokeProgress < -TimeBuffer)
        {
            LoseDay();
        }
    }

    public void WinDay()
    {
        StartCoroutine(WinDayCoroutine());
    }

    public void LoseDay()
    {
        StartCoroutine(LoseDayCoroutine());
    }

    private void WinGame()
    {
        dayEndText.text = $"Congrats! \n You're a catmedy legend!";
        //_isGameOver = true;
    }

    private void LoseGame()
    {
        dayEndText.text = $"Wow dude! \n Put your material in the litter box!";
        //_isGameOver = true;
    }

    private IEnumerator WinDayCoroutine()
    {
        _isGameOver = true;
        jokeBook.CloseBook();
        dayEndText.gameObject.SetActive(false);
        dayEndHolder.SetActive(true);
        dayEndHolder.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        PurrfectAudioManager.Instance.WinCheer((float)_currentCatCount / crowdGenerator.MaxCatCount);
        for (int i = 0; i < cats.Count; i++)
        {
            StartCoroutine(cats[i].Laugh());
        }
        yield return new WaitForSeconds(0.8f);
        blinds.Play();
        yield return new WaitForSeconds(0.7f);
        dayEndHolder.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        dayEndText.gameObject.SetActive(true);
        if (_currentCatCount >= crowdGenerator.MaxCatCount)
        {
            WinGame();
            yield break;
        }
        dayEndText.text = $"Great joke! \n Get ready for {(DayOfWeek)(((int)_currentDayOfWeek + 1) % 7)}! \n \n Audience of {_currentCatCount + 1}!";
        yield return new WaitForSeconds(3f);
        dayEndHolder.SetActive(false);
        StartNewDay(_currentCatCount + 1);
    }

    private IEnumerator LoseDayCoroutine()
    {
        _isGameOver = true;
        jokeBook.CloseBook();
        dayEndText.gameObject.SetActive(false);
        dayEndHolder.SetActive(true);
        dayEndHolder.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        PurrfectAudioManager.Instance.LoseBoo((float)_currentCatCount / crowdGenerator.MaxCatCount);
        yield return new WaitForSeconds(0.75f);
        blinds.Play();
        yield return new WaitForSeconds(0.75f);
        dayEndHolder.GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        dayEndText.gameObject.SetActive(true);
        if (_currentCatCount <= 1)
        {
            LoseGame();
            yield break;
        }
        dayEndText.text = $"You bombed! \n Get ready for {(DayOfWeek)(((int)_currentDayOfWeek + 1) % 7)}! \n \n Audience of {_currentCatCount - 1}!";
        yield return new WaitForSeconds(3f);
        dayEndHolder.SetActive(false);
        StartNewDay(_currentCatCount - 1);
    }
}