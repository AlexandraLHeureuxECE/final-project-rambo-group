using UnityEngine;

public class HelmetPickup : MonoBehaviour
{
    public string helmetType; // Assign in Inspector (e.g., "Armor", "Speed")

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerStats.hasHelmet)
        {
            PlayerStats.hasHelmet = true;
            PlayerStats.selectedHelmet = helmetType;

            Debug.Log("Helmet equipped: " + helmetType);

            Destroy(gameObject);
        }
    }
}
