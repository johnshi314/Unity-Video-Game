using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RussianBlue : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Rigidbody2D rb;
    [Header("Attributes")]
    private bool distract = false;
    private bool hasCollided = false;

    // Speed of the enemy
    [SerializeField] private float moveSpeed = 2f;
    private int numberOfCats = 0;
    private Transform target;
    private int pathIndex = 0;
    [SerializeField] private ScratchPost scratchposts;
    [SerializeField] private int livesToTake = 1;
    public GameObject energyBarPrefab;
    private GameObject energyBar;
    private RussianBlueSpawner waveSpawner;



    void Start()
    {
        target = LevelManager.main.path[0];
        energyBar = Instantiate(energyBarPrefab, transform);
        energyBar.transform.localPosition = transform.position;
        waveSpawner = FindAnyObjectByType<RussianBlueSpawner>();

    }

    private void Update()
    {

        energyBar.transform.position = transform.position + new Vector3(0.0f, 1f, 0f);

        if (Vector2.Distance(target.position, transform.position) <= 0.1f && !distract)
        {

            hasCollided = false;
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                RussianBlueSpawner.onEnemyDestory.Invoke();
                Destroy(gameObject);
                LevelManager.main.TakeLives(livesToTake);
                waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
                RotateTowardsTarget();
            }
        }



    }
    public GameObject getEnergyObject()
    {
        return this.energyBar;
    }

    public void OnCollisionEnter2D(Collision2D scratchpost)
    {



        if (scratchpost.gameObject.CompareTag("tower"))
        {
            if (hasCollided) return;
            StartCoroutine(Wait());

            distract = true;
            scratchposts.setCatsDistracted();

            if (scratchposts.getCatsDistracted() >= 4)
            {
                //needs to make the enemy and tower destroy at the same time
                Destroy(scratchpost.gameObject);
            }
            hasCollided = true;
        }


    }

    IEnumerator Wait()
    {
        numberOfCats = scratchposts.getCatsDistracted();
        yield return new WaitForSeconds(0.1f * (5 - numberOfCats));
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(2f);
        GetComponent<Health>().TakeDamage(1,"ScratchPost");
        distract = false;
        if (scratchposts.getCatsDistracted() >= 4)
        {
            scratchposts.setCatsDistracted();

        }

    }
    private void FixedUpdate()
    {
        if (GetComponent<Health>().currentHealth() > 0)
        {
            if (distract == false)
            {

                Vector2 direction = (target.position - transform.position).normalized;
                rb.velocity = direction * moveSpeed;
            }
        }




    }

    private void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}