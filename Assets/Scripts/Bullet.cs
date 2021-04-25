using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f, boostFactor = 0.25f;
    
    [SerializeField]
    private Vector3 boost = Vector3.zero;
    public Vector3 Boost { set { boost = value; } }

    private void Start() {

        FindObjectOfType<AudioManager>().Play("Shoot");
    }

    void Update()
    {
        Vector3 velocity = (transform.up * moveSpeed + boost * boostFactor);
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {

        // Check collision type
        switch (LayerMask.LayerToName(other.gameObject.layer)) 
            {
            case "Bounds":
                OutOfBounds();
                break;
            case "Target":
                OutOfBounds();
                break;
            case "Enemy":
                Enemy enemyHit = other.gameObject.GetComponentInParent<Enemy>();
                if (enemyHit != null) { HitEnemy(enemyHit); }
                break;
            }
    }

    private void OutOfBounds() {

        // Destroy projectile
        Destroy(gameObject);
    }

    private void HitEnemy(Enemy enemy) {

        // Pass damage to enemy
        enemy.TakeDamage();

        // Destroy projectile
        Destroy(gameObject);
    }
}
