using UnityEngine;


public class BreakableCrate : MonoBehaviour, IInteractable
{
    public bool hasKeyInside = false;
    public GameObject keyPrefab;

    public void Interact()
    {
        if (PlayerStats.hasStrength)
        {
            Debug.Log("Crate broken!");

            if (hasKeyInside && keyPrefab != null)
            {
                Instantiate(keyPrefab, transform.position + Vector3.up, Quaternion.identity);
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("You need strength to break this crate.");
        }
    }
}