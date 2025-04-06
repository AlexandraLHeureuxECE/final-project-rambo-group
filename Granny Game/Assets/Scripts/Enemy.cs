using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    int currentHealth;
    public int maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;
        Debug.Log("Takes " + amount + " Damage");
        if(currentHealth <= 0) {
            Death();
        }
    }

    private void Death() {
        // Add any death animations or behavior here
        Debug.Log(gameObject.name + " has died.");
        Destroy(gameObject);
    }
}
