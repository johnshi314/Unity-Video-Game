using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftedItemTracker : MonoBehaviour
{
    public static CraftedItemTracker main;

    public Dictionary<string, int> craftedItems = new Dictionary<string, int>(); // Hashtable to keep track of items. Key is the item, value is the quantity

    public Tower[] towers;
    public int selectedIndex;
    [SerializeField] TextMeshProUGUI sprayBottleUI;
    [SerializeField] TextMeshProUGUI laserPointerUI;
    [SerializeField] TextMeshProUGUI scratchPostUI;


    private void Awake()
    {
        main = this;
    }

    // Set a given crafted items amount
    public void SetCrafted(string key, int value)
    {
        craftedItems[key] = value;
    }

    // Return the amount of that crafted item
    public int GetCrafted(string key)
    {
        return craftedItems[key];
    }

    public Tower GetSelected()
    {
        return towers[selectedIndex];
    }

    public void SetTower(string name)
    {
        Debug.Log("Setting tower: " + name);
        if (name == "SprayBottle")
        {
            selectedIndex = 0;
        } 
        else if( name == "LaserPointer")
        {
            selectedIndex = 1;
        } else if (name == "ScratchPost")
        {
            selectedIndex = 2;
        }
        
    }

    private void OnGUI()
    {

        // Update each of the inventory counts
        if (craftedItems.ContainsKey("SprayBottle"))
        {
            sprayBottleUI.text = craftedItems["SprayBottle"].ToString();
        } else
        {
            sprayBottleUI.text = "0";
        }
       
        if (craftedItems.ContainsKey("LaserPointer"))
        {
            laserPointerUI.text = craftedItems["LaserPointer"].ToString();
        } else
        {
            laserPointerUI.text = "0";
        }

        if (craftedItems.ContainsKey("ScratchPost"))
        {
            scratchPostUI.text = craftedItems["ScratchPost"].ToString();
        } else
        {
            scratchPostUI.text = "0";
        }


        
       
    }
}
