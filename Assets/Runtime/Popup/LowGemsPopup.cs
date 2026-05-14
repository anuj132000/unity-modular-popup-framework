using UnityEngine;
using TMPro;

public class LowGemsPopup : BasePopup
{
    [Header("UI References")]
    //[SerializeField] private TextMeshPro titleTMP;

    [SerializeField] private Button2D watchAdButton;
    [SerializeField] private Button2D buyGemsButton;
    [SerializeField] private Button2D cancelButton;
    [SerializeField] private Button2D closeButton;
    [SerializeField] private TextMeshPro movesOnAdTMP;

    [Header("Ad Reward Config")]
    [SerializeField] private int movesOnAd = 1;

    //private int requiredGems;
    //private int userGems;
    private bool IsForMove;

    private void Awake()
    {
        watchAdButton.SetTapCallback(OnWatchAd);
        buyGemsButton.SetTapCallback(OnBuyGems);
        cancelButton.SetTapCallback(ClosePopup);
        closeButton.SetTapCallback(ClosePopup);
    }

    private void OnEnable()
    {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.NoPopup, PopupsQueueManager.PopupGroup.GAME);
    }

    private void OnDisable()
    {
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.NoPopup);
    }

    public void Initialize(bool isForMove)
    {
        gameObject.SetActive(true);
        IsForMove = isForMove;
        //requiredGems = required;
        //userGems = current;
        //userGems = GemsManager.Instance.GetCurrentGems();

        //UpdatePopupText();
        if (IsForMove)
            movesOnAdTMP.text = $"<b>{movesOnAd}</b> {(movesOnAd > 1 ? "MOVES" : "MOVE")}";
        else
            movesOnAdTMP.text = "1 Holder";

        AnimatePopupIn();
    }

    //private void UpdatePopupText()
    //{
    //    titleTMP.text = "Not Enough";
    //    deficitInfoTMP.text =
    //        $"You need <b>{requiredGems}</b> gems, but you only have <b>{userGems}</b>.";
    //}

    private void OnWatchAd()
    {
        AdsManager.Instance.ShowRewardedAd(
        onRewarded: () =>
        {
            Debug.Log("Player earned reward!");
            OnAdCompleted();
        },
        onFailed: () =>
        {
            Debug.Log("Rewarded Ad failed or skipped → Not added Gems");
        });
    }

    private void OnAdCompleted()
    {
        if (IsForMove)
            GrantExtraMoves();
        else
            GrantExtraContainer();
    }

    private void GrantExtraMoves()
    {
        ClosePopup();
        EventManager.triggerEvent(ItemSortGameEvents.ExtraMovesGrantedEvent.ToString(), gameObject, movesOnAd);
    }

    private void GrantExtraContainer()
    {
        ClosePopup();
        EventManager.triggerEvent(ItemSortGameEvents.AddTubeReqestEvent.ToString(), gameObject);
    }

    private void OnBuyGems()
    {
        EventManager.triggerEvent(ItemSortGameEvents.BuyGemsReqestEvent.ToString(), null);
        gameObject.SetActive(false);

    }

    private void ClosePopup()
    {
        AnimatePopupOut(() => {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
        });
    }
}
