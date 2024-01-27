using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ComedianController : MonoBehaviour
{
    private const float TimeBuffer = 0.05f;

    [SerializeField] private Button jokeBookButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private TMP_Text dayOfWeekText;
    [SerializeField] private CrowdGenerator crowdGenerator;
    [SerializeField] private JokeBook jokeBook;
    [SerializeField] private int startCatCount = 3;
    [SerializeField] private float secondsForEachJoke = 60f;

    private int currentCatCount;
    private DayOfWeek currentDayOfWeek = DateTime.Today.DayOfWeek;
    private float currentJokeProgress;

    private void Start()
    {
        exitButton.onClick.SetListener(BackToMainMenu);
        StartNewDay(startCatCount);
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
        currentJokeProgress = 1f;
    }

    private void Update()
    {
        currentJokeProgress -= Time.deltaTime / secondsForEachJoke;
        timeSlider.value = currentJokeProgress;
        if (currentJokeProgress < -TimeBuffer)
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