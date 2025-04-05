using UnityEngine;

public class PotionPickup : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Potion Interacted!");

        PlayerStatsManager.Instance.AddStrength(50);
        Destroy(gameObject); // destroy potion
    }
}