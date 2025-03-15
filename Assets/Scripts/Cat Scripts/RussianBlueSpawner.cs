using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RussianBlueSpawner : MonoBehaviour
{

    public static RussianBlueSpawner main;

    [Header("References")]
    public Wave[] waves;


    [Header("Attributes")]
    [SerializeField] private float countdown; // First wave timer
    [SerializeField] private AudioSource audioSource;


    [Header("Events")]
    public static UnityEvent onEnemyDestory = new UnityEvent();



    public int currentWave = 1;
    public Canvas energyCanvas;
    private bool readyToCountDown;


    public int currentWaveIndex = 0;

    private void Awake()
    {
        onEnemyDestory.AddListener(EnemyDestroyed);
        main = this;
    }

    private void Start()
    {
        readyToCountDown = true;

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].enemiesLeft = waves[i].enemies.Length;
            // Debug.Log("Enemies left at: " + i + " " + waves[i].enemiesLeft);
        }

    }


    private void Update()
    {

        // If there is no more waves...
        if (currentWaveIndex >= waves.Length)
        {
            SceneManager.LoadScene("YouWin");
            return;
        }


        // Start count down when ready
        if (readyToCountDown == true)
        {
            countdown -= Time.deltaTime;
        }

        // Debug.Log("Count down: " + countdown);
        // Time to start the next wave
        if (countdown <= 0)
        {
            readyToCountDown = false;

            // Change countdown to the new time for next wave
            countdown = waves[currentWaveIndex].timeToNextWave;
            StartCoroutine(StartWave());
        }

        if (waves[currentWaveIndex].enemiesLeft == 0)
        {
            readyToCountDown = true;

            currentWaveIndex++;
            currentWave++;
        }


    }


    private void EnemyDestroyed()
    {
        audioSource.Play();

    }

    // Start is called before the first frame update
    private IEnumerator StartWave()
    {
        Debug.Log("Spawning Wave");
        if (currentWaveIndex < waves.Length)
        {
            // A loop to spawn every enemy for that wave
            for (int i = 0; i < waves[currentWaveIndex].enemies.Length; i++)
            {
                // Spawn the enemy
                Instantiate(waves[currentWaveIndex].enemies[i], LevelManager.main.startPoint.position, Quaternion.identity, energyCanvas.transform);
                // Then wait a bit to spawn the next enemy
                yield return new WaitForSeconds(waves[currentWaveIndex].timeToNextEnemy);

            }
        }
    }
}

[System.Serializable]
public class Wave
{
    public RussianBlue[] enemies;
    public float timeToNextEnemy;
    public float timeToNextWave;

    [HideInInspector] public int enemiesLeft;
}
