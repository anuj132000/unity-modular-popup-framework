using UnityEngine;
using TMPro;
using System;
using System.Collections;

public class YouLosePopup : BasePopup {

    [SerializeField] Button2D watchAdsButton;
    [SerializeField] Button2D replayButton;
    [SerializeField] TextMeshPro bodyTMP;

    private YouLosePopup Instance;
    private const float AnimDuration = 1.0f;
    private const float MinSize = 90f;
    private const float MaxSize = 140f;

    #region System Methods
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            watchAdsButton.SetTapCallback(onWatchAdsButtonTapped);
            replayButton.SetTapCallback(onReplayButtonTapped);            
        }
    }
    #endregion

    #region YouLosePopup Methods
    public void showPopup(string requesterID) {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.YouLosePopup);
        EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOff, gameObject);
        gameObject.SetActive(true);
        setBodyText(requesterID);
        AnimatePopupIn();
    }

    public void hidePopup(PopupButtons button) {
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
            if (button == PopupButtons.Replay) {
                replayTheGame();
            } else if (button == PopupButtons.WatchAds) {                
                showRewardedAdv();
            }
        });
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.YouLosePopup);
        EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOn, gameObject);
    }

    public void destroyPopup() {
        AnimatePopupOut(() => Destroy(gameObject));
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.YouLosePopup);
        EventManager.triggerEvent(AppEventsId.EggTouchToJumpLayerOn, gameObject);
    }

    private void closePopup(PopupButtons button) {        
        hidePopup(button);        
    }
    #endregion

    #region Helper Methods
    private void setBodyText(string requesterID) {
        string originalText = "";
        if (requesterID.Equals(typeof(TimerDownHandler).ToString())) {
            int earnedSeconds = (int)Math.Ceiling(EggGameDataManager.GetTimeLimit() * EggGameDataManager.WatchAdvBenefitPercentage);
            originalText = "Grab <b><size={0}%>" + earnedSeconds.ToString() + "</size></b> more seconds to win this level";
        } else if (requesterID.Equals(typeof(CrashGround).ToString())) {
            int earnedEggs = (int)Math.Ceiling(EggGameDataManager.GetMaxEggs() * EggGameDataManager.WatchAdvBenefitPercentage);
            originalText = "Grab <b><size={0}%>" + earnedEggs.ToString() + "</size></b> more eggs to win this level";
        }
        StartCoroutine(animateSizeTaggedFonts(originalText));
    }

    private void showRewardedAdv() {
        Debug.Log("YouLosePopup.showRewardedAdv()...Player opted to watch the adv!!!...");
        AdsManager.Instance.ShowRewardedAd(
        onRewarded: () => {
            Debug.Log("Player watched the advertisments hence earned reward!!!");
            onRewardedAdvWatched();
        },
        onFailed: () => {
            Debug.Log("Rewarded Ad failed or skipped hence no reward given!!!");
            replayTheGame();
        }, parentContainer);
    }

    private void onRewardedAdvWatched() {
        EventManager.triggerEvent(AppEventsId.YouLosePopupResumeGame, gameObject);
    }

    private void replayTheGame() {
        EventManager.triggerEvent(AppEventsId.YouLosePopupReplayGame, gameObject);
    }

    IEnumerator animateSizeTaggedFonts(string originalText) {
        float t = 0f;
        while (true) {            
            float size = Mathf.Lerp(MinSize, MaxSize, (Mathf.Sin(t * Mathf.PI * 2f) + 1f) / 2f);    // Animate size using a sine wave for a pulsing effect            
            bodyTMP.text = string.Format(originalText, size);   // Update the text with the new size value using rich text tags
            t += Time.deltaTime / AnimDuration;
            if (t > 1.0f) {
                t -= 1.0f;
            }
            yield return null;
        }
    }
    #endregion

    #region Event Methods
    private void onWatchAdsButtonTapped() {
        Debug.Log("YouLosePopup.WatchAdsButtonTapped()...");
        AudioPlayer.PlayTapSound();
        closePopup(PopupButtons.WatchAds);
    }

    private void onReplayButtonTapped() {
        Debug.Log("YouLosePopup.ReplayButtonTapped()...");        
        AudioPlayer.PlayTapSound();
        closePopup(PopupButtons.Replay);
    }
    #endregion
}