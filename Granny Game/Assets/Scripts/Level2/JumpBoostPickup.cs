using UnityEngine;

public class JumpBoostPickup : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private PlayerJumpBoost jumpBoost;
    private JumpBoostUI ui;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (jumpBoost != null)
            {
                jumpBoost.UnlockBoots();

                if (ui != null)
                {
                    ui.ShowPickupPrompt(false);
                    ui.ShowUseHint(); 
                }

                Destroy(gameObject); 
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jumpBoost = other.GetComponent<PlayerJumpBoost>();
            ui = other.GetComponent<JumpBoostUI>();
            isPlayerNearby = true;

            if (ui != null)
                ui.ShowPickupPrompt(true);
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