using UnityEngine;

public class PotionInteraction : MonoBehaviour, IInteractable
{
    public GameObject potionObject;
    private Dialogue dialogue;
    public string[] potionDialogue;
    private Player player;
    private WindowInteraction windowInteraction;

    void Start()
    {
        dialogue = FindObjectOfType<Dialogue>(); 
        player = FindObjectOfType<Player>();
        windowInteraction = FindObjectOfType<WindowInteraction>(); 
    }

    public void Interact() 
    {
        if (dialogue != null)
        {
            dialogue.SetDialogue(potionDialogue); 
            
            player.walkSpeed = 6f;
            player.runSpeed = 12f;
            
            gameObject.SetActive(false);
            
            windowInteraction.setCanBreak();
        }
    }
}