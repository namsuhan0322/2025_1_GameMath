using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityRandomSeed : MonoBehaviour
{
    void Start()
    {
        Random.InitState(1234);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(Random.Range(1, 7));
        }
    }
}
