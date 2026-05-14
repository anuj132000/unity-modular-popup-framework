using System;
using UnityEngine;

public class PopupSpawner : MonoBehaviourSingleton<PopupSpawner> {

    #region System Defined Methods
    protected override void SingletonAwakened() {
        setMyDontDestroyName();
    }
    #endregion

    #region LoaderView Public  Methods
    public static BasePopup SpawnPopup(BasePopup basePopup) {
        return Instantiate(basePopup);
    }

    public static BasePopup SpawnPopup(BasePopup basePopup, Transform parent) {
        return Instantiate(basePopup, parent);
    }

    public static BasePopup SpawnPopup(BasePopup basePopup, Transform parent, Vector3 position) {
        BasePopup popup = Instantiate(basePopup, parent);
        popup.setParentContainer(parent);
        popup.setPosition(position);        
        return popup;
    }

    public static BasePopup SpawnPopup(BasePopup basePopup, Transform parent, Action finishCB, Vector3 position) {
        BasePopup popup = Instantiate(basePopup, parent);
        popup.setFinishCB(finishCB);
        popup.setPosition(position);
        return popup;
    }
    #endregion

    #region LoaderView Local  Methods
    private void setMyDontDestroyName() {
        gameObject.name = "[" + Instance.GetType().ToString() + "]";
    }
    #endregion
}