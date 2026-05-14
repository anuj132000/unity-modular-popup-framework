using System;
using System.Collections;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private LevelWinPopup levelWinPopupPref;
    [SerializeField] private LevelLoosePopup levelLoosePopupPref;
    [SerializeField] private QuitLevelPopup quitLevelPopupPref;
    [SerializeField] private LowGemsPopup lowGemsPopupPref;
    [SerializeField] private GemsStorePopup gemsStorePopupPref;
    [SerializeField] private GemBuySuccPopup gemsBuySuccPopupPref;

    private LevelWinPopup levelWinPopup;
    private LevelLoosePopup levelLoosePopup;
    private QuitLevelPopup quitLevelPopup;
    private LowGemsPopup lowGemsPopup;
    private GemsStorePopup gemsStorePopup;
    private GemBuySuccPopup gemsBuySuccPopup;

    void Start()
    {
        RegisterEvents();
    }

    private void OnDestroy()
    {
        UnRegisterEvents();
    }

    private void RegisterEvents()
    {
        EventManager.startListening(ItemSortGameEvents.OutOfMoveEvent.ToString(), OnLevelLoose);
        EventManager.startListening(ItemSortGameEvents.LevelCompleteEvent.ToString(), OnLevelComplete);
        EventManager.startListening(ItemSortGameEvents.QuitLevelEvent.ToString(), OnQuitLevel);
        EventManager.startListening(ItemSortGameEvents.LowGemsEvent.ToString(), OnLowGems);
        EventManager.startListening(ItemSortGameEvents.BuyGemsReqestEvent.ToString(), OnReqBuyGems);
        EventManager.startListening(ItemSortGameEvents.BuyGemsSuccEvent.ToString(), OnBuyGemsSucc);
    }

    private void UnRegisterEvents()
    {
        EventManager.stopListening(ItemSortGameEvents.OutOfMoveEvent.ToString(), OnLevelLoose);
        EventManager.stopListening(ItemSortGameEvents.LevelCompleteEvent.ToString(), OnLevelComplete);
        EventManager.stopListening(ItemSortGameEvents.QuitLevelEvent.ToString(), OnQuitLevel);
        EventManager.stopListening(ItemSortGameEvents.LowGemsEvent.ToString(), OnLowGems);
        EventManager.stopListening(ItemSortGameEvents.BuyGemsReqestEvent.ToString(), OnReqBuyGems);
        EventManager.stopListening(ItemSortGameEvents.BuyGemsSuccEvent.ToString(), OnBuyGemsSucc);
    }

    private void OnLevelLoose(GameObject senderObj, object data)
    {
        ItemSortGameLevelData levelData = data as ItemSortGameLevelData;
        StartCoroutine(ShowLevelLoosePopup(levelData));
    }

    private IEnumerator ShowLevelLoosePopup(ItemSortGameLevelData levelData)
    {
        yield return new WaitForSeconds(0.5f);
        if(levelLoosePopup==null)
            levelLoosePopup = PopupSpawner.SpawnPopup(levelLoosePopupPref, transform) as LevelLoosePopup;
        levelLoosePopup.Initialize(levelData);
    }

    private void OnLevelComplete(GameObject obj, object data)
    {
        ItemSortGameLevelData levelData = data as ItemSortGameLevelData;
        StartCoroutine(ShowLevelCompletePopup(levelData));
    }

    private IEnumerator ShowLevelCompletePopup(ItemSortGameLevelData levelData)
    {
        yield return new WaitForSeconds(0.5f);
        if(levelWinPopup == null)
            levelWinPopup = PopupSpawner.SpawnPopup(levelWinPopupPref, transform) as LevelWinPopup;
        levelWinPopup.Initialize(levelData);
    }

    private void OnQuitLevel(GameObject senderObj)
    {
        if(quitLevelPopup == null)
            quitLevelPopup = PopupSpawner.SpawnPopup(quitLevelPopupPref, transform) as QuitLevelPopup;
        quitLevelPopup.Initialize();
    }

    private void OnLowGems(GameObject senderObj, object isForMove)
    {
        if (lowGemsPopup == null)
            lowGemsPopup = PopupSpawner.SpawnPopup(lowGemsPopupPref, transform) as LowGemsPopup;
        lowGemsPopup.Initialize(isForMove: (bool)isForMove);
    }

    private void OnReqBuyGems(GameObject senderObj)
    {
        if (gemsStorePopup == null)
            gemsStorePopup = PopupSpawner.SpawnPopup(gemsStorePopupPref, transform) as GemsStorePopup;
        gemsStorePopup.Initialize();
    }

    private void OnBuyGemsSucc(GameObject senderObj, object winGems)
    {
        if (gemsBuySuccPopup == null)
            gemsBuySuccPopup = PopupSpawner.SpawnPopup(gemsBuySuccPopupPref, transform) as GemBuySuccPopup;
        gemsBuySuccPopup.Initialize((int)winGems);
    }

}
