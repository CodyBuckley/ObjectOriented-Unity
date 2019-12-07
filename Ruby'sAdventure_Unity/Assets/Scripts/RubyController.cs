using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Movement
    public float speed = 4.0f;

    Rigidbody2D rigidbody2d;

    // Health
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public Transform respawnPosition;
    

    public int Health
    {
        get
        {
            return currentHealth;
        }
    }

    int currentHealth;
    bool isInvincible;
    float invincibleTimer;

    // Projectile
    public GameObject projectilePrefab;    
    
    // Audio
    public AudioClip throwClip;
    public AudioClip hitClip;

    // Sound
    AudioSource audioSource;

    // Animation
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    // Start is called before the first frame update
    private void Start()
    {
        // Movement
        rigidbody2d = GetComponent<Rigidbody2D>();

        // Health
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;

        // Animation
        animator = GetComponent<Animator>();

        // Audio
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Set up horizontal and vertical movement with input commands.
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Stores directional inputs into a single vector
        Vector2 move = new Vector2(horizontal, vertical);

        // Check to see if move.x or move.y is 0
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            // Set player sprite direction pointed towards move vector
            // Normalize to set the vector length to 1
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        // Animation directionals
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // Changes position based on physics and move vector
        Vector2 position = rigidbody2d.position;
        position += move * speed * Time.deltaTime;

        rigidbody2d.MovePosition(position);

        // frames invincible in the case of taking damage, checked once per frame
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // Press C to fire projectile
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        // Press X to talk to NPC
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }

    // Method used to launch projectiles at enemys. It instantiates an object and applys physics to the projectile
    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwClip);
    }

    private void Respawn()
    {
        ChangeHealth(maxHealth);
        transform.position = respawnPosition.position;
    }

    // Method used in order to change player character's health
    public void ChangeHealth(int amount)
    {
        // Checks to see if player has taken damage for invincibility frames
        if (amount < 0)
        {
            if (isInvincible)
                return;


            isInvincible = true;
            invincibleTimer = timeInvincible;

            animator.SetTrigger("Hit");
            PlaySound(hitClip);
        }

        currentHealth = Mathf.Clamp(Health + amount, 0, maxHealth);

        if (Health == 0)
            Respawn();

        UIHealthBar.instance.SetValue(Health/(float)maxHealth);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

}
