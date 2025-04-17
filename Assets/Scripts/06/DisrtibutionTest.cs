using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisrtibutionTest : MonoBehaviour
{
    public float mean = 50f;
    public float stdDev = 5f;

    public float lambda = 3f;

    public int trials = 10;
    public float chance = 0.3f;

    public float p = 0.1f;

    public int minInclusive = 0;
    public int maxExclusive = 4;

    public void NormalDistribution()
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        float sample =  mean + stdDev * randStdNormal;

        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"Normal Sample {i + 1}: {sample:F2}");
        }
    }

    public void PoissonDistribution()
    {
        int k = 0;
        float p = 1f;
        float L = Mathf.Exp(-lambda);
        while (p > L)
        {
            k++;
            p *= Random.value;
        }

        float count = k - 1;

        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"Minute {i + 1}: {count} events");
        }
    }

    public void BinomialDistribution()
    {
        int successes = 0;
        for (int i = 0; i < trials; i++)
        {
            if (Random.value < chance)
                successes++;
        }

        int result = successes;

        Debug.Log($"Successes out of 10 trials: {result}");
    }

    public void GeometricDistribution()
    {
        int tries = 1;
        while (Random.value >= p)
        {
            trials++;
        }

        int result = trials;

        Debug.Log($"Tries until first success: {result}");
    }

    public void UniformDistribution()
    {
        int result = Random.Range(minInclusive, maxExclusive);

        Debug.Log($"Uniform Sample: {result}");
    }
}
