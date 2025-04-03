using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class WeaponSelectorUI : MonoBehaviour
{
    public GameObject uiPanel;
    public Button weapon1Button;
    public Button weapon2Button;
    public Button weapon3Button;

    private bool isUIOpen = false;
    private bool hasUIBeenOpened = false;
    private PadLockPassword _padLockPassword;
    private GameObject chestTop; // Declare here but initialize in Start
    private GameObject padlock; // Declare here but initialize in Start
    private GameObject metalBat; // Add reference to MetalBat
    private GameObject hammer;
    private GameObject broom;


    void Start()
    {
        _padLockPassword = FindObjectOfType<PadLockPassword>();
        chestTop = GameObject.FindWithTag("chestTop"); // Initialize here
        padlock = GameObject.FindWithTag("padlock"); // Initialize here

        // setting weapons disabled at first
        metalBat = GameObject.FindWithTag("bat");
        metalBat.SetActive(false);
        hammer = GameObject.FindWithTag("hammer");
        hammer.SetActive(false);
        broom = GameObject.FindWithTag("broom");
        broom.SetActive(false);


        if (metalBat == null)
        {   
            Debug.LogError("MetalBat not found with tag 'bat'");
        }
        if (chestTop == null)
        {
            Debug.LogError("Could not find object with tag 'chestTop'");
        }

        uiPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        weapon1Button.onClick.AddListener(() => SelectWeapon("Weapon 1"));
        weapon2Button.onClick.AddListener(() => SelectWeapon("Weapon 2"));
        weapon3Button.onClick.AddListener(() => SelectWeapon("Weapon 3"));
    }

    void Update()
    {
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
        
        if (chestTop != null && padlock != null)
        {
            chestTop.SetActive(false);
            padlock.SetActive(false);
        }
        else
        {
            Debug.LogWarning("chestTop reference is null when trying to disable it");
        }
    }

    void SelectWeapon(string weaponName)
    {
        Debug.Log("Weapon Selected: " + weaponName);
        Debug.Log("Closing Weapon Selector UI");

        uiPanel.SetActive(false);

        switch(weaponName) {
            case "Weapon 1": 
                if (hammer != null) {
                    hammer.SetActive(true);
                    Debug.Log("Equipping hammer");
                } break;
            case "Weapon 2": 
                if (metalBat != null) {
                    metalBat.SetActive(true);
                    Debug.Log("Equipping bat");
                } break;
            case "Weapon 3": 
                if (broom != null) {
                    broom.SetActive(true);
                    Debug.Log("Equipping Broom");
                } break;
            default:
            Debug.Log("No weapon found for " + weaponName);
            break;
        }

        isUIOpen = false;  
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        
    }
}