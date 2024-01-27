using Newtonsoft.Json;
using RuleSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JokeBook : MonoBehaviour
{
    [SerializeField] private Button _tellJokeButton;
    [SerializeField] private Button _jokeBookButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _closeBookButton;

    private JokeBookConfig _config;

    private void Start()
    {
        _config ??= JsonConvert.DeserializeObject<JokeBookConfig>(Resources.Load<TextAsset>("jokeBook").text);

        _exitButton.onClick.AddListener(BackToMainMenu);
    }

    private void ShowJoke(int jokeNumber)
    {
        var jokeText = _config.GetJokeConfig(jokeNumber).text;

        Debug.LogError(jokeText);
    }

    private void TellJoke(int jokeNumber)
    {
        if(RuleBook.Instance.IsCorrectJoke(jokeNumber))
        {
            Debug.LogError("Correct!");
        }
        else
        {
            Debug.LogError("Incorrect!");
        }
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("SceneMainMenu");
    }

}
