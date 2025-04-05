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
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
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
        DialogueManager.Instance.ShowDialogue("You wake up in a mysterious basement...");
        StartCoroutine(HideDialogueAfterDelay(3f));

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        IEnumerator HideDialogueAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            DialogueManager.Instance.HideDialogue();
        }

    }

    void Update()
    {

        #region Handles Movment

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
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

        //region Handles Interaction

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed ✅");

            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            Debug.DrawRay(r.origin, r.direction * InteractRange, Color.red, 2f);

            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log("Raycast HIT: " + hitInfo.collider.gameObject.name);

                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
            else
            {
                Debug.Log("Raycast did NOT hit anything ❌");
            }
        }
    }
}
