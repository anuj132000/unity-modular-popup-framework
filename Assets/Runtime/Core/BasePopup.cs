using System;
using UnityEngine;
using DG.Tweening;

public class BasePopup : MonoBehaviour {

    public enum PopupButtons {
        None,
        Replay,
        Next,
        WatchAds
    }

    [SerializeField] protected Transform parentContainer;
    [SerializeField] protected Transform childsHolder;

    protected Action closeActionCB;

    private float initialScale = -1f;   //-1f NOT SET;

    public void setFinishCB(Action closeCB) {
        closeActionCB = closeCB;
    }

    public void setPosition(Vector3 newPos) {
        transform.localPosition = newPos;
    }

    public void setParentContainer(Transform container) {
        parentContainer = container;
    }

    protected void AnimatePopupIn() {
        if (childsHolder == null) {
            return;
        }
        initialScale = Mathf.Approximately(initialScale, -1f) ? childsHolder.localScale.x : initialScale;  //Keeping orignal scale only if inital value is -1f(NOT SET); Useful if objects scale is other than 1f;
        setScaleToZero();
        childsHolder.DOScale(initialScale, 0.4f).SetEase(Ease.OutBack);
    }

    protected void AnimatePopupOut(Action OutCallBack = null) {
        if (childsHolder == null) {
            return;
        }
        childsHolder.DOScale(0f, 0.3f).OnComplete(() => { OutCallBack(); });
    }

    protected void setScaleToZero() {
        childsHolder.localScale = Vector3.zero;
    }

    protected void setScaleToOne() {
        childsHolder.localScale = Vector3.one;
    }

    public virtual void showPopup() {
        if (parentContainer) {
            transform.SetParent(parentContainer);
        }
    }
}