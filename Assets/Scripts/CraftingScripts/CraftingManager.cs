using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using UnityEngine.U2D;


public class CraftingManager : MonoBehaviour
{
    public static CraftingManager main;

    private Item currentItem;
    public Image customCursor;

    public Slot[] craftingSlots;

    public List<Item> itemList;
    public string[] recipes;
    public Tower[] recipeResults;
    public Slot resultSlot;
    [SerializeField] TextMeshProUGUI notEnoughCurrency;

    [Header("Stage Sprites")]
    public Sprite stage1Sprite;
    public Sprite stage2Sprite;
    public Sprite stage3Sprite;
    public Sprite stage4Sprite;

    [Header("Sounds")]

    public AudioClip craftedSound;
    public AudioClip noMoneySound;


    private int selectedTower = 0;
    public int crafted = 0;

    private AudioSource audioSource;


    public void Awake()
    {
        main = this;
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlayCraftedSound()
    {
        audioSource.clip = craftedSound;
        audioSource.Play();
    }

    private void PlayErrorSound()
    {
        audioSource.clip = noMoneySound;
        audioSource.Play();
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            if(currentItem != null)
            {
                customCursor.gameObject.SetActive(false);
                Slot nearestSlot = null;
                float shortestDistance = float.MaxValue;


                // Find the nearest slot to drop the item into
                foreach(Slot slot in craftingSlots)
                {
                    float dist = Vector2.Distance(Input.mousePosition,slot.transform.position);

                    if (dist < shortestDistance)
                    {
                        shortestDistance = dist;
                        nearestSlot = slot;
                    }
                }

                // Drop the item into that slot
                nearestSlot.gameObject.SetActive(true);
                nearestSlot.GetComponent<Image>().sprite = currentItem.GetComponent<Image>().sprite;
                nearestSlot.item = currentItem;
                itemList[nearestSlot.index] = currentItem;

                //Debug.Log("Nearest Slot correct item: " + nearestSlot.correctItem);
                //Debug.Log("CurrentItem.name:" + currentItem.name);

                
               
                currentItem = null;
                CheckForCompletedRecipes();
                
            }
        }
    }

    void ShowSpriteProgression()
    {
        int correctCount = 0;
        resultSlot.gameObject.SetActive(true);

        // For every item, count the amount of correct slots
        foreach (Slot slot in craftingSlots)
        {
            // Check if the slot is not null and if it has an item
            if (slot != null && slot.item != null)
            {
                // Check if the item in the slot matches the correct item
                if (slot.correctItem == slot.item.name)
                {
                    // Increment correctCount if the item is correct
                    correctCount++;
                }
            }
        }

        // Then depending on the correct amount of slots, give the correct stage progression
        switch (correctCount)
        {
            case 1:
                resultSlot.GetComponent<Image>().sprite = stage1Sprite;
                break;
            case 2:
                resultSlot.GetComponent<Image>().sprite = stage2Sprite;
                break;
            case 3:
                resultSlot.GetComponent<Image>().sprite = stage3Sprite;
                break;
            case 4:
                resultSlot.GetComponent<Image>().sprite = stage4Sprite;
                break;
            case 0:
                resultSlot.GetComponent<Image>().sprite = null;
                break;
            default:
                Debug.Log(correctCount);
                break;
        }

    }

    void CheckForCompletedRecipes()
    {
        
        resultSlot.item = null;


        /* I want to be able to check if the player has inputted the right item in the slots one at a time
         * That way, when the player puts in the correct item I can change the image of the result slot to show that it is slowly being built
         * as the player continues to place the correct patterns. 
         */

        ShowSpriteProgression();

        // Build the recipe string
        string currentRecipeString = "";
        foreach (Item item in itemList)
        {
            if (item != null)
            {
                currentRecipeString += item.itemName;
            }
            else
            {
                currentRecipeString += "null";
            }
        }

        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                // Recipe matched, instantiate the prefab and play the sound
                Tower recipeResult = recipeResults[i];
                GameObject towerToBuild = recipeResult.prefab; // Get the prefab from the item
                PlayCraftedSound();


                // Instantiate the prefab
                if (towerToBuild != null && LevelManager.main.SpendAvailable(recipeResult.cost))
                {
                    // Check if we have that item crafted already
                    if (!CraftedItemTracker.main.craftedItems.ContainsKey(recipeResult.name))
                    {
                        // If we don't, add it to the table with a value of 1
                        CraftedItemTracker.main.craftedItems.Add(recipeResult.name,1);
                    } else
                    {
                        // Otherwise just increment the value by one.
                        CraftedItemTracker.main.SetCrafted(recipeResult.name, CraftedItemTracker.main.GetCrafted(recipeResult.name) + 1);
                    }

                    // Debug.Log("Selected Tower: " + GetSelectedTower().name);
                    Debug.Log("Crafted after Crafting: " + CraftedItemTracker.main.GetCrafted(recipeResult.name));


                    // Debug.Log("Instantiating prefab: " + towerToBuild.name);
                    LevelManager.main.SpendCurrency(recipeResult.cost);
                    ClearSlots();
                    // Update the result slot item and sprite
                    resultSlot.gameObject.SetActive(true);
                    // resultSlot.item = recipeResult.name; // Set the result item
                }
                else if (towerToBuild == null)
                {
                    Debug.Log("There is no prefab");
                    // If there's no prefab, just update the result slot item and sprite
                    resultSlot.gameObject.SetActive(true);
                    /* resultSlot.item = recipeResult; // Set the result item
                     resultSlot.GetComponent<Image>().sprite = recipeResult.GetComponent<Image>().sprite;*/
                }
                else
                {
                    PlayErrorSound();
                    ClearSlots();
                    notEnoughCurrency.text = towerToBuild.name + " needs $" + recipeResult.cost.ToString();
                    Invoke("ClearText", 3f);
                    Debug.Log("You don't have enough money");
                }
            }
        }
    }

    public void SetSeletectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    public Tower GetSelectedTower()
    {
        return recipeResults[selectedTower];
    }

    private void ClearText()
    {
        notEnoughCurrency.text = null;
    }

   
  
    // Clear all slots once the item is crafted
    public void ClearSlots() {
        foreach (Slot slot in craftingSlots)
        {
            slot.item = null;
            slot.gameObject.SetActive(false);
            slot.GetComponent<Image>().sprite = null;
        }
        for (int n = 0; n < itemList.Count; n++)
        {
            itemList[n] = null;
        }

        resultSlot.gameObject.SetActive(false);
        resultSlot.GetComponent<Image>().sprite = null;
    }

    // Get rid of the item on that slot
    public void OnClickSlot(Slot slot)
    {
        slot.item = null;
        itemList[slot.index] = null;
        slot.gameObject.SetActive(false);
        
        CheckForCompletedRecipes();
    }

    // Drag the item
    public void OnMouseDownItem(Item item)
    {
        if (currentItem == null)
        {
            currentItem = item;
            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentItem.GetComponent<Image>().sprite;
        }
    }
}
