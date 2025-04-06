using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    // variables for movement and camera
    public int playerHealth = 100;
    public Camera playerCamera;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask attackLayer;
    public GameObject hitEffect;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool canMove = true;

    // Attack-related
    private float attackDistance;
    private float attackSpeed;
    private int attackDamage;
    bool attacking = false;
    bool readyToAttack = true;

    CharacterController characterController;
    private Animator metalBatAnimator;
    private Animator hammerAnimator;
    private Animator broomAnimator;

    private Dictionary<string, WeaponStats> weaponStats;
    private WeaponStats currentWeaponStats;

    void Start()
    {
        DialogueManager.Instance.ShowDialogue("You wake up in a mysterious basement...");
        StartCoroutine(HideDialogueAfterDelay(3f));

        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        metalBatAnimator = GameObject.Find("MetalBat").GetComponent<Animator>();
        hammerAnimator = GameObject.Find("HammerAnimator").GetComponent<Animator>();
        broomAnimator = GameObject.Find("BroomAnimator").GetComponent<Animator>();

        walkSpeed = 3f;
        runSpeed = 6f;

        // CHANGE WEAPON STATS HERE IF NEEDED
        weaponStats = new Dictionary<string, WeaponStats>
        {
            { "Weapon 1", new WeaponStats { name = "Hammer", damage = 1, range = 1f, attackSpeed = 0.25f } },
            { "Weapon 2", new WeaponStats { name = "Metal Bat", damage = 5, range = 3f, attackSpeed = 1f } },
            { "Weapon 3", new WeaponStats { name = "Broom", damage = 3, range = 6f, attackSpeed = 0.5f } }
        };

    }

    void Update()
    {
        HandleMovement();
        HandleJumping();
        HandleRotation();
        HandleInteraction();

        if (Input.GetMouseButtonDown(0) && readyToAttack)
        {
            Attack();
        }
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float verticalInput = Input.GetKey(KeyCode.W) ? 1f : (Input.GetKey(KeyCode.S) ? -1f : 0f);
        float horizontalInput = Input.GetKey(KeyCode.D) ? 1f : (Input.GetKey(KeyCode.A) ? -1f : 0f);

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * verticalInput : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * horizontalInput : 0;
        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        moveDirection.y = movementDirectionY;
    }

    void HandleJumping()
    {
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void HandleRotation()
    {
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
    }
<<<<<<< HEAD
<<<<<<< HEAD
    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Player took damage! Current health: " + playerHealth);

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has been defeated!");
        // You can add respawn logic here
=======

    public void UpdateWeaponStats()
    {
        if (weaponStats.TryGetValue(WeaponSelectorUI.CurrentWeapon, out WeaponStats stats))
        {
            currentWeaponStats = stats;
            attackDamage = stats.damage;
            attackDistance = stats.range;
            attackSpeed = stats.attackSpeed;
            Debug.Log($"Equipped {stats.name}: Damage={stats.damage}, Range={stats.range}, Speed={stats.attackSpeed}");
        }
    }

    public void Attack()
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        switch (WeaponSelectorUI.CurrentWeapon)
        {
            case "Weapon 1":
                if (hammerAnimator != null && hammerAnimator.gameObject.activeInHierarchy)
                    hammerAnimator.SetTrigger("Attack");
                break;
            case "Weapon 2":
                if (metalBatAnimator != null && metalBatAnimator.gameObject.activeInHierarchy)
                    metalBatAnimator.SetTrigger("Attack");
                break;
            case "Weapon 3":
                if (broomAnimator != null && broomAnimator.gameObject.activeInHierarchy)
                    broomAnimator.SetTrigger("Attack");
                break;
            default:
                Debug.Log("No weapon selected or weapon not recognized");
                break;
        }

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackSpeed * 0.5f);
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * attackDistance, Color.red, 0.5f);

        if (Physics.Raycast(ray, out RaycastHit hit, attackDistance, attackLayer))
        {
            if (hit.transform.TryGetComponent<Actor>(out Actor actor))
            {
                actor.TakeDamage(attackDamage);
            }
        }
>>>>>>> level3-Andy
    }
}
