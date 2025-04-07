using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.hasKey = true; // Set hasKey to true when the player picks up the key
            Debug.Log("Key collected! hasKey: " + PlayerStats.hasKey); // Debug log to confirm key collection

            DialogueManager.Instance.ShowDialogue("🔑 You picked up the key!");

            Destroy(gameObject); // Remove key from the scene
        }
    }
}