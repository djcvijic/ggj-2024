using RuleSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button commedianButton;
    public Button whispererButton;
    public Button commedianClickButton;
    public Button whispererClickButton;
    public TMP_InputField seedInputField;
    public Button shareButton;
    public Button creditsButton;
    public Image shareQRImage;
    public Image creditsImage;
    public Button closeShare;
    public Button closeCredits;

    public void Start()
    {
        commedianButton.onClick.SetListener(() => LoadScene(PlayerType.Catmedian));
        whispererButton.onClick.SetListener(() => LoadScene(PlayerType.CatWhisperer));
        commedianClickButton.onClick.SetListener(() => SwitchCatButton(PlayerType.Catmedian));
        whispererClickButton.onClick.SetListener(() => SwitchCatButton(PlayerType.CatWhisperer));
        shareButton.onClick.SetListener(() => OpenSharePopup());
        creditsButton.onClick.SetListener(() => OpenCreditsPopup());

        PurrfectAudioManager.Instance.StartMainMenuMusic();
    }

    public void LoadScene(PlayerType playerType)
    {
        RuleBook.Instance.ShuffleRuleBook(seedInputField.text);
        if (playerType == PlayerType.Catmedian)
        {
            SceneManager.LoadScene("SceneCommedian");
        }
        else if (playerType == PlayerType.CatWhisperer)
        {
            SceneManager.LoadScene("SceneWhisperer");
        }
    }

    public void SwitchCatButton(PlayerType playerType)
    {
        if (playerType == PlayerType.Catmedian)
        {
            PurrfectAudioManager.Instance.SelectComedian();
            commedianButton.gameObject.SetActive(true);
            whispererButton.gameObject.SetActive(false);
            seedInputField.gameObject.SetActive(true);
        }
        else if (playerType == PlayerType.CatWhisperer)
        {
            PurrfectAudioManager.Instance.SelectWhisperer();
            commedianButton.gameObject.SetActive(false);
            whispererButton.gameObject.SetActive(true);
            seedInputField.gameObject.SetActive(true);
        }
    }

    private void OpenSharePopup()
    {
        PurrfectAudioManager.Instance.FlipPage();
        shareQRImage.gameObject.SetActive(true);
        closeShare.onClick.SetListener(() => CloseSharePopup());
    }

    private void CloseSharePopup()
    {
        PurrfectAudioManager.Instance.FlipPage();
        shareQRImage.gameObject.SetActive(false);
    }

    private void OpenCreditsPopup()
    {
        PurrfectAudioManager.Instance.FlipPage();
        creditsImage.gameObject.SetActive(true);
        closeCredits.onClick.SetListener(() => CloseCreditsPopup());
    }
    private void CloseCreditsPopup()
    {
        PurrfectAudioManager.Instance.FlipPage();
        creditsImage.gameObject.SetActive(false);
    }
}
