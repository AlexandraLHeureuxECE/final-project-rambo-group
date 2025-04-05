using UnityEngine;

public class BreakableCrate : MonoBehaviour, IInteractable
{
    public int strengthRequired = 50;
    public bool hasKeyInside = false;
    public GameObject keyPrefab;

    public void Interact()
    {
        if (PlayerStatsManager.Instance.GetStrengthSystem().HasEnough(strengthRequired))
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

            if (hasKeyInside && keyPrefab != null)
            {
                GameObject key = Instantiate(keyPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);

                KeyPickupVisual keyVisual = key.GetComponent<KeyPickupVisual>();
                if (keyVisual != null)
                {
                    keyVisual.Activate(); // 🔥 Trigger animation now
                }
            }


            GetComponent<Collider>().enabled = false;
            Destroy(this); // Disable script
        }
        else
        {
            DialogueManager.Instance.ShowDialogue("😩 I don't have the strength... maybe I need to drink something.");
        }
    }
}