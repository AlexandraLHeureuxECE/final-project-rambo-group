using UnityEngine;
using TMPro;

public class HelmetPickup : MonoBehaviour, IInteractable
{
    public enum HelmetType { Speed, Strength }
    public HelmetType helmetType;

    public string helmetName;
    public TextMeshProUGUI labelText; // 🔥 Assign label in Inspector
    public float speedMultiplier = 2f;
    public float strengthBonusPercent = 0.2f;

    public bool isPickedUp = false;

    private void Start()
    {
        if (labelText != null)
            labelText.gameObject.SetActive(false); // Hide label by default
    }

    public void ShowLabel()
    {
        if (!isPickedUp && labelText != null)
            labelText.gameObject.SetActive(true);
    }

    public void HideLabel()
    {
        if (labelText != null)
            labelText.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (isPickedUp || PlayerStats.hasHelmet) return;

        PlayerStats.hasHelmet = true;
        PlayerStats.selectedHelmet = helmetName;

        if (helmetType == HelmetType.Speed)
        {
            PlayerStats.speedMultiplier = speedMultiplier;
            DialogueManager.Instance.ShowDialogue($"🪖 {helmetName} equipped! Speed x{speedMultiplier}.");
        }
        else if (helmetType == HelmetType.Strength)
        {
            PlayerStats.strength += 20; // flat +20 strength

            DialogueManager.Instance.ShowDialogue($"🪖 {helmetName} equipped! Strength + 20.");
        }

        isPickedUp = true;

        if (labelText != null)
            labelText.gameObject.SetActive(false);

        Destroy(gameObject); // ✅ DESTROY the helmet object on pickup
    }
}