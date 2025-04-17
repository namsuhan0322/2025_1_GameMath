using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedGame : MonoBehaviour
{
    [SerializeField] float critChance = 0.2f;
    [SerializeField] float meanDamage = 20f;
    [SerializeField] float stdDevDamage = 5f;
    [SerializeField] float enemyHP = 100f;
    [SerializeField] float poissonLambda = 2f;              // Ǫ�Ƽ� ����
    [SerializeField] float hitRate = 0.6f;
    [SerializeField] float critDamageRate = 2f;
    [SerializeField] int maxHitsPerTurn = 5;                // ���� ��ȣ
    [SerializeField] float defaultRare = 0.2f;
    [SerializeField] float rareProbability = 0.1f;
    
    // ���� ���
    public Text totalTurn;
    public Text totalEnemy;
    public Text enemyKilled;
    public Text attackHit;
    public Text critical;
    public Text maxDamage;
    public Text minDamage;

    // ȹ���� ������
    public Text potionCount;
    public Text goldCount;
    public Text normalWeaponCount;
    public Text rareWeaponCount;
    public Text normalArmorCount;
    public Text rareArmorCount;

    int turn = 0;
    bool rareItemObtained = false;

    string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };

    // ���� ��� ���� ����
    int totalEnemyCount = 0;
    int totalKilled = 0;
    int totalHits = 0;
    int totalCrits = 0;
    float maxDmg = float.MinValue;
    float minDmg = float.MaxValue;

    // ������ ȹ�� ����
    int potions = 0;
    int golds = 0;
    int normalWeapons = 0;
    int rareWeapons = 0;
    int normalArmors = 0;
    int rareArmors = 0;

    public void StartSimulation()
    {
        // ���Ϻ��� ���ø�: ���� �������� ���� ������ �ݺ��ϴ� ����
        rareItemObtained = false;
        turn = 0;

        totalEnemyCount = 0;
        totalKilled = 0;
        totalHits = 0;
        totalCrits = 0;
        maxDmg = float.MinValue;
        minDmg = float.MaxValue;

        potions = 0;
        golds = 0;
        normalWeapons = 0;
        rareWeapons = 0;
        normalArmors = 0;
        rareArmors = 0;

        while (!rareItemObtained)
        {
            SimulateTurn();
            turn++;
        }

        UpdateUI();
    }

    void SimulateTurn()
    {
        // Ǫ�Ƽ� ���ø�: �� ���� ��
        int enemyCount = SamplePoisson(poissonLambda);
        totalEnemyCount += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            // ���� ���ø�: ���� Ƚ��
            int hits = SampleBinomial(maxHitsPerTurn, hitRate);
            totalHits += hits;

            float totalDamage = 0f;
            int critCount = 0;

            for (int j = 0; j < hits; j++)
            {
                float damage = SampleNormal(meanDamage, stdDevDamage);

                // �������� ���� ���ø�: Ȯ�� ��� ġ��Ÿ �߻�
                if (Random.value < critChance)
                {
                    damage *= critDamageRate;
                    critCount++;
                }

                totalCrits += (damage >= meanDamage * critDamageRate) ? 1 : 0;

                if (damage > maxDmg) maxDmg = damage;
                if (damage < minDmg) minDmg = damage;

                totalDamage += damage;
            }

            if (totalDamage >= enemyHP)
            {
                totalKilled++;

                // �յ� ���� ���ø�: ���� ����
                string reward = rewards[UnityEngine.Random.Range(0, rewards.Length)];

                float currentRare = Mathf.Clamp01(defaultRare + rareProbability * turn);

                //Debug.Log($"���� Ȯ�� {(currentRare * 100f):F0}%");

                if (reward == "Potion")
                {
                    potions++;
                }
                else if (reward == "Gold")
                {
                    golds++;
                }
                else if (reward == "Weapon")
                {
                    if (Random.value < currentRare)
                    {
                        rareWeapons++;
                        rareItemObtained = true;
                    }
                    else
                    {
                        normalWeapons++;
                    }
                }
                else if (reward == "Armor")
                {
                    if (Random.value < currentRare)
                    {
                        rareArmors++;
                        rareItemObtained = true;
                    }
                    else
                    {
                        normalArmors++;
                    }
                }
            }
        }
    }

    void UpdateUI()
    {
        totalTurn.text = $"�� ���� �� �� : {turn}";
        totalEnemy.text = $"�߻��� �� : {totalEnemyCount}";
        enemyKilled.text = $"óġ�� �� : {totalKilled}";
        attackHit.text = $"���� ���� ��� : {(totalHits * 100f / (maxHitsPerTurn * totalEnemyCount)):F2}%";
        critical.text = $"�߻��� ġ��Ÿ�� ��� : {(totalCrits * 100f / totalHits):F2}%";
        maxDamage.text = $"�ִ� ������ : {maxDmg:F2}";
        minDamage.text = $"�ּ� ������ : {minDmg:F2}";

        potionCount.text = $"���� : {potions}��";
        goldCount.text = $"��� : {golds}��";
        normalWeaponCount.text = $"���� - �Ϲ� : {normalWeapons}��";
        rareWeaponCount.text = $"���� - ���� : {rareWeapons}��";
        normalArmorCount.text = $"�� - �Ϲ� : {normalArmors}��";
        rareArmorCount.text = $"�� - ���� : {rareArmors}��";
    }

    // --- ���� ���� �Լ��� ---
    int SamplePoisson(float lambda)
    {
        int k = 0;
        float p = 1f;
        float L = Mathf.Exp(-lambda);
        while (p > L)
        {
            k++;
            p *= Random.value;
        }
        return k - 1;
    }

    int SampleBinomial(int n, float p)
    {
        int success = 0;
        for (int i = 0; i < n; i++)
            if (Random.value < p) success++;
        return success;
    }

    float SampleNormal(float mean, float stdDev)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return mean + stdDev * z;
    }
}
