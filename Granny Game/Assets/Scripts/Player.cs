using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // variables for movement and camera
    public Camera playerCamera;
    public float walkSpeed = 1.5f;  // Reduced from 3f
    public float runSpeed = 3f;     // Reduced from 6f
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;

    // variables for interaction
    public Transform InteractorSource;
    public float InteractRange;
    
    CharacterController characterController;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        #region Handles Movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        
        // Only use W and S for forward/back movement
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.W)) verticalInput = 1f;
        if (Input.GetKey(KeyCode.S)) verticalInput = -1f;
        
        // Only use A and D for left/right movement
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * verticalInput : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * horizontalInput : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        #endregion

        #region Handles Interaction
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward); 
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)) 
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) 
                {
                    interactObj.Interact();
                }
            }
        }
        #endregion
    }
}