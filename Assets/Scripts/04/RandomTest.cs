using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    void Start()
    {
        float chance = Random.value;
        int dice = Random.Range(1, 7);

        System.Random sysRand = new System.Random();
        int number = sysRand.Next(1, 7);

        Debug.Log($"Unity Random (Random.Value) : {chance}");
        Debug.Log($"Unity Random (Random.Range) : {dice}");
        Debug.Log($"System Random (next) : {number}");
    }
}
