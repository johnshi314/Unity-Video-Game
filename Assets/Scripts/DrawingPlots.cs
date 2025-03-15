using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrawingPlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{  
    [SerializeField] private Image sr;
    [SerializeField] private Color hoverColor;
    private Color startColor;
    private List<int> plotsPassed = new List<int> ();
    /// <summary>
    /// from top to down and from left to right 
    /// 1 4 7
    /// 2 5 8
    /// 3 6 9
    /// </summary>
    public int plotIndex;
    private Renderer renderer;
    private Vector2 size;
    private Vector2 range;

    private void Start()
    {
        startColor = sr.color;
        renderer = GetComponent<Renderer>();
        /*
         * weird behavior, not sure how to set the range of the plot so it can detect that the mouse is actually on the plot when drawing
        Vector3 size = renderer.bounds.size;
        Vector2 screenSpaceSize = new Vector2(
            Camera.main.WorldToScreenPoint(size).x - Camera.main.WorldToScreenPoint(Vector3.zero).x,
            Camera.main.WorldToScreenPoint(size).y - Camera.main.WorldToScreenPoint(Vector3.zero).y);
        Vector2 range = screenSpaceSize / 2;
        */
    }

    void Update()
    {
        /* weird behavior, not sure how to set the range of the plot so it can detect that the mouse is actually on the plot when drawing
        Vector2 plotPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePosition = Camera.main.WorldToScreenPoint(Input.mousePosition);*/
        if (Input.GetMouseButtonUp(0))//&& Mathf.Abs(mousePosition.x - plotPosition.x) <= range.x && Mathf.Abs(mousePosition.y - plotPosition.y) <= range.y))
        {
            int currentPlotIndex = LevelManager.main.GetPlotIndex(this);
            if (!plotsPassed.Contains(currentPlotIndex))
            {
                plotsPassed.Add(currentPlotIndex);
                Debug.Log(currentPlotIndex);
                Debug.Log("Plots passed:");
            }
        }
        else
        {
           // Debug.Log(Mathf.Abs(mousePosition.x - plotPosition.x));
           // Debug.Log(Mathf.Abs(mousePosition.y - plotPosition.y));
           // Debug.Log(range);
            checkDrawing();
        }
    }



    // Instead of having plots passed in here move it to something like the crafting mananger. Then in here call Crafting manager.main.plotsPassed.add(currentPlotIndex)
    void checkDrawing() { 

        
    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = hoverColor;

        // I want to print out plotsNumber here somehow.
        //Debug.Log("Entered: " + plotIndex);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = startColor;

        //Debug.Log("Exited: " + plotIndex);
    }
}
