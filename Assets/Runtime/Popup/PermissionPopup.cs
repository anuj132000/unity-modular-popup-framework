using UnityEngine;

public class PermissionPopup : BasePopup
{
    [SerializeField] private Button2D privacyButton;
    [SerializeField] private Button2D termsButton;
    [SerializeField] private Button2D acceptButton;

    void Start()
    {
        setupButtons();
    }

    public void CheckForPrivacy()
    {
        if (PlayerPrefs.GetInt("PrivacyAccepted", 0) == 0)
            ShowPopup();
        else
            DestroyPopup();
    }

    private void OnEnable()
    {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.SettingsPopup);
    }

    private void OnDisable()
    {
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.SettingsPopup);
    }

    private void setupButtons()
    {
        if (privacyButton != null)
            privacyButton.SetTapCallback(() => Application.OpenURL("https://sites.google.com/view/puzzlemaniastudio/privacy-policy"));

        if (termsButton != null)
            termsButton.SetTapCallback(() => Application.OpenURL("https://sites.google.com/view/puzzlemaniastudio/terms-of-service"));

        if (acceptButton != null)
            acceptButton.SetTapCallback(OnAcceptClicked);
    }

    private void ShowPopup()
    {
        gameObject.SetActive(true);
    }

    private void OnAcceptClicked()
    {
        AudioPlayer.PlayTapSound();
        PlayerPrefs.SetInt("PrivacyAccepted", 1);
        PlayerPrefs.Save();
        DestroyPopup();
    }

    private void DestroyPopup()
    {
        Destroy(gameObject);
    }
}
