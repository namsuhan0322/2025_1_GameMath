using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Critical : MonoBehaviour
{
    public Text totalAttacksText;
    public Text criticalHitsText;
    public Text critRateText;
    public Text resultText;

    private int totalAttacks = 0;
    private int criticalHits = 0;

    private const float BASE_CRIT_CHANCE = 10f;

    void Start()
    {
        UpdateUI();
    }

    public void PerformAttack()
    {
        bool isCritical = CalculateCriticalHit();

        totalAttacks++;
        if (isCritical) criticalHits++;

        ShowResult(isCritical);
        UpdateUI();
    }

    bool CalculateCriticalHit()
    {
        int nextTotal = totalAttacks + 1;

        float critRateIfCrit = (criticalHits + 1) / (float)nextTotal * 100f;
        float critRateIfNot = criticalHits / (float)nextTotal * 100f;

        bool forceCrit = critRateIfCrit < BASE_CRIT_CHANCE;
        bool forceNot = critRateIfNot > BASE_CRIT_CHANCE;

        if (forceCrit)
        {
            return true;
        }
        else if (forceNot)
        {
            return false;
        }
        else
        {
            return Random.Range(0f, 100f) < BASE_CRIT_CHANCE;
        }
    }

    void UpdateUI()
    {
        totalAttacksText.text = $"Total Attacks: {totalAttacks}";
        criticalHitsText.text = $"Critical Hits: {criticalHits}";

        if (totalAttacks > 0)
        {
            float currentRate = (float)criticalHits / totalAttacks * 100f;
            critRateText.text = $"Crit Rate: {currentRate:F1}%";
        }
        else
        {
            critRateText.text = "Crit Rate: 0%";
        }
    }

    void ShowResult(bool isCritical)
    {
        resultText.text = isCritical ? "Critical Hit!" : "Normal Hit";
        resultText.color = isCritical ? Color.red : Color.white;

        // Flash effect
        StopAllCoroutines();
        StartCoroutine(FlashResult());
    }

    private IEnumerator FlashResult()
    {
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            resultText.transform.localScale = Vector3.Lerp(
                Vector3.one * 1.2f,
                Vector3.one,
                elapsed / duration
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        resultText.transform.localScale = Vector3.one;
    }
}