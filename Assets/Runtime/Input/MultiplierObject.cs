using TMPro;
using UnityEngine;
using DG.Tweening;

public class MultiplierObject : MonoBehaviour
{
    [Header("Punch Animation")]
    [SerializeField] private float punchStrength = 0.3f;
    [SerializeField] private float punchDuration = 0.4f;
    [SerializeField] private int punchVibrato = 10;
    [SerializeField] private float punchElasticity = 1f;

    [Header("Final Scale")]
    [SerializeField] private float endScaleMultiplier = 1.2f;
    [SerializeField] private Color endDispColor = Color.red;

    private TextMeshPro valueTMP;
    private Vector3 originalScale;
    private Tween currentTween;
    private Color orignalColor;

    private void Awake()
    {
        valueTMP = GetComponentInChildren<TextMeshPro>();
        originalScale = transform.localScale;
        orignalColor = valueTMP.color;
    }

    private void OnEnable()
    {
        ResetVisual();
    }

    public void SetMultiplier(int value)
    {
        valueTMP.text = $"{value}x";
    }

    public void PlaySelectedEffect()
    {
        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();

        Vector3 punch = originalScale * punchStrength; // relative offset from original
        currentTween = transform
            .DOPunchScale(punch, punchDuration, punchVibrato, punchElasticity)
            .OnComplete(() =>
            {
                transform.localScale = originalScale * endScaleMultiplier;
                valueTMP.transform.localScale *= endScaleMultiplier;
            });

        // Flash color effect
        valueTMP.DOColor(endDispColor, 0.15f)
            .SetLoops(3, LoopType.Yoyo); //odd No of loops to stop tween on updated color even for same color
    }

    public void ResetVisual()
    {
        if (currentTween != null && currentTween.IsActive()) currentTween.Kill();
        transform.localScale = originalScale;
        valueTMP.transform.localScale = Vector3.one;
        valueTMP.color = orignalColor;
    }

    //public void ResetColor()
    //{
    //    valueTMP.color = orignalColor;
    //}
}
