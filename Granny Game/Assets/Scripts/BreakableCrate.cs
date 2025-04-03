using UnityEngine;


public class BreakableCrate : MonoBehaviour, IInteractable
{
    public bool hasKeyInside = false;
    public GameObject keyPrefab;

    public void Interact()
    {
        if (PlayerStats.strength >= PlayerStats.strengthRequiredToBreak)
        {
            Debug.Log("Crate broken!");

            Rigidbody[] pieces = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in pieces)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // Optionally disable the main collider or script here
            GetComponent<Collider>().enabled = false;
            Destroy(this); // optional: prevent repeat interaction
        }
        else
        {
            Debug.Log("Not strong enough to break crate.");
        }
    }

}
