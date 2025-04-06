using UnityEngine;

public class WindowInteraction : MonoBehaviour, IInteractable
{
    private Dialogue dialogue;
    public string[] windowDialogue;
    public bool cannotBreakWindow = true;
    private PadLockPassword _padLockPassword; 
    public void setCanBreak() {
        cannotBreakWindow = false;
    }

    void Start()
    {
        if (dialogue == null)
        {
            dialogue = Object.FindFirstObjectByType<Dialogue>(); 
            _padLockPassword = FindAnyObjectByType<PadLockPassword>();

        }
    }

    public void Interact() {
        if (dialogue != null && cannotBreakWindow)
        {   
            dialogue.SetDialogue(windowDialogue); // Pass unique lines to Dialogue
        }
        if (_padLockPassword.PasswordCorrect && !cannotBreakWindow) {
            Debug.Log("Escape to level 4");
            // ADD CODE HERE TO TRANSITION TO NEXT SCENE
        }
    }
}
