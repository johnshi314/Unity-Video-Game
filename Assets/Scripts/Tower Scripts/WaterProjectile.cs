using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    [SerializeField] private Sprite basicCatSprite;
    [SerializeField] private Sprite fatCatSprite;
    [SerializeField] private Sprite tigerSprite;
    //[SerializeField] private GameObject basicCat;
    // [SerializeField] private GameObject fatCat;
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDmg = 1;


    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDmg, "SprayBottle");
        // Take health from enemy
        Destroy(gameObject);
    }
    /*
    private IEnumerator RunAway(Collision2D other, float duration) { 
        other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        other.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        other.gameObject.GetComponent<Rigidbody2D>().velocity = direction * 3;
    }*/
}
