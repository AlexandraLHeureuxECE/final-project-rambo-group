using UnityEngine;
using TMPro;
using System.Collections;

public class JumpBoostUI : MonoBehaviour
{
    public TextMeshProUGUI pickupPrompt;
    public TextMeshProUGUI useHint;

    public void ShowPickupPrompt(bool state)
    {
        if (pickupPrompt != null)
            pickupPrompt.gameObject.SetActive(state);
    }

    public void ShowUseHint(float duration = 3f)
    {
        if (useHint != null)
            StartCoroutine(DisplayHint(duration));
    }

    private IEnumerator DisplayHint(float duration)
    {
        useHint.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration);
        useHint.gameObject.SetActive(false);
    }
}