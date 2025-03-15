using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Sprite wetSprite;
    [SerializeField] private Sprite confusedSprite;



    [Header("Attributes")]
    [SerializeField] private float hitPoints;
    private float maxLives;
    [SerializeField] private int currencyWorth = 50;
    private GameObject energy;
    private Image energyImage;
    public RussianBlue russianBlue;

    private RussianBlueSpawner waveSpawner;


    private bool isDestoryed = false;
    void Start()
    {

        russianBlue = GetComponent<RussianBlue>();
        waveSpawner = FindAnyObjectByType<RussianBlueSpawner>();
        if (russianBlue != null)
        {
            energy = russianBlue.getEnergyObject();
            GameObject container = energy.transform.GetChild(0).gameObject;
            GameObject energyBar = container.transform.GetChild(0).gameObject;
            energyImage = energyBar.GetComponent<Image>();

        }
        maxLives = hitPoints;

    }
    void Update()
    {

    }
    public void TakeDamage(int damage, string source)
    {

        hitPoints -= damage;
        //Debug.Log("Health left: " + hitPoints);
        energyImage.fillAmount = hitPoints / maxLives;


        // If health is 0, destory the object and give the right amount of currency
        if (hitPoints <= 0 && !isDestoryed)
        {
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestoryed = true;
            RussianBlueSpawner.onEnemyDestory.Invoke();
            Destroy(gameObject);
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        } else
        {
            if (source == "SprayBottle")
            {
                russianBlue.GetComponent<SpriteRenderer>().sprite = wetSprite;
            } else if (source == "LaserPointer")
            {
                russianBlue.GetComponent<SpriteRenderer>().sprite = confusedSprite;
            }

            
        }

        
        //Debug.Log("Enemies left: " + waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft.ToString());
    }


    public float currentHealth()
    {
        return hitPoints;
    }
}