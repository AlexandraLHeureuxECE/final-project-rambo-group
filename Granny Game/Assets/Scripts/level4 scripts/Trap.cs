using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float stuckDuration = 4f; // Time the player is stuck in seconds
    private bool _isTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object triggering the trap is the player
        if (other.CompareTag("Player"))
        {
            // Ensure the trap is triggered only once
            if (!_isTriggered)
            {
                _isTriggered = true;
                StartCoroutine(TrapEffect(other)); // Apply trap effect to the player
            }
        }
    }

    IEnumerator TrapEffect(Collider player)
    {
        // Disable player's movement
        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            playerScript.canMove = false; // Assuming Player script has a `canMove` variable
        }

        // Wait for the duration of the trap effect
        yield return new WaitForSeconds(stuckDuration);

        // Re-enable player's movement after the effect
        if (playerScript != null)
        {
            playerScript.canMove = true;
        }

        // Optionally destroy the trap or reset it
        _isTriggered = false; // Reset the trap state
    }
}