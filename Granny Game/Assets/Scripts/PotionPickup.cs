using System.Collections;
using UnityEngine;

public class PotionPickup : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Potion Interacted!");
        DialogueManager.Instance.ShowDialogue("💪 You feel strength surge through your body!");
        StartCoroutine(HideDialogueAfterDelay(3f));

        IEnumerator HideDialogueAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            DialogueManager.Instance.HideDialogue();
        }



        PlayerStatsManager.Instance.AddStrength(50);
        Destroy(gameObject); // destroy potion
    }
}