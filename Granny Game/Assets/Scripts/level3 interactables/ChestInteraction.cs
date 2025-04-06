using UnityEngine;

public class ChestInteraction : MonoBehaviour, IInteractable
{
    private Dialogue dialogue;
    public string[] chestDialogue;
    void Start()
    {
        if (dialogue == null)
        {
            dialogue = Object.FindFirstObjectByType<Dialogue>(); 
        }
    }

    public void Interact() {
        if (dialogue != null)
        {
            dialogue.SetDialogue(chestDialogue); // Pass unique lines to Dialogue
        }
    }



}
