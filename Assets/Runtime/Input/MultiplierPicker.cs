using UnityEngine;
using TMPro;
using System;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class MultiplierPicker : MonoBehaviour
{
    [Header("🎯 Core")]
    [SerializeField] private ArrowMover arrowMover;
    [SerializeField] private Transform boxContainer;

    [Header("🎯 UI Elements")]
    [SerializeField] private TMP_Text tapHintText;
    [SerializeField] private Button2D adClaimButton, skipClaimButton, pickerButton;
    [SerializeField] private TMP_Text claimHelperText, adsGemsText, baseGemsText;
    [SerializeField] private GameObject claimButtonsGroup, moverObj;

    //private Button2D pickerButton;
    public event Action<int> OnMultiplierConfirmed;
    private readonly int[] multipliers = { 2, 3, 2, 4, 2, 3, 2 };
    private List<MultiplierObject> multiplierObjects = new();
    private int selectedIndex = -1;
    private bool isStopped = false;
    private int baseReward;

    private void Awake()
    {
        multiplierObjects.Clear();
        foreach (Transform child in boxContainer)
        {
            var obj = child.GetComponent<MultiplierObject>();
            if (obj != null) multiplierObjects.Add(obj);
        }

        //pickerButton = GetComponent<Button2D>();
        //string skipLabel = "<size=120%><b><color=#FFD700>Skip</color></b></size> & Claim base reward";
        
    }

    private void Start()
    {
        for (int i = 0; i < multiplierObjects.Count && i < multipliers.Length; i++)
        {
            multiplierObjects[i].SetMultiplier(multipliers[i]);
        }
        pickerButton.SetTapCallback(HandleUserTap);
        adClaimButton.SetTapCallback(HandleAdClaim);
        skipClaimButton.SetTapCallback(HandleSkipClaim);
    }

    private void OnEnable()
    {
        isStopped = false;
        arrowMover.StartMovement();
        claimButtonsGroup.SetActive(false);
        moverObj.SetActive(true);
        //pickerButton.toggleInteraction(true);
        
        if (tapHintText != null)
            tapHintText.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        arrowMover.StopMovement();
    }

    public void Initialize(int baseWinAmount)
    {
        baseReward = baseWinAmount;
    }

    private void HandleUserTap()
    {
        if (isStopped) return;
        isStopped = true;

        if (tapHintText != null) tapHintText.gameObject.SetActive(false);

        arrowMover.StopMovement();
        //pickerButton.toggleInteraction(false);

        selectedIndex = GetClosestSquareIndex();
        MultiplierObject selectedObj = multiplierObjects[selectedIndex];
        arrowMover.SnapArrowToPosX(selectedObj.transform.position.x);
        selectedObj.PlaySelectedEffect();

        Invoke(nameof(ShowClaimOptions), 1.5f);
        //ShowClaimOptions();
    }

    private int GetClosestSquareIndex()
    {
        float arrowX = arrowMover.transform.position.x;
        int closestIndex = 0;
        float minDistance = float.MaxValue;

        for (int i = 0; i < multiplierObjects.Count; i++)
        {
            float dist = Mathf.Abs(multiplierObjects[i].transform.position.x - arrowX);
            if (dist < minDistance)
            {
                minDistance = dist;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    private void ShowClaimOptions()
    {
        claimButtonsGroup.SetActive(true);
        moverObj.SetActive(false);

        adClaimButton.toggleInteraction(true);
        skipClaimButton.toggleInteraction(true);

        string word = GetMultiplierWord(multipliers[selectedIndex]);
        claimHelperText.text = $"Watch a short ad to {word} your reward";
        //claimBtnText.text = $"Claim {multipliers[selectedIndex]}X";
        adsGemsText.text = $"{multipliers[selectedIndex] * baseReward}";
        baseGemsText.text = baseReward.ToString();
    }

    private string GetMultiplierWord(int multiplier)
    {
        switch (multiplier)
        {
            case 1: return "base reward";
            case 2: return "Double";
            case 3: return "Triple";
            case 4: return "Quadruple";
            default: return $"{multiplier}X";
        }
    }

    private void HandleAdClaim()
    {
        AdsManager.Instance.ShowRewardedAd(
        onRewarded: () =>
        {
            Debug.Log("Player earned reward!");
            AdsManager.Instance.PrintError("Player earned reward!");
            ConfirmMultiplier(multipliers[selectedIndex]);
        },
        onFailed: () =>
        {
            Debug.Log("Rewarded Ad failed or skipped → based gems");
            AdsManager.Instance.PrintError("Rewarded Ad failed or skipped → based gems");
            ConfirmMultiplier(1);
        });
    }

    private void HandleSkipClaim()
    {
        ConfirmMultiplier(1);
    }

    private void ConfirmMultiplier(int multiplier)
    {
        if(multiplier > 1)
        {
            adClaimButton.toggleInteraction(false);
            skipClaimButton.toggleInteraction(false);
            StartCoroutine(WaitForButtonsHide());
        }
        else
            claimButtonsGroup.SetActive(false);
        OnMultiplierConfirmed?.Invoke(multiplier);
    }

    private IEnumerator WaitForButtonsHide()
    {
        yield return new WaitForSeconds(0.5f);
        claimButtonsGroup.SetActive(false);
    }

}