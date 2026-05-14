using UnityEngine;
using TMPro;

public class YouWonPopup : BasePopup {

    [SerializeField] Button2D collectButton;
    [SerializeField] TextMeshPro winAmountTMP;

    private YouWonPopup Instance;  

    #region System Methods
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            collectButton.SetTapCallback(onCollectButtonTapped);           
        }
    }
    #endregion

    #region YouWonPopup Methods
    public void showPopup() {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.YouWonPopup);
        gameObject.SetActive(true);
    }

    public void hidePopup() {
        gameObject.SetActive(false);
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.YouWonPopup);
    }

    public void destroyPopup() {
        Destroy(gameObject);
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.YouWonPopup);
    }

    private void closePopup() {        
        hidePopup();
        closeActionCB?.Invoke();
    }
    #endregion

    #region Event Methods
    private void onCollectButtonTapped() {
        Debug.Log("YouWonPopup.onCollectButtonTapped()...");
        EventManager.triggerEvent(AppEventsId.YouWonPopupCollectCoins, gameObject);
        AudioPlayer.PlayTapSound();
        closePopup();
    }
    #endregion
}