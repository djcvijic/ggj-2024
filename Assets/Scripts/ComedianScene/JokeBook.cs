using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    public event Action<int> JokeTold;

    private void Start()
    {
        _config ??= JsonConvert.DeserializeObject<JokeBookConfig>(Resources.Load<TextAsset>("jokeBook").text);
    }
    public void OpenBook()
    {
        PurrfectAudioManager.Instance.FlipPage();
        _openBookButton.gameObject.SetActive(false);
        _noteObject.SetActive(true);
        _closeBookButton.gameObject.SetActive(true);

        for (int i = 0; i < _jokeNumberButtons.Count; i++)
        {
            var jokeNumber = i + 1;
            _jokeNumberButtons[i].onClick.SetListener(() => ShowJoke(jokeNumber));
        }

        _closeBookButton.onClick.SetListener(CloseBook);
    }

    private void ShowJoke(int jokeNumber)
    {
        PurrfectAudioManager.Instance.FlipPage();
        _noteObject.SetActive(false);
        _noteWithJokeObject.SetActive(true);

        _jokeText.text = GetJokeText(jokeNumber);

        _tellJokeButton.onClick.SetListener(() => TellJoke(jokeNumber));
        _backButton.onClick.SetListener(CloseJoke);
    }

    private void CloseJoke()
    {
        PurrfectAudioManager.Instance.FlipPage();
        _noteObject.SetActive(true);
        _noteWithJokeObject.SetActive(false);
    }

    private void TellJoke(int jokeNumber)
    {
        JokeTold?.Invoke(jokeNumber);
    }

    public void CloseBook()
    {
        PurrfectAudioManager.Instance.FlipPage();
        _noteObject.SetActive(false);
        _noteWithJokeObject.SetActive(false);
        _closeBookButton.gameObject.SetActive(false);
        _openBookButton.gameObject.SetActive(true);
    }

    private string GetJokeText(int jokeNumber)
    {
        var joke = _config.GetJokeConfig(jokeNumber).text
            .Replace("\\n", "\n");
        return $"JOKE #{jokeNumber}:\n\n{joke}";
    }

}
