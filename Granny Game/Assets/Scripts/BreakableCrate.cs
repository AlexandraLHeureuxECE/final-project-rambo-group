using UnityEngine;

public class BreakableCrate : MonoBehaviour
{
    public GameObject itemInside; // Drag helmet or key prefab here

    private void OnMouseDown()
    {
        if (PlayerStats.hasStrength)
        {
            if (itemInside != null)
            {
                Instantiate(itemInside, transform.position + Vector3.up * 1.5f, Quaternion.identity);
            }
            Debug.Log("Crate broken!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("I am not strong enough to break this...");
        }
    }
}