using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
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

    private PlayerHealth playerHealth;

    void Start()
    {
        DialogueManager.Instance.ShowDialogue("Ah! What happened? Where am I? I need to get out. Let me examine everything here");
        StartCoroutine(HideDialogueAfterDelay(3f));

        characterController = GetComponent<CharacterController>();
        playerHealth = GetComponent<PlayerHealth>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        metalBatAnimator = GameObject.Find("MetalBat").GetComponent<Animator>();
        hammerAnimator = GameObject.Find("HammerAnimator").GetComponent<Animator>();
        broomAnimator = GameObject.Find("BroomAnimator").GetComponent<Animator>();

        walkSpeed = 3f;
        runSpeed = 6f;

        weaponStats = new Dictionary<string, WeaponStats>
        {
            { "Weapon 1", new WeaponStats { name = "Hammer", damage = 1, range = 1f, attackSpeed = 0.25f } },
            { "Weapon 2", new WeaponStats { name = "Metal Bat", damage = 5, range = 3f, attackSpeed = 1f } },
            { "Weapon 3", new WeaponStats { name = "Broom", damage = 3, range = 6f, attackSpeed = 0.5f } }
        };

        PlayerStats.speedMultiplier = 0.5f;
    }

    IEnumerator HideDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        DialogueManager.Instance.HideDialogue();
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
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        Debug.DrawRay(r.origin, r.direction * InteractRange, Color.red, 2f);

        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
        {
            Debug.Log("Raycast HIT: " + hitInfo.collider.gameObject.name);

            foreach (var helmetPickup in FindObjectsOfType<HelmetPickup>())
            {
                helmetPickup.HideLabel();
            }

            if (hitInfo.collider.gameObject.TryGetComponent(out HelmetPickup targetedHelmet))
            {
                targetedHelmet.ShowLabel();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    targetedHelmet.Interact();
                }
            }
            else if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactObj.Interact();
                }
            }
        }
        else
        {
            foreach (var helmetPickup in FindObjectsOfType<HelmetPickup>())
            {
                helmetPickup.HideLabel();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("PlayerHealth component not found!");
        }
    }

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
                hammerAnimator?.SetTrigger("Attack");
                break;
            case "Weapon 2":
                metalBatAnimator?.SetTrigger("Attack");
                break;
            case "Weapon 3":
                broomAnimator?.SetTrigger("Attack");
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
    }
}
