using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBar : MonoBehaviour
{
    public Transform cat;

    void Update()
    {
        if (cat != null)
        {
            RectTransform canvasRect = GetComponent<RectTransform>();
            canvasRect.position = Camera.main.WorldToScreenPoint(cat.position);
        }
    }
}
