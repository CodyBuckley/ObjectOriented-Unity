using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handle Enemy behaviour. It make them walk back & forth as long as they aren't fixed, and then just idle
/// without being able to interact with the player anymore once fixed.
/// </summary>
public class EnemyController : MonoBehaviour
{ 
    // Movement
    public float speed = 3.0f;
    public float changeTime = 3.0f;
    public bool vertical;
    
    public ParticleSystem smokeEffect;
    public AudioClip fixedClip;

    Rigidbody2D rigidbody2d;
    float timer;
    int direction = 1;
    bool broken;

    // Animation
    Animator animator;
    
    // Sound
    AudioSource audioSource;
    

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check every frame to see if enemy is fixed or not
        if (broken)
            return;

        timer -= Time.deltaTime;

        // Time Count to change directions of enemy
        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        Vector2 position = rigidbody2d.position;

        // Movement handler if moving vertically
        if (vertical)
        {
            position.y += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }

        // Movement handler if moving horizontally
        else
        {
            position.x += Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2d.MovePosition(position);
    }


    // Collision handler if player character comes into contact with the enemy
    private void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
            player.ChangeHealth(-1);

    }

    // Method to fix the robot if hit by projectile
    public void Fix()
    {
        UIProgress.fixedCount += 1;

        // Change animation to Fixed animation
        animator.SetTrigger("Fixed");
        broken = false;
        rigidbody2d.simulated = false;

        // Sound
        audioSource.Stop();
        audioSource.PlayOneShot(fixedClip);

        // Stop smoke effect
        smokeEffect.Stop();

        



    }
}
