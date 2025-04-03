using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float speed = 2f;
    public float movementRange = 2f;
    private Vector3 initialPosition;
    private bool movingRight = true;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= initialPosition.x + movementRange)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;

            if (transform.position.x <= initialPosition.x - movementRange)
            {
                movingRight = true;
            }
        }
    }
}