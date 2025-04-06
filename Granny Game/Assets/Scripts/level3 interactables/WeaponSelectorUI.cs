using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectorUI : MonoBehaviour
{
    public GameObject uiPanel;
    public Button weapon1Button;
    public Button weapon2Button;
    public Button weapon3Button;

    private bool isUIOpen = false;
    private bool hasUIBeenOpened = false; // New flag to ensure UI only opens once
    private PadLockPassword _padLockPassword; // Reference to PadLockPassword

    void Start()
    {
        _padLockPassword = FindObjectOfType<PadLockPassword>(); // Find the PadLockPassword script

        uiPanel.SetActive(false); // Start hidden
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        weapon1Button.onClick.AddListener(() => SelectWeapon("Weapon 1"));
        weapon2Button.onClick.AddListener(() => SelectWeapon("Weapon 2"));
        weapon3Button.onClick.AddListener(() => SelectWeapon("Weapon 3"));
    }

    void Update()
    {
        // only open UI once when password is correct and UI has not been opened yet
        if (_padLockPassword != null && _padLockPassword.PasswordCorrect && !isUIOpen && !hasUIBeenOpened)
        {
            OpenUI();
        }
    }

    void OpenUI()
    {
        Debug.Log("Opening Weapon Selector UI");

        isUIOpen = true;  
        hasUIBeenOpened = true;  
        uiPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void SelectWeapon(string weaponName)
    {
        Debug.Log("Weapon Selected: " + weaponName);

        Debug.Log("Closing Weapon Selector UI");

        uiPanel.SetActive(false);
        isUIOpen = false;  
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
}
