using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PathFollower : MonoBehaviour
{
   /* Node[] PathNode;
    public GameObject Cat;
    public float movespeed;
    float timer;
    static Vector3 CurrentPositionHolder;
    int CurrentNode = 0;
    private Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        PathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode()
    {
        timer = 0;
        CurrentPositionHolder = PathNode[CurrentNode].transform.position;
        startPosition = Cat.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * movespeed;
        
        if (Cat.transform.position != CurrentPositionHolder)
        {

            Cat.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, timer);
        } else
        {
            if (CurrentNode < PathNode.Length - 1) {
                CurrentNode++;
                CheckNode();
            }
        }
    }*/
}
