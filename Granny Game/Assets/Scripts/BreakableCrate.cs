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

            Rigidbody[] pieces = GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody rb in pieces)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            if (hasKeyInside && keyPrefab != null)
            {
                Instantiate(keyPrefab, transform.position + Vector3.up, Quaternion.identity);
                Debug.Log("Key dropped!");
            }

            GetComponent<Collider>().enabled = false;
            Destroy(this); // optional: disable interaction after break
        }
        else
        {
            Debug.Log("Not enough strength to break the crate!");
        }
    }
}