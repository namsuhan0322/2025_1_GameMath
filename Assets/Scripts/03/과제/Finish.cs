using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Finish : MonoBehaviour
{
    public Player player;
    public GameObject gameClearText;

    private void Start()
    {
        gameClearText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.Finish();
            ShowGameClearText();
            Time.timeScale = 0f;
        }
    }

    void ShowGameClearText()
    {
        gameClearText.SetActive(true);
    }
}
