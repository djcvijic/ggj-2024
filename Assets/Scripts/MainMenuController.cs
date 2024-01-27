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
        commedianButton.onClick.AddListener(() => LoadScene(PlayerType.Catmedian));
        whispererButton.onClick.AddListener(() => LoadScene(PlayerType.CatWhisperer));
        commedianClickButton.onClick.AddListener(() => SwitchCatButton(PlayerType.Catmedian));
        whispererClickButton.onClick.AddListener(() => SwitchCatButton(PlayerType.CatWhisperer));
        //shareButton.onClick.AddListener(() => OpenSharePopup());
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
        shareQRImage.gameObject.GetComponentInChildren<Button>().onClick.AddListener(() => CloseSharePopup());
    }

    private void CloseSharePopup()
    {
        shareQRImage.gameObject.SetActive(false);
    }
}
