using System.Collections;
using UnityEngine;

public class PotionPickup : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Potion Interacted!");
        DialogueManager.Instance.ShowDialogue("I Feel so STRONG now! Lets see what else i can find, maybe the key " +
                                              "can be inside these crates or drawers");
        StartCoroutine(HideDialogueAfterDelay(3f));

        IEnumerator HideDialogueAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            DialogueManager.Instance.HideDialogue();
        }

        PlayerStatsManager.Instance.AddStrength(50); // flat +50 strength

        Destroy(gameObject);
    }
}