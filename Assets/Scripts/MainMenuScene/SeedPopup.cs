using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SeedPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text comedianText;
    [SerializeField] private TMP_Text whispererText;
    [SerializeField] private TMP_InputField seedTextArea;
    [SerializeField] private Button comedianPlayButton;
    [SerializeField] private Button whispererPlayButton;
    [SerializeField] private Button backButton;

    public void Open(string seed, PlayerType playerType, UnityAction onPlayClicked)
    {
        gameObject.SetActive(true);
        comedianText.gameObject.SetActive(playerType == PlayerType.Catmedian);
        whispererText.gameObject.SetActive(playerType == PlayerType.CatWhisperer);
        seedTextArea.text = seed;
        seedTextArea.readOnly = playerType == PlayerType.Catmedian;
        comedianPlayButton.onClick.SetListener(onPlayClicked);
        whispererPlayButton.onClick.SetListener(onPlayClicked);
        comedianPlayButton.gameObject.SetActive(playerType == PlayerType.Catmedian);
        whispererPlayButton.gameObject.SetActive(playerType == PlayerType.CatWhisperer);
        backButton.onClick.SetListener(Close);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public string GetSeed()
    {
        return seedTextArea.text;
    }
}