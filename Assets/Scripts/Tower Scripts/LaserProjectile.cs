using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 7.5f;
    [SerializeField] private int bulletDmg = 3; 


    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;

        Vector2 direction = (_target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
        other.gameObject.GetComponent<Health>().TakeDamage(bulletDmg,"LaserPointer");
        // Take health from enemy
        Destroy(gameObject);
    }
}
