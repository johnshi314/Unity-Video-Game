using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPlot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;

    private Color startColor;
    private AudioSource audioSource;

    private void Start()
    {
        startColor = sr.color;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Audio/Menu Sounds/Place Tower");
    }


    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;


        Tower towerToBuild = CraftedItemTracker.main.GetSelected();
        //Debug.Log("Crafted right now: " + CraftedItemTracker.main.GetCrafted(towerToBuild.name));
        //Debug.Log("Building: " + towerToBuild.name);
        int amt = CraftedItemTracker.main.GetCrafted(towerToBuild.name);

        // If we have the item in the dictionary...
        if (CraftedItemTracker.main.craftedItems.ContainsKey(towerToBuild.name))
        {
            // Check if you have enough of those towers to be able to place.
            if (amt >= 1)
            {
                //Debug.Log("Crafted Before plot: " + CraftedItemTracker.main.GetCrafted(towerToBuild.name));


                // Only allow the scratch post on this plot
                if (towerToBuild.name == "ScratchPost")
                {
                    tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
                    audioSource.Play();
                    CraftedItemTracker.main.SetCrafted(towerToBuild.name, CraftedItemTracker.main.GetCrafted(towerToBuild.name) - 1);
                }


                //Debug.Log("Crafted After plot: " + CraftedItemTracker.main.GetCrafted(towerToBuild.name));
            }
        }
    }
}
