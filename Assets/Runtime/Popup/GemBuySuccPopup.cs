using System;
using TMPro;
using UnityEngine;

public class GemBuySuccPopup : BasePopup
{
    [SerializeField] TextMeshPro rewardedAmtTMP;
    [SerializeField] private Button2D thanksButton, crossBtn;

    private void Awake()
    {
        thanksButton.SetTapCallback(OnThanksClicked);
        crossBtn.SetTapCallback(ClosePopup);
        
    }

    private void OnEnable()
    {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.SettingsPopup);
    }

    private void OnDisable()
    {
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.SettingsPopup);
    }

    internal void Initialize(int winGems)
    {
        rewardedAmtTMP.text = winGems.ToString();
        AnimatePopupIn();

        GemsManager.Instance.AddGems(winGems);
    }

    private void OnThanksClicked()
    {
        ClosePopup();
        
    }

    private void ClosePopup()
    {
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
        });
    }
}
