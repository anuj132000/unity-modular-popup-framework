using UnityEngine;
using DG.Tweening;

public class ArrowMover : MonoBehaviour {

    [SerializeField] private Transform arrowTransform;
    [SerializeField] private Transform leftTarget;
    [SerializeField] private Transform rightTarget;

    [SerializeField] private bool onLoadStart = false;
    [SerializeField] private float moveDuration = 0.6f;
    [SerializeField] private Ease moveEase = Ease.InOutSine;

    [Header("Punch Animation")]
    [SerializeField] private float punchStrength = 0.15f;
    [SerializeField] private float punchDuration = 0.25f;
    [SerializeField] private int vibrato = 8;
    [SerializeField] private float elasticity = 0.8f;

    private Sequence moveSequence;
    private Vector3 originalScale;

    public float CurrentArrowX => arrowTransform.position.x;

    private void Start() {
        originalScale = transform.localScale;
        if (onLoadStart) {
            StartMovement(true);
        }
    }

    public void StartMovement() {
        moveSequence?.Kill();

        // Step 1: Random start position
        float randomX = Random.Range(leftTarget.position.x, rightTarget.position.x);
        arrowTransform.position = new Vector3(randomX, arrowTransform.position.y, arrowTransform.position.z);

        // Step 2: Random direction
        bool startToRight = Random.value > 0.5f;
        Transform firstTarget = startToRight ? rightTarget : leftTarget;
        Transform secondTarget = startToRight ? leftTarget : rightTarget;

        // Step 3: Calculate distance-based duration
        float fullDistance = Mathf.Abs(rightTarget.position.x - leftTarget.position.x);
        float partialDistance = Mathf.Abs(randomX - firstTarget.position.x);
        float initialDuration = moveDuration * (partialDistance / fullDistance);

        // Step 4: Tween to firstTarget at matching speed
        arrowTransform.DOMoveX(firstTarget.position.x, initialDuration)
            .SetEase(moveEase)
            .OnComplete(() => {
                moveSequence = DOTween.Sequence()
                    .Append(arrowTransform.DOMoveX(secondTarget.position.x, moveDuration).SetEase(moveEase))
                    .Append(arrowTransform.DOMoveX(firstTarget.position.x, moveDuration).SetEase(moveEase))
                    .SetLoops(-1, LoopType.Yoyo);
            });
    }

    public void StartMovement(bool onLoad) {
        moveSequence?.Kill();
        arrowTransform.position = new Vector3(leftTarget.position.x, arrowTransform.position.y, arrowTransform.position.z);
        moveSequence = DOTween.Sequence()
            .Append(arrowTransform.DOMoveX(rightTarget.position.x, moveDuration).SetEase(moveEase))
            .Append(arrowTransform.DOMoveX(leftTarget.position.x, moveDuration).SetEase(moveEase))
            .SetLoops(-1, LoopType.Yoyo);
    }

    public void StopMovement() {
        moveSequence?.Kill();
    }

    public void SnapArrowToPosX(float targetPosX) {
        transform.position = new Vector3(targetPosX, transform.position.y, transform.position.z);
        PlaySnapPunchScale();
    }

    private void PlaySnapPunchScale() {
        transform.DOKill();
        Vector3 punch = originalScale * punchStrength;
        transform.DOPunchScale(punch, punchDuration, vibrato, elasticity);
    }
}