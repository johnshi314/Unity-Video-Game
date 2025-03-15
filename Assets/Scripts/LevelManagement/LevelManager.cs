using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    public static LevelManager main;
    
    public Transform[] path;
    public DrawingPlots[] drawingPlots;
    public Transform startPoint;
    public int currency;
    public int lives = 10;
    public int maxWaves = 2;


    public int GetPlotIndex(DrawingPlots plot)
    {   
        int i = 0;
        foreach(DrawingPlots eachPlot in drawingPlots)
        {           
            if (drawingPlots[i] == plot)
            {
                return i; 
            }
            else
                i++;
        }
        return -1; 
    }

    private void Awake()
    {
        main = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currency = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  /*  public void WaveCompleted(int waveNumber)
    {
        if (waveNumber >=  maxWaves) {
            SceneManager.LoadScene("YouWin");
        }
    }*/

    public void TakeLives(int _lives)
    {
        lives -= _lives;

        if (lives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            // Buy item
            currency -= amount;
            return true;
        } else
        {
            Debug.Log("You don't have enough money");
            return false;
        }
    }
    public bool SpendAvailable(int amount) {
        return currency >= amount;
    }
}
