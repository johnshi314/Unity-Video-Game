using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{

    public GameObject objectToDrag;
    public GameObject objectDragToPos;

    public float snapRange;

    public bool isLocked;

    Vector2 objectInitPos;

    // Start is called before the first frame update
    void Start()
    {
        objectInitPos = objectToDrag.transform.position;
    }

    public void DragObject()
    {
        if (!isLocked)
        {
            objectToDrag.transform.position = Input.mousePosition;
        }
    }

    public void DropOject()
    {
        float distance = Vector3.Distance(objectToDrag.transform.position, objectDragToPos.transform.position);

        if (distance < snapRange)
        {
            isLocked = true;
            objectToDrag.transform.position = objectDragToPos.transform.position;
        }
        else
        {
            objectToDrag.transform.position = objectInitPos;
        }
    }
}
