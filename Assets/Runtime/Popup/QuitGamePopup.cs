using System;
using UnityEngine;

public class QuitGamePopup : BasePopup {

    [SerializeField] Button2D yesButton;
    [SerializeField] Button2D noButton;

    private QuitGamePopup Instance;
    private bool isTargetScreenPortrait;

    #region System Methods
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            noButton.SetTapCallback(onNoButtonTapped);
            yesButton.SetTapCallback(onYesButtonTapped);
        }
    }
    #endregion

    #region QuitGamePopup Methods
    public void showPopup(bool isPortrait) {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.QuitGamePopup);        
        isTargetScreenPortrait = isPortrait;
        gameObject.SetActive(true);
        AnimatePopupIn();
        if (EggGameManager.Instance.IsGameRunning) {
            EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOff, gameObject);
        }
    }

    public void hidePopup() {
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
        });
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.QuitGamePopup);
        if (EggGameManager.Instance.IsGameRunning) {
            EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOn, gameObject);
        }
    }

    private void closePopup() {
        hidePopup();        
    }

    private void loadSceneByName() {
        SceneSwitcher.LoadSceneByName(AppConst.AppHomeScene);
    }
    #endregion

    #region Event Methods
    private void onNoButtonTapped() {
        Debug.Log("QuitGamePopup.onNoButtonTapped()...");
        AudioPlayer.PlayTapSound();
        EventManager.triggerEvent(AppEventsId.NoQuitTheGameEvent, gameObject);
        noQuitTheGame();
    }

    private void onYesButtonTapped() {
        Debug.Log("QuitGamePopup.onYesButtonTapped()...");
        AudioPlayer.PlayTapSound();
        EventManager.triggerEvent(AppEventsId.YesQuitTheGameEvent, gameObject);
    }

    public void yesQuitTheGame() {
        LoaderView.StartLoader("", isTargetScreenPortrait);
        SceneSwitcher.SwitchOrientation(isTargetScreenPortrait);
        Invoke(nameof(loadSceneByName), 0.5f);
        closePopup();
    }

    public void noQuitTheGame() {
        closePopup();
    }

    public void quitTheApp() {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    #endregion
}