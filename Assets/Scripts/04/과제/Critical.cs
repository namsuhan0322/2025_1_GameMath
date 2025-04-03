using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Critical : MonoBehaviour
{
    public Text criText;

    private int criticalHits = 0;
    public int trials = 100;

    public float criticalChance = 0.1f;
    public bool isCriticalHitLast = false;


    public void Ciriti()
    {
        for (int i = 0; i < trials; i++)
        {
            criticalHits = 0;

            bool isCritical = SimulCritical();

            if (isCritical)
            {
                criticalHits++;
            }

            float percent = (float)criticalHits / trials * 100f;

            Debug.Log($"치명타 발생 : {criticalHits} / {trials}\n 치명타 확률 : {percent:F2}%");

            criText.text = $"치명타 발생 : {criticalHits} / {trials}\n 치명타 확률 : {percent:F2}%";
        }
    }

    private bool SimulCritical()
    {
        if (Random.value < criticalChance)
        {
            isCriticalHitLast = true;
            return true;
        }
        else
        {
            if (!isCriticalHitLast && Random.value < criticalChance)
            {
                isCriticalHitLast = true;
                return true;
            }

            isCriticalHitLast = false;
            return false;
        }
    }
}
