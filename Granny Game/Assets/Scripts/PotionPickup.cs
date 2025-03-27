using UnityEngine;

public class PotionPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.hasStrength = true;
            Debug.Log("Potion drank. Player is now strong!");
            Destroy(gameObject);
        }
    }
}