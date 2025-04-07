using UnityEngine;

public class DoorLogic : MonoBehaviour, IInteractable
{
    private bool isOpened = false;

    [SerializeField] private GameObject blackPassageObject; // Assign in Inspector

    public void Interact()
    {
        TryOpenDoor();
    }

    public void TryOpenDoor()
    {
        if (isOpened) return;

        Debug.Log("Player has key? " + PlayerStats.hasKey);

        if (PlayerStats.hasKey)
        {
            isOpened = true;

            DialogueManager.Instance.ShowDialogue("🔓 The door opens!");

            // 🚪 Hide the door
            gameObject.SetActive(false);

            // 🕳️ Show black passage
            if (blackPassageObject != null)
            {
                blackPassageObject.SetActive(true);
            }
        }
        else
        {
            DialogueManager.Instance.ShowDialogue("🚪 It's locked... maybe the key is in one of these crates.");
        }
    }
}