 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public float snapRange = 0.5f;

    
    void Update()
    {
        // Search for Draggable objects every frame
        foreach (Draggable draggable in FindObjectsOfType<Draggable>())
        {

            // Add a drag ended callback if one hasn't been added already
            draggable.dragEndedCallback = OnDragEnded;

        }
    }

    // Update is called once per frame
    private void OnDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition;
        }
    }
}