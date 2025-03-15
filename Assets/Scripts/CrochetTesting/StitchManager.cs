using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StitchManager : MonoBehaviour
{

    [SerializeField] private List<StitchSlot> slotPrefabs;
    [SerializeField] private Transform slotParent, stitchParent;
    [SerializeField] private StitchPattern patternPrefab;

    private void Start()
    {
        Spawn();
    }


    void Spawn()
    {
        for(int i = 0; i < slotPrefabs.Count; i++)
        {
            var spawnedSlot = Instantiate(slotPrefabs[i],slotParent.GetChild(i).position,Quaternion.identity);

            var spawnedPattern = Instantiate(patternPrefab, stitchParent.GetChild(i).position, Quaternion.identity);
            spawnedPattern.Init(spawnedSlot);
        }
    }
}
