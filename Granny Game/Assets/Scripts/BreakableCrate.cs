using UnityEngine;
using UnityEngine;

public class BreakableCrate : MonoBehaviour, IInteractable
{
    public bool hasKeyInside = false;
    public GameObject keyPrefab;

    public void Interact()
    {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (player != null && PlayerStats.hasStrength)
        {
            Debug.Log("Crate broken!");

            if (hasKeyInside && keyPrefab != null)
            {
                Instantiate(keyPrefab, transform.position + Vector3.up, Quaternion.identity);
                Debug.Log("Key dropped!");
            }

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("You need more strength to break this crate.");
        }
    }
}
