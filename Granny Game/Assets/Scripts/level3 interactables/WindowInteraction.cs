using UnityEngine;

public class WindowInteraction : MonoBehaviour, IInteractable
{
    private Dialogue dialogue;
    public string[] windowDialogue;
    private bool canBreakWindow = false;
    
    public void setCanBreak() {
        canBreakWindow = true;
    }

    void Start()
    {
        if (dialogue == null)
        {
            dialogue = Object.FindFirstObjectByType<Dialogue>(); 
        }
    }

    public void Interact() {
        if (dialogue != null && canBreakWindow == false)
        {
            dialogue.SetDialogue(windowDialogue); // Pass unique lines to Dialogue
        }
    }


    void Update()
    {
        
    }
}
