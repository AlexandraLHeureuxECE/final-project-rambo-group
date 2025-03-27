using UnityEngine;

public class PotionPickup : MonoBehaviour, IInteractable
{
    public int strengthAmount = 50;

    public void Interact()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            PlayerStats.strength += strengthAmount;
            Debug.Log("Potion picked up. Strength now: " + PlayerStats.strength);
        }

        Destroy(gameObject);
    }
}