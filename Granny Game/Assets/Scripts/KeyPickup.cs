using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats.hasKey = true;
            Debug.Log("Key collected!");
            Destroy(gameObject);
        }
    }
}