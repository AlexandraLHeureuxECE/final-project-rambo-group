using UnityEngine;

public class PotionInteraction : MonoBehaviour, IInteractable
{
    private Dialogue dialogue;
    public string[] potionDialogue;
    Player player;
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
            dialogue.SetDialogue(potionDialogue); // Pass unique lines to Dialogue
            player.walkSpeed = 8f;
            player.runSpeed = 14f;
        }
    }


    void Update()
    {
        
    }
}
