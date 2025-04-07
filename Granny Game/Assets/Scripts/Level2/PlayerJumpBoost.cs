using UnityEngine;

public class PlayerJumpBoost : MonoBehaviour
{
    public float normalJumpPower = 7f;
    public float boostedJumpPower = 50f;
    public float boostDuration = 5f;

    private float currentJumpPower;
    private bool hasBoots = false;
    private bool isBoosting = false;

    private CharacterController controller;
    private Vector3 moveDirection;

    void Start()
    {
        currentJumpPower = normalJumpPower;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (hasBoots && !isBoosting && Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("Pressed J to activate jump boost.");
            StartCoroutine(ActivateJumpBoost());
        }

        HandleMovement();
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;

        float verticalVelocity = moveDirection.y;
        moveDirection = move * 5f;
        moveDirection.y = verticalVelocity;

        if (controller.isGrounded)
        {
            moveDirection.y = -1f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = currentJumpPower;
                Debug.Log("Jumping with power: " + currentJumpPower);
            }
        }

        moveDirection.y -= 9.81f * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void UnlockBoots()
    {
        hasBoots = true;
        Debug.Log("Jump boots picked up!");
    }

    private System.Collections.IEnumerator ActivateJumpBoost()
    {
        isBoosting = true;
        currentJumpPower = boostedJumpPower;
        Debug.Log("Jump Boost Activated!");

        yield return new WaitForSeconds(boostDuration);

        currentJumpPower = normalJumpPower;
        isBoosting = false;
        Debug.Log("Jump Boost Ended.");
    }
}