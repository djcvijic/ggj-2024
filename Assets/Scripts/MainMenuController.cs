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
        commedianButton.onClick.AddListener(() => LoadScene(PlayerType.Catmedian));
        whispererButton.onClick.AddListener(() => LoadScene(PlayerType.CatWhisperer));
        shareButton.onClick.AddListener(() => OpenSharePopup());
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
