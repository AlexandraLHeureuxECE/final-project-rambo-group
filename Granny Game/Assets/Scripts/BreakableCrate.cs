using UnityEngine;

public class BreakableCrate : MonoBehaviour, IInteractable
{
    public int strengthRequired = 50;
    public bool hasKeyInside = false;
    public GameObject keyPrefab;

    public void Interact()
    {
        if (PlayerStats.strength >= strengthRequired)
        {
            Debug.Log("Crate broken!");

            DialogueManager.Instance.ShowDialogue("💥 Oh yes, I’m strong now!");

            // Break visuals
            Rigidbody[] pieces = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in pieces)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            if (hasKeyInside)
            {
                PlayerStats.hasKey = true;
                DialogueManager.Instance.ShowDialogue("🔑 You found a key inside!");

                if (keyPrefab != null)
                {
                    GameObject key = Instantiate(keyPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
                    KeyPickupVisual keyVisual = key.GetComponent<KeyPickupVisual>();
                    if (keyVisual != null)
                    {
                        keyVisual.Activate();
                    }
                }
            }

            GetComponent<Collider>().enabled = false;
            Destroy(this);
        }
        else
        {
            DialogueManager.Instance.ShowDialogue(" I don't have the strength... maybe I need to drink something.");
        }
    }
}