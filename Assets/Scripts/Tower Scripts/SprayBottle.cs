using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SprayBottle : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject sellUI;
    [SerializeField] private Button sellButton;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bps = 1f; // bullets for second

    private Transform target;
    private float timeUntilFire;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            // Calculate the direction from the tower to the target
            Vector2 direction = target.position - transform.position;

            // Calculate the angle the tower needs to rotate to face the target
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // Rotate the tower to face the target
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        WaterProjectile bulletScript = bulletObj.GetComponent<WaterProjectile>();
        bulletScript.SetTarget(target);
        audioSource.Play();
    }
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
    
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

/*    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
*/




    // Start is called before the first frame update
    void Start()
    {
        sellButton.onClick.AddListener(Sell);
    }

    public void CloseSellUI()
    {
        sellUI.SetActive(false);
        UIManager.Instance.SetHoveringState(false);
    }

    public void OpenSellUI()
    {
        sellUI.SetActive(true);
    }

    public void Sell()
    {
        LevelManager.main.IncreaseCurrency(5);
        CloseSellUI();
        Destroy(gameObject);

    }

    
}
