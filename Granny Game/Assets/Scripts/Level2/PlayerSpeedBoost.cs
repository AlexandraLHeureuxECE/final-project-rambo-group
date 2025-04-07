using UnityEngine;

public class PlayerSpeedBoost : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float boostSpeed = 10f;
    private float currentSpeed;
    private bool hasShoes = false;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        currentSpeed = normalSpeed;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool boosting = hasShoes && Input.GetKey(KeyCode.LeftControl);
        currentSpeed = boosting ? boostSpeed : normalSpeed;

        Vector3 move = transform.right * h + transform.forward * v;
        moveDirection = move * currentSpeed;

        moveDirection.y -= 9.81f * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void UnlockShoes()
    {
        hasShoes = true;
    }
}