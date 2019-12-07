using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will apply continuous damage to the Player as long as it stay inside the trigger on the same object
/// </summary>
public class DamageZone : MonoBehaviour
{

    // For every second on the damage zone, the player takes 1
    private void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        // controller handles if invincibility is active
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

}
