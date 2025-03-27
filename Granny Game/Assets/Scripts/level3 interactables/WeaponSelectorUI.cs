using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectorUI : MonoBehaviour
{
    public GameObject uiPanel;
    public Button weapon1Button;
    public Button weapon2Button;
    public Button weapon3Button;
    public KeyCode openKey = KeyCode.Q; // Press Q to open weapon selector

    private bool isUIOpen = false;

    void Start()
    {
        uiPanel.SetActive(false); // Start hidden
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        weapon1Button.onClick.AddListener(() => SelectWeapon("Weapon 1"));
        weapon2Button.onClick.AddListener(() => SelectWeapon("Weapon 2"));
        weapon3Button.onClick.AddListener(() => SelectWeapon("Weapon 3"));
    }

    void Update()
    {
        if (Input.GetKeyDown(openKey) && !isUIOpen)
        {
            OpenUI();
        }
    }

    void OpenUI()
    {
        isUIOpen = true;
        uiPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void SelectWeapon(string weaponName)
    {
        Debug.Log("Weapon Selected: " + weaponName);
        uiPanel.SetActive(false);
        isUIOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Call your weapon equip logic here if needed
        // EquipWeapon(weaponName);
    }
}
