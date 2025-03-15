using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ScratchPost : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    private int catsDistracted;

    void Start()
    {

        catsDistracted = 0;
    }

    public int getCatsDistracted()
    {
        return this.catsDistracted;
    }
    public void setCatsDistracted()
    {
        if (getCatsDistracted() < 4)
            catsDistracted++;
        else
            catsDistracted = 0;
    }






}