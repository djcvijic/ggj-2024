using RuleSystem;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private const int MinSeed = 100;
    private const int MaxSeed = 999;

    [SerializeField] private Cat comedianCat;
    [SerializeField] private Cat whispererCat;
    public Button commedianButton;
    public Button whispererButton;
    public Button commedianClickButton;
    public Button whispererClickButton;
    public SeedUI seedUI;
    public Button shareButton;
    public Button creditsButton;
    public Image shareQRImage;
    public Image creditsImage;
    public Button closeShare;
    public Button closeCredits;

    public void Start()
    {
        comedianCat.Initialize();
        whispererCat.Initialize();
        commedianClickButton.onClick.SetListener(() => SwitchCatButton(PlayerType.Catmedian));
        whispererClickButton.onClick.SetListener(() => SwitchCatButton(PlayerType.CatWhisperer));
        commedianButton.onClick.SetListener(() => PlayCatButton(PlayerType.Catmedian));
        whispererButton.onClick.SetListener(() => PlayCatButton(PlayerType.CatWhisperer));
        seedUI.Deactivate();
        shareButton.onClick.SetListener(ToggleSharePopup);
        creditsButton.onClick.SetListener(ToggleCreditsPopup);

        PurrfectAudioManager.Instance.StartMainMenuMusic();
    }

    private void SwitchCatButton(PlayerType playerType)
    {
        if (playerType == PlayerType.Catmedian)
        {
            PurrfectAudioManager.Instance.SelectComedian();
            commedianButton.gameObject.SetActive(true);
            whispererButton.gameObject.SetActive(false);
        }
        else if (playerType == PlayerType.CatWhisperer)
        {
            PurrfectAudioManager.Instance.SelectWhisperer();
            commedianButton.gameObject.SetActive(false);
            whispererButton.gameObject.SetActive(true);
        }
    }

    private void PlayCatButton(PlayerType playerType)
    {
        var seed = playerType == PlayerType.Catmedian
            ? Random.Range(MinSeed, MaxSeed + 1).ToString()
            : "";
        seedUI.Initialize(seed, playerType, () => LoadScene(playerType));
    }

    private void LoadScene(PlayerType playerType)
    {
        var seedText = seedUI.GetSeed();
        if (!int.TryParse(seedText, out var seed) || seed is < MinSeed or > MaxSeed)
        {
            // TODO cvile: some fail alert for the player
            return;
        }

        RuleBook.Instance.Shuffle(seedText);
        if (playerType == PlayerType.Catmedian)
        {
            PurrfectSceneManager.LoadScene(SceneName.ComedianScene);
        }
        else if (playerType == PlayerType.CatWhisperer)
        {
            PurrfectSceneManager.LoadScene(SceneName.WhispererScene);
        }
    }

    private void ToggleSharePopup()
    {
        if (!shareQRImage.gameObject.activeSelf)
        {
            PurrfectAudioManager.Instance.FlipPage();
            shareQRImage.gameObject.SetActive(true);
            closeShare.onClick.SetListener(CloseSharePopup);
        }
        else
        {
            CloseSharePopup();
        }
    }

    private void CloseSharePopup()
    {
        PurrfectAudioManager.Instance.FlipPage();
        shareQRImage.gameObject.SetActive(false);
    }

    private void ToggleCreditsPopup()
    {
        if (!creditsImage.gameObject.activeSelf)
        {
            PurrfectAudioManager.Instance.FlipPage();
            creditsImage.gameObject.SetActive(true);
            closeCredits.onClick.SetListener(CloseCreditsPopup);
        }
        else
        {
            CloseCreditsPopup();
        }
    }

    private void CloseCreditsPopup()
    {
        PurrfectAudioManager.Instance.FlipPage();
        creditsImage.gameObject.SetActive(false);
    }
}
