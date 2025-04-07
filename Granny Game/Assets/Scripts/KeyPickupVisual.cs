using UnityEngine;

public class KeyPickupVisual : MonoBehaviour
{
    public float floatHeight = 2f;
    public float floatSpeed = 2f;
    public float duration = 3f;

    private Vector3 startPos;
    private float elapsedTime;
    private bool shouldAnimate = false;

    public GameObject keyParticles; // 👈 Assign in Inspector

    public void Activate()
    {
        startPos = transform.position;
        elapsedTime = 0f;
        shouldAnimate = true;

        // ✨ Play the particle effect
        if (keyParticles != null)
        {
            var ps = keyParticles.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Play();
            }
        }
    }

    void Update()
    {
        if (!shouldAnimate) return;

        elapsedTime += Time.deltaTime;

        // Floating animation
        transform.position = startPos + Vector3.up * Mathf.Sin(elapsedTime * floatSpeed) * floatHeight;

        if (elapsedTime >= duration)
        {
            DialogueManager.Instance.ShowDialogue("🔑 You got the key!");
            Destroy(gameObject);
        }
    }
}