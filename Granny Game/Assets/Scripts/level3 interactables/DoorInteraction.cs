using UnityEngine;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    private Dialogue dialogue;
    public string[] doorDialogue;
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
            dialogue.SetDialogue(doorDialogue); // Pass unique lines to Dialogue
        }
    }


    void Update()
    {
        
    }
}
