using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] TextMeshProUGUI livesUI;


    private void OnGUI()
    {
        waveUI.text = "Wave: " + RussianBlueSpawner.main.currentWave.ToString();
        livesUI.text = "Lives: " + LevelManager.main.lives.ToString();
    }
}
