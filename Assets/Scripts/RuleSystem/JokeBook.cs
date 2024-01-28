using Newtonsoft.Json;
using RuleSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JokeBook : MonoBehaviour
{
    [SerializeField] private Button _tellJokeButton;
    [SerializeField] private Button _openBookButton;
    [SerializeField] private Button _closeBookButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private List<Button> _jokeNumberButtons;
    [SerializeField] private GameObject _noteObject;
    [SerializeField] private GameObject _noteWithJokeObject;
    [SerializeField] private TextMeshProUGUI _jokeText;

    private JokeBookConfig _config;

    private void Start()
    {
        _config ??= JsonConvert.DeserializeObject<JokeBookConfig>(Resources.Load<TextAsset>("jokeBook").text);
    }
    public void OpenBook()
    {
        _openBookButton.gameObject.SetActive(false);
        _noteObject.SetActive(true);
        _closeBookButton.gameObject.SetActive(true);

        for (int i = 0; i < _jokeNumberButtons.Count; i++)
        {
            var jokeNumber = i;
            _jokeNumberButtons[i].onClick.SetListener(() => ShowJoke(jokeNumber));
        }

        _closeBookButton.onClick.SetListener(CloseBook);
    }

    private void ShowJoke(int jokeNumber)
    {
        _noteObject.SetActive(false);
        _noteWithJokeObject.SetActive(true);

        _jokeText.text = _config.GetJokeConfig(jokeNumber).text;

        _tellJokeButton.onClick.SetListener(() => TellJoke(jokeNumber));
        _backButton.onClick.SetListener(CloseJoke);
    }

    private void CloseJoke()
    {
        _noteObject.SetActive(true);
        _noteWithJokeObject.SetActive(false);
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

    private void CloseBook()
    {
        _noteObject.SetActive(false);
        _noteWithJokeObject.SetActive(false);
        _closeBookButton.gameObject.SetActive(false);
        _openBookButton.gameObject.SetActive(true);
    }

}
