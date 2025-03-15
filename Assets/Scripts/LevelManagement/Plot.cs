using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    public SprayBottle sprayBottle;
    public LaserPointer laserPointer;

    private Color startColor;
    private AudioSource audioSource;

    private void Start()
    {
        startColor = sr.color;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Resources.Load <AudioClip> ("Audio/Menu Sounds/Place Tower");
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
        // Dont allow the player to click other plots if the mouse is over the UI
        if (UIManager.Instance.IsHoveringUI()) return;


        Tower towerToBuild = CraftedItemTracker.main.GetSelected();

        // Theres a tower on this plot already
        if (tower != null)
        {
            if (tower.GetComponent<SprayBottle>())
            {
                sprayBottle.OpenSellUI();
            } else if (tower.GetComponent<LaserPointer>())
            {
                laserPointer.OpenSellUI();
            }
            return;
        }

        //Debug.Log("Crafted right now: " + CraftedItemTracker.main.GetCrafted(towerToBuild.name));
        //Debug.Log("Building: " + towerToBuild.name);

        // If we have the crafted item...
        if (CraftedItemTracker.main.craftedItems.ContainsKey(towerToBuild.name))
        {
            int amt = CraftedItemTracker.main.GetCrafted(towerToBuild.name);
            // Check if you have enough of those towers to be able to place.
            if (amt >= 1)
            {
                //Debug.Log("Crafted Before plot: " + CraftedItemTracker.main.GetCrafted(towerToBuild.name));


                // Only allow everything but the scratch post on this plot
                if (towerToBuild.name != "ScratchPost")
                {
                    tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
                    audioSource.Play();

                    if (towerToBuild.name == "SprayBottle")
                    {
                        sprayBottle = tower.GetComponent<SprayBottle>();
                    } else if (towerToBuild.name == "LaserPointer")
                    {
                        laserPointer = tower.GetComponent<LaserPointer>(); 
                    }
                    CraftedItemTracker.main.SetCrafted(towerToBuild.name, CraftedItemTracker.main.GetCrafted(towerToBuild.name) - 1);
                }

           
                //Debug.Log("Crafted After plot: " + CraftedItemTracker.main.GetCrafted(towerToBuild.name));
            }
        }
        


       
    }
        


}
