using UnityEngine;

public class GamePausePopup : BasePopup {

    [SerializeField] Button2D playButton;

    private GamePausePopup Instance;

    #region System Methods
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            playButton.SetTapCallback(onPlayButtonTapped);
        }
    }
    #endregion

    #region GamePausePopup Methods
    public void showPopup() {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.GamePausePopup);
        gameObject.SetActive(true);
        setScaleToOne();
    }

    public void hidePopup() {
        Time.timeScale = 1;
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
            PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.GamePausePopup);
            
        });        
    }

    public void destroyPopup() {
        AnimatePopupOut(() => {
            Destroy(gameObject);
            closeActionCB?.Invoke();
            PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.GamePausePopup);
        });
    }

    private void closePopup() {
        hidePopup();
    }
    #endregion

    #region Event Methods
    private void onPlayButtonTapped() {
        Debug.Log("GamePausePopup.onPlayButtonTapped()...");
        AudioPlayer.PlayTapSound();
        closePopup();
    }
    #endregion
}
