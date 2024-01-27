using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComedianController : MonoBehaviour
{
    private const int StartCatCount = 3;
    private static readonly DayOfWeek StartDayOfWeek = DateTime.Today.DayOfWeek;

    [SerializeField] private Button jokeBookButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text dayOfWeekText;
    [SerializeField] private CrowdGenerator crowdGenerator;
    [SerializeField] private JokeBook jokeBook;
    [SerializeField] private float secondsForEachJoke = 60f;

    private int currentCatCount = StartCatCount;
    private DayOfWeek currentDayOfWeek = StartDayOfWeek;
    private float currentJokeProgress;

    private void Start()
    {
        exitButton.onClick.AddListener(BackToMainMenu);
        StartNewDay(StartCatCount);
    }

    private static void BackToMainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

    private void StartNewDay(int catCount)
    {
        currentCatCount = catCount;
        currentDayOfWeek = (DayOfWeek)(((int)currentDayOfWeek + 1) % 7);
        dayOfWeekText.text = currentDayOfWeek.ToString();
        crowdGenerator.GenerateCats(currentCatCount, currentDayOfWeek);
        currentJokeProgress = 0;
    }

    private void Update()
    {
        currentJokeProgress += Time.deltaTime / secondsForEachJoke;
        timeSlider.value = currentJokeProgress;
        if (currentJokeProgress > 1f)
        {
            LoseDay();
        }
    }

    private void WinDay()
    {
        StartNewDay(currentCatCount + 1);
    }

    private void LoseDay()
    {
        StartNewDay(currentCatCount - 1);
    }
}