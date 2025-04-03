using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemRandomSeed : MonoBehaviour
{
    void Start()
    {
        System.Random rnad = new System.Random(1234);
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(rnad.Next(1, 7));
        }
    }
}
