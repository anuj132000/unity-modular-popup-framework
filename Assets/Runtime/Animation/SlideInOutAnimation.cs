using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SlideInOutAnimation : MonoBehaviour {

    #region Public Methods

    public virtual Tweener showTopInAnim(Transform tran, float duration, bool animChild = false, Ease ease = Ease.OutBack, Action finishCB = null) {
        return showTopBottomInAnim(tran, duration, animChild, ease, 1, finishCB);
    }

    public virtual Tweener closeTopOutAnim(Transform tran, float duration, bool animChild = false, Ease ease = Ease.InBack, Action finishCB = null) {
        return closeTopBottomOutAnim(tran, duration, animChild, finishCB, ease, 1);
    }

    public virtual Tweener showBottomInAnim(Transform tran, float duration, bool animChild = false, Ease ease = Ease.OutBack, Action finishCB = null) {
        return showTopBottomInAnim(tran, duration, animChild, ease, -1, finishCB);
    }

    public virtual Tweener closeBottomOutAnim(Transform tran, float duration, bool animChild = false, Ease ease = Ease.InBack, Action finishCB = null) {
        return closeTopBottomOutAnim(tran, duration, animChild, null, ease, -1, finishCB);
    }

    public virtual Tweener showLeftInAnim(Transform tran, Transform childTran = null, float duration = 0.3f, Ease ease = Ease.OutBack, Action finishCB = null) {
        return showLeftRightInAnim(tran, childTran, duration, ease, -1, finishCB);
    }

    public virtual Tweener closeLeftOutAnim(Transform tran, Transform childTran = null, float duration = 0.3f, Ease ease = Ease.InBack, Action finishCB = null) {
        return closeLeftRightOutAnim(tran, childTran, duration, ease, -1, finishCB);
    }

    public virtual Tweener showRightInAnim(Transform tran, Transform childTran = null, float duration = 0.3f, Ease ease = Ease.OutBack, Action finishCB = null) {
        return showLeftRightInAnim(tran, childTran, duration, ease, 1, finishCB);
    }

    public virtual Tweener closeRightOutAnim(Transform tran, Transform childTran = null, float duration = 0.3f, Ease ease = Ease.InBack, Action finishCB = null) {
        return closeLeftRightOutAnim(tran, childTran, duration, ease, 1, finishCB);
    }

    public virtual Tweener closeFadeOutAnim(Transform tran, Transform childTran, Action closeCB, float duration = 0.1f) {
        Transform tranToAnim = childTran ? childTran : tran;
        CanvasGroup cGroup = tranToAnim.GetComponent<CanvasGroup>();
        if (cGroup != null) {
            return cGroup.DOFade(0, duration).OnComplete(callBack);
        }
        return tranToAnim.GetComponent<Image>().DOFade(0, duration).OnComplete(callBack);

        void callBack() {
            tran.gameObject.SetActive(false);
            closeCB?.Invoke();
        }
    }

    protected virtual Tweener showAnim(Transform tran, bool animChild = true, float scale = 1) {
        Transform tranToAnim = animChild ? tran.GetChild(0) : tran;
        tran.gameObject.SetActive(true);
        tranToAnim.localScale = 0.7f * scale * Vector3.one;
        return tranToAnim.DOScale(Vector3.one * scale, 0.3f).SetEase(Ease.OutBack);
    }

    protected virtual Tweener showAnim(Transform tran, Transform childTran, float scale = 1) {
        Transform tranToAnim = childTran ? childTran : tran;
        tran.gameObject.SetActive(true);
        tranToAnim.localScale = 0.7f * scale * Vector3.one;
        return tranToAnim.DOScale(Vector3.one * scale, 0.3f).SetEase(Ease.OutBack);
    }

    protected virtual void closeAnim(Transform tran, bool animChild = true, Action closeCB = null) {
        Transform tranToAnim = animChild ? tran.GetChild(0) : tran;
        tranToAnim.DOScale(Vector3.one * 0.7f, 0.4f).SetEase(Ease.InBack).OnComplete(() => {
            tran.gameObject.SetActive(false);
            closeCB?.Invoke();
        });
    }

    protected virtual void closeAnim(Transform tran, Transform childTran, Action closeCB = null) {
        Transform tranToAnim = childTran ? childTran : tran;
        tranToAnim.DOScale(Vector3.one * 0.7f, 0.4f).SetEase(Ease.InBack).OnComplete(() => {
            tran.gameObject.SetActive(false);
            closeCB?.Invoke();
        });
    }
    #endregion

    #region Private Methods
    private Tweener showTopBottomInAnim(Transform tran, float duration, bool animChild, Ease ease, int dir, Action finishCB = null) {
        Transform tranToAnim = animChild ? tran.GetChild(0) : tran;
        tran.gameObject.SetActive(true);
        tranToAnim.localPosition = new Vector3(0, 1024 * dir);
        return tranToAnim.DOLocalMoveY(0, duration).SetEase(ease).OnComplete(() => {
            finishCB?.Invoke();
        });
    }

    private Tweener closeTopBottomOutAnim(Transform tran, float duration, bool animChild, Action closeCB, Ease ease, int dir, Action finishCB = null) {
        Transform tranToAnim = animChild ? tran.GetChild(0) : tran;
        return tranToAnim.DOLocalMoveY(1024 * dir, duration).SetEase(ease).OnComplete(() => {
            tran.gameObject.SetActive(false);
            closeCB?.Invoke();
        });
    }

    private Tweener showLeftRightInAnim(Transform tran, Transform childTran, float duration, Ease ease, int dir, Action finishCB = null) {
        Transform tranToAnim = childTran ? childTran : tran;
        tran.gameObject.SetActive(true);
        tranToAnim.localPosition = new Vector3(Screen.width * dir, 0);
        return tranToAnim.DOLocalMoveX(0, duration).SetEase(ease).OnComplete(() => {
            finishCB?.Invoke();
        });
    }

    private Tweener closeLeftRightOutAnim(Transform tran, Transform childTran, float duration, Ease ease, int dir, Action closeCB) {
        Transform tranToAnim = childTran ? childTran : tran;
        return tranToAnim.DOLocalMoveX(Screen.width * dir, duration).SetEase(ease).OnComplete(() => {
            tran.gameObject.SetActive(false);
            closeCB?.Invoke();
        });
    }

    protected virtual Tweener showFadeInAnim(Transform tran, Transform childTran, float duration = 0.1f, Action finishCB = null) {
        Transform tranToAnim = childTran ? childTran : tran;
        tran.gameObject.SetActive(true);
        CanvasGroup cGroup = tranToAnim.GetComponent<CanvasGroup>();
        if (cGroup != null) {
            return cGroup.DOFade(1, duration);
        }
        return tranToAnim.GetComponent<Image>().DOFade(0, duration).From().OnComplete(() => {
            finishCB?.Invoke();
        });
    }
    #endregion
}
