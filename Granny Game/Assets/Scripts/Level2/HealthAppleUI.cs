using UnityEngine;

public class HealthAppleUI : MonoBehaviour
{
    public GameObject pickupPromptText;
    public GameObject useHintText;

    void Start()
    {
        pickupPromptText.SetActive(false);
        useHintText.SetActive(false);
    }

    public void ShowPickupPrompt(bool state)
    {
        if (pickupPromptText != null)
            pickupPromptText.SetActive(state);
    }

    public void ShowUseHint()
    {
        if (useHintText != null)
            useHintText.SetActive(true);
    }

    public void HideUseHint()
    {
        if (useHintText != null)
            useHintText.SetActive(false);
    }
}