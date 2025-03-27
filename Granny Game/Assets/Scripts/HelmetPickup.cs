using UnityEngine;

public class HelmetPickup : MonoBehaviour, IInteractable
{
    public enum HelmetType { Speed, Armor }  // 👈 This gives you the dropdown
    public HelmetType helmetType;            // 👈 Set this per helmet in Inspector

    public void Interact()
    {
        if (!PlayerStats.hasHelmet)
        {
            PlayerStats.hasHelmet = true;
            PlayerStats.selectedHelmet = helmetType.ToString();

            switch (helmetType)
            {
                case HelmetType.Speed:
                    PlayerStats.speedMultiplier = 1.5f;
                    Debug.Log("Speed Helmet Equipped!");
                    break;

                case HelmetType.Armor:
                    PlayerStats.hasArmor = true;
                    Debug.Log("Armor Helmet Equipped!");
                    break;
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("You already have a helmet.");
        }
    }
}