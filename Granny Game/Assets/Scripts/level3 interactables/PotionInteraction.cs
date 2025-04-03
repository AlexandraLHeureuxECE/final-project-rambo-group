using UnityEngine;

public class PotionInteraction : MonoBehaviour, IInteractable
{
    public GameObject potionObject;
    private Dialogue dialogue;
    public string[] potionDialogue;
    private Player player;

    void Start()
    {
        // Find the Dialogue component
        if (dialogue == null)
        {
            dialogue = FindObjectOfType<Dialogue>(); 
        }
        
        // Find the Player component (make sure to assign this if possible)
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
    }

    public void Interact() 
    {
        if (dialogue != null)
        {
            dialogue.SetDialogue(potionDialogue); // Pass unique lines to Dialogue
            
            player.walkSpeed = 8f;
            player.runSpeed = 14f;
            
            gameObject.SetActive(false);
            
        }
    }
}