using UnityEngine;
using TMPro;

public class InAppFailedPopup : BasePopup
{
    [SerializeField] private Button2D closeButton, crossBtn;
    [SerializeField] private TextMeshPro failReasonTxt;

    private void Start()
    {
        setupButtons();
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
        
        if (closeButton != null)
            closeButton.SetTapCallback(closePopup);
        if (crossBtn != null)
            crossBtn.SetTapCallback(closePopup);

    }

    public void Initialize(string failReason)
    {
        //failReasonTxt.text = failReason;
        gameObject.SetActive(true);
    }

    public void hidePopup()
    {
        gameObject.SetActive(false);
    }

    private void closePopup()
    {
        AudioPlayer.PlayTapSound();
        hidePopup();
    }
}
