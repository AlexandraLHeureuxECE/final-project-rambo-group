using UnityEngine;

public class HelmetPickup : MonoBehaviour, IInteractable
{
    public string helmetType; // Assign in Inspector (e.g., "Armor", "Speed")

    public void Interact()
    {
        if (!PlayerStats.hasHelmet)
        {
            PlayerStats.hasHelmet = true;
            PlayerStats.selectedHelmet = helmetType;

            Debug.Log("Helmet equipped: " + helmetType);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("You already have a helmet.");
        }
    }
}