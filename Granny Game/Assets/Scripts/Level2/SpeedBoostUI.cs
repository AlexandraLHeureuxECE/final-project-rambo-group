using UnityEngine;
using TMPro;
using System.Collections;

public class SpeedBoostUI : MonoBehaviour
{
    public TextMeshProUGUI pickupPrompt;
    public TextMeshProUGUI boostHint;

    public void ShowPickupPrompt(bool state)
    {
        pickupPrompt.gameObject.SetActive(state);
    }

    public void ShowBoostHint(float duration = 2f)
    {
        StartCoroutine(DisplayHint(duration));
    }

    private IEnumerator DisplayHint(float duration)
    {
        boostHint.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        boostHint.gameObject.SetActive(false);
    }
}