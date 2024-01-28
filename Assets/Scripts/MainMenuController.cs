using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button commedianButton;
    public Button whispererButton;
    public Button commedianClickButton;
    public Button whispererClickButton;
    public Button shareButton;
    public Image shareQRImage;

    public void Start()
    {
        commedianButton.onClick.SetListener(() => LoadScene(PlayerType.Catmedian));
        whispererButton.onClick.SetListener(() => LoadScene(PlayerType.CatWhisperer));
        commedianClickButton.onClick.SetListener(() => SwitchCatButton(PlayerType.Catmedian));
        whispererClickButton.onClick.SetListener(() => SwitchCatButton(PlayerType.CatWhisperer));
        //shareButton.onClick.SetListener(() => OpenSharePopup());

        PurrfectAudioManager.Instance.StartMainMenuMusic();
    }

    public void LoadScene(PlayerType playerType)
    {
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
            commedianButton.gameObject.SetActive(true);
            whispererButton.gameObject.SetActive(false);
        }
        else if (playerType == PlayerType.CatWhisperer)
        {
            commedianButton.gameObject.SetActive(false);
            whispererButton.gameObject.SetActive(true);
        }
    }

    private void OpenSharePopup()
    {
        shareQRImage.gameObject.SetActive(true);
        shareQRImage.gameObject.GetComponentInChildren<Button>().onClick.SetListener(() => CloseSharePopup());
    }

    private void CloseSharePopup()
    {
        shareQRImage.gameObject.SetActive(false);
    }
}
