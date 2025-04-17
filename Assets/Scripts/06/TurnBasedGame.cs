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
    [SerializeField] float poissonLambda = 2f;              // 푸아송 분포
    [SerializeField] float hitRate = 0.6f;
    [SerializeField] float critDamageRate = 2f;
    [SerializeField] int maxHitsPerTurn = 5;                // 이항 분호
    [SerializeField] float defaultRare = 0.2f;
    [SerializeField] float rareProbability = 0.1f;
    
    // 전투 결과
    public Text totalTurn;
    public Text totalEnemy;
    public Text enemyKilled;
    public Text attackHit;
    public Text critical;
    public Text maxDamage;
    public Text minDamage;

    // 획득한 아이템
    public Text potionCount;
    public Text goldCount;
    public Text normalWeaponCount;
    public Text rareWeaponCount;
    public Text normalArmorCount;
    public Text rareArmorCount;

    int turn = 0;
    bool rareItemObtained = false;

    string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };

    // 전투 결과 집계 변수
    int totalEnemyCount = 0;
    int totalKilled = 0;
    int totalHits = 0;
    int totalCrits = 0;
    float maxDmg = float.MinValue;
    float minDmg = float.MaxValue;

    // 아이템 획득 집계
    int potions = 0;
    int golds = 0;
    int normalWeapons = 0;
    int rareWeapons = 0;
    int normalArmors = 0;
    int rareArmors = 0;

    public void StartSimulation()
    {
        // 기하분포 샘플링: 레어 아이템이 나올 때까지 반복하는 구조
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
        // 푸아송 샘플링: 적 등장 수
        int enemyCount = SamplePoisson(poissonLambda);
        totalEnemyCount += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            // 이항 샘플링: 명중 횟수
            int hits = SampleBinomial(maxHitsPerTurn, hitRate);
            totalHits += hits;

            float totalDamage = 0f;
            int critCount = 0;

            for (int j = 0; j < hits; j++)
            {
                float damage = SampleNormal(meanDamage, stdDevDamage);

                // 베르누이 분포 샘플링: 확률 기반 치명타 발생
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

                // 균등 분포 샘플링: 보상 결정
                string reward = rewards[UnityEngine.Random.Range(0, rewards.Length)];

                float currentRare = Mathf.Clamp01(defaultRare + rareProbability * turn);

                //Debug.Log($"레어 확률 {(currentRare * 100f):F0}%");

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
        totalTurn.text = $"총 진행 턴 수 : {turn}";
        totalEnemy.text = $"발생한 적 : {totalEnemyCount}";
        enemyKilled.text = $"처치한 적 : {totalKilled}";
        attackHit.text = $"공격 명중 결과 : {(totalHits * 100f / (maxHitsPerTurn * totalEnemyCount)):F2}%";
        critical.text = $"발생한 치명타율 결과 : {(totalCrits * 100f / totalHits):F2}%";
        maxDamage.text = $"최대 데미지 : {maxDmg:F2}";
        minDamage.text = $"최소 데미지 : {minDmg:F2}";

        potionCount.text = $"포션 : {potions}개";
        goldCount.text = $"골드 : {golds}개";
        normalWeaponCount.text = $"무기 - 일반 : {normalWeapons}개";
        rareWeaponCount.text = $"무기 - 레어 : {rareWeapons}개";
        normalArmorCount.text = $"방어구 - 일반 : {normalArmors}개";
        rareArmorCount.text = $"방어구 - 레어 : {rareArmors}개";
    }

    // --- 분포 샘플 함수들 ---
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
