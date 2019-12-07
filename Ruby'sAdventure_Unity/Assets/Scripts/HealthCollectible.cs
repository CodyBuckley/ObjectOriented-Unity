using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will handle giving health to the character when they enter the trigger.
/// </summary>

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;

    // Detect when player character collides with object
    private void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        // Heals the player if their health is less than the max value
        if (controller != null)
        {
            if(controller.Health < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);
            }
        }
    }
}
