using UnityEngine;

public class QuitLevelPopup : BasePopup
{
    [SerializeField] private Button2D quitButton;
    [SerializeField] private Button2D resumeButton;
    [SerializeField] private Button2D closeButton, crossBtn;

    private void Awake()
    {
        // Button bindings
        quitButton.SetTapCallback(OnQuitClicked);
        resumeButton.SetTapCallback(OnResumeClicked);

        if (closeButton != null)
            closeButton.SetTapCallback(ClosePopup);
        if (crossBtn != null)
            crossBtn.SetTapCallback(ClosePopup);

    }

    private void OnEnable()
    {
        // Mark popup as active
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.QuitGamePopup, PopupsQueueManager.PopupGroup.GAME);
    }

    private void OnDisable()
    {
        // Safety: Ensure removal in all cases
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.QuitGamePopup);
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        AnimatePopupIn();
    }

    private void OnQuitClicked()
    {
        ClosePopup();
        EventManager.triggerEvent(ItemSortGameEvents.BackToMenuEvent.ToString(), gameObject);
    }

    private void OnResumeClicked()
    {
        ClosePopup();
    }

    private void ClosePopup()
    {
        AnimatePopupOut(()=> {
            gameObject.SetActive(false);
            closeActionCB?.Invoke();
        });
    }
}
