using UnityEngine;

public class SpeedShoesPickup : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private PlayerSpeedBoost playerBoost;
    private SpeedBoostUI ui;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (playerBoost != null)
            {
                playerBoost.UnlockShoes();
                if (ui != null)
                {
                    ui.ShowPickupPrompt(false);
                    ui.ShowBoostHint();
                }
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerBoost = other.GetComponent<PlayerSpeedBoost>();
            ui = other.GetComponent<SpeedBoostUI>();
            isPlayerNearby = true;

            if (ui != null)
            {
                ui.ShowPickupPrompt(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && ui != null)
        {
            isPlayerNearby = false;
            ui.ShowPickupPrompt(false);
        }
    }
}