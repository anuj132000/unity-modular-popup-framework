using System;
using UnityEngine;

public class LevelLoosePopup : BasePopup
{
    [Header("UI References")]
    [SerializeField] private Button2D watchAdButton;
    [SerializeField] private Button2D menuButton;
    [SerializeField] private Button2D replayButton;

    [Header("Config")]
    [SerializeField] private int extraMoves = 3;

    //private const float GemsDiscountMultiplier = 0.5f;
    //private int gemCost;
    private ItemSortGameLevelData currLevelData;

    private void Awake()
    {
        // Button bindings
        watchAdButton.SetTapCallback(OnWatchAdClicked);
        menuButton.SetTapCallback(OnMenuClicked);
        replayButton.SetTapCallback(OnReplayClicked);
    }

    private void OnEnable()
    {
        // Mark popup as active
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.YouLosePopup, PopupsQueueManager.PopupGroup.GAME);
    }

    private void OnDisable()
    {
        // Safety: Ensure removal in all cases
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.YouLosePopup);
    }

    internal void Initialize(ItemSortGameLevelData data)
    {
        gameObject.SetActive(true);
        currLevelData = data;
        //gemCost = (int)(gemsRequired * extraMoves * GemsDiscountMultiplier);
        AnimatePopupIn();
    }

    private void OnMenuClicked()
    {
        ClosePopup();
        EventManager.triggerEvent(ItemSortGameEvents.BackToMenuEvent.ToString(), gameObject);
    }

    private void OnReplayClicked()
    {
        ClosePopup();
        //EventManager.triggerEvent(eventName: ItemSortGameEvents.LevelSelectedEvent.ToString(), null, (object)currLevelData);
        Action proceedToRetry = () =>
        {
            EventManager.triggerEvent(ItemSortGameEvents.LevelSelectedEvent.ToString(), null, (object)currLevelData);
        };

        AdsManager.Instance?.TryShowInterstitial(proceedToRetry);
    }

    private void OnWatchAdClicked()
    {
        AdsManager.Instance.ShowRewardedAd(
        onRewarded: () =>
        {
            Debug.Log("Player earned reward!");
            GrantExtraMoves();
        },
        onFailed: () =>
        {
            Debug.Log("Rewarded Ad failed or skipped → no extra moves");
        });
    }

    private void GrantExtraMoves()
    {
        ClosePopup();
        EventManager.triggerEvent(ItemSortGameEvents.ExtraMovesGrantedEvent.ToString(), gameObject, extraMoves);
    }

    private void ClosePopup()
    {
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
        });
    }
}
