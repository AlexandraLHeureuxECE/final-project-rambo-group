using UnityEngine;

public class PotionPickup : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            PlayerStats.hasStrength = true;
            Debug.Log("Player drank the potion and gained strength!");
        }

        Destroy(gameObject);
    }
}