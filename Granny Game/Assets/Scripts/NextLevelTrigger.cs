using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    public string nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.LoadNextLevel(nextLevelName);
        }
    }
}