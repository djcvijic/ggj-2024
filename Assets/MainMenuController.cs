using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public Button commedianButton;
    public Button whispererButton;
    public Button shareButton;
    public Image shareQRImage;

    public void Start()
    {
        commedianButton.onClick.AddListener(() => LoadScene(PlayerType.Commedian));
        whispererButton.onClick.AddListener(() => LoadScene(PlayerType.Whisperer));
        shareButton.onClick.AddListener(() => OpenSharePopup());
    }

    public void LoadScene(PlayerType playerType)
    {
        if (playerType == PlayerType.Commedian)
        {
            SceneManager.LoadScene("SceneCommedian");
        }
        else if (playerType == PlayerType.Whisperer)
        {
            SceneManager.LoadScene("SceneWhisperer");
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
