using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomRightSpawner : MonoBehaviour
{
    public GameObject russianBlue;

    public float spawnRate = 2.0f;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {

        russianBlue = Resources.Load<GameObject>("Prefabs/RussianBlue");

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Instantiate(russianBlue, transform.position, transform.rotation);
            timer = 0;
        }

    }
}
