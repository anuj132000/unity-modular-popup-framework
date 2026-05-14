
using System;
using UnityEngine;

public class AdsNotAvailablePopup : BasePopup
{
    [SerializeField] private Button2D closeButton, continueBtn;
    public Action OnContClickedEvent;

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
        continueBtn.SetTapCallback(closePopup);
    }

    public override void showPopup()
    {
        base.showPopup();
        gameObject.SetActive(true);
        setInitialScale();
        AnimatePopupIn();
    }

    private void setInitialScale()
    {
        float newScale = AppUtill.IsScreenPortrait ? 0.7f : 1f;
        childsHolder.localScale = Vector3.one * newScale;
    }

    private void closePopup()
    {
        AudioPlayer.PlayTapSound();
        hidePopup();
    }

    private void hidePopup()
    {
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            OnContClickedEvent?.Invoke();            
        });
    }

}
