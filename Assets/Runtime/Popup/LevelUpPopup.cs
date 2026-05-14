using TMPro;
using UnityEngine;

public class LevelUpPopup : BasePopup {

    [SerializeField] Button2D menuButton;
    [SerializeField] Button2D nextLevelButton;
    [SerializeField] Button2D replayButton;
    [SerializeField] TextMeshPro winAmountTMP;  //Amount won on Level Up...
    [SerializeField] TextMeshPro levelTMP;  //New Reached Level...

    private LevelUpPopup Instance;

    #region System Methods
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            menuButton.SetTapCallback(onMenuButtonTapped);
            nextLevelButton.SetTapCallback(onNextLevelButtonTapped);
            replayButton.SetTapCallback(onReplayButtonTapped);
        }
    }
    #endregion

    #region LevelUpPopup Methods
    public void showPopup(bool isPortrait) {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.LevelUpPopup);
        EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOff, gameObject);
        gameObject.SetActive(true);
        AnimatePopupIn();
    }

    public void hidePopup(PopupButtons button) {
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.LevelUpPopup);
        EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOn, gameObject);
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
            if (button == PopupButtons.Replay) {
                EventManager.triggerEvent(AppEventsId.LevelUpPopupReplayGame, gameObject);
            } else if (button == PopupButtons.Next) {
                EventManager.triggerEvent(AppEventsId.LevelUpPopupNextLevel, gameObject);
            }
        });
    }

    public void destroyPopup() {
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.LevelUpPopup);
        EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOn, gameObject);
        AnimatePopupOut(() => Destroy(gameObject));
    }

    private void closePopup(PopupButtons button) {
        hidePopup(button);        
    }

    public void setWinAmountText(string winText) {
        levelTMP.text = winText;
    }

    public void setLevelText(string levelText) {
        levelTMP.text = levelText;
    }
    #endregion

    #region Event Methods
    private void onMenuButtonTapped() {
        Debug.Log("LevelUpPopup.onMenuButtonTapped()...");
        AudioPlayer.PlayTapSound();
        closePopup(PopupButtons.None);
    }

    private void onNextLevelButtonTapped() {
        Debug.Log("LevelUpPopup.onNextLevelButtonTapped()...");        
        AudioPlayer.PlayTapSound();        
        closePopup(PopupButtons.Next);
    }

    private void onReplayButtonTapped() {
        Debug.Log("LevelUpPopup.onReplayButtonTapped()...");
        EggGameDataManager.UpdateLevelBy(-1);   //As current level has already been increased so to play last level again reduce current level by 1;        
        AudioPlayer.PlayTapSound();
        closePopup(PopupButtons.Replay);
    }
    #endregion
}