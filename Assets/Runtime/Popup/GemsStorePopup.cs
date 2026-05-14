using System.Collections.Generic;
using DG.Tweening;
using PuzzleMania.IAP;
using UnityEngine;

public class GemsStorePopup : BasePopup
{
    [SerializeField] private GemPackTile[] gemPackTilesArr;
    [SerializeField] private Button2D closeButton;
    

    private void Awake()
    {
        closeButton.SetTapCallback(CloseStore);
        
    }

    private void OnEnable()
    {
        PopupsQueueManager.AddMyPopupId(PopupsQueueManager.PopupId.SettingsPopup, PopupsQueueManager.PopupGroup.GAME);
    }

    private void OnDisable()
    {
        PopupsQueueManager.RemoveMyPopupId(PopupsQueueManager.PopupId.SettingsPopup);
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateStore();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        AnimatePopupIn();
    }

    private void PopulateStore()
    {
        List<IAPProductItem> packsList = IAPManager.Instance?.GetConsumableProducts(GameKey.ItemSortGame);
        if (packsList?.Count > 0)
        {
            GemPackTile packTile;
            IAPProductItem packItem;
            for (int idx = 0; idx < gemPackTilesArr.Length; ++idx)
            {
                packTile = gemPackTilesArr[idx];
                bool isPackItemAvailable = idx < packsList.Count;
                packTile.gameObject.SetActive(isPackItemAvailable);
                if (isPackItemAvailable)
                {
                    packItem = packsList[idx];
                    packTile.Initialize(packItem);

                    packTile.OnBuyClickedEvent = OnGemPackBuyClicked;
                }  
            }
        }
    }

    private void OnGemPackBuyClicked(GemPackTile clickedTile)
    {
        Debug.Log($"[GemsStorePopup] Closing store after clicking {clickedTile.name}");
        DOVirtual.DelayedCall(0.15f, CloseStore);
    }

    public void CloseStore()
    {
        AnimatePopupOut(() => { gameObject.SetActive(false); });
    }
}
