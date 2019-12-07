using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle the projectile launched by the player to fix the robots.
/// </summary>
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // To ensure the object doesn't fly away and cluster the game with active projectiles
        if(transform.position.magnitude > 1000.0f)    
            Destroy(gameObject);
    }

    // Physics behind launching the projectile
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    // Collision handling when projectile hits an enemy
    // Runs Fix method in EnemyController
    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy = other.collider.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.Fix();
        }

        Destroy(gameObject);

    }
}
