using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectorUI : MonoBehaviour
{
    public GameObject uiPanel;
    public Button weapon1Button;
    public Button weapon2Button;
    public Button weapon3Button;

    public static string CurrentWeapon = "";

    private bool isUIOpen = false;
    private bool hasUIBeenOpened = false;

    private PadLockPassword _padLockPassword;
    private GameObject chestTop;
    private GameObject padlock;
    private GameObject metalBat;
    private GameObject hammer;
    private GameObject broom;

    void Start()
    {
        _padLockPassword = FindObjectOfType<PadLockPassword>();
        chestTop = GameObject.FindWithTag("chestTop");
        padlock = GameObject.FindWithTag("padlock");

        metalBat = GameObject.FindWithTag("bat");
        hammer = GameObject.FindWithTag("hammer");
        broom = GameObject.FindWithTag("broom");

        metalBat?.SetActive(false);
        hammer?.SetActive(false);
        broom?.SetActive(false);

        uiPanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        weapon1Button.onClick.AddListener(() => SelectWeapon("Weapon 1"));
        weapon2Button.onClick.AddListener(() => SelectWeapon("Weapon 2"));
        weapon3Button.onClick.AddListener(() => SelectWeapon("Weapon 3"));
    }

    void Update()
    {
        if ((_padLockPassword != null && _padLockPassword.PasswordCorrect && !isUIOpen && !hasUIBeenOpened) || Input.GetKeyDown(KeyCode.P))
        {
            OpenUI();
        }
    }

    void OpenUI()
    {
        isUIOpen = true;
        hasUIBeenOpened = true;
        uiPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        chestTop?.SetActive(false);
        padlock?.SetActive(false);
    }

    void SelectWeapon(string weaponName)
    {
        CurrentWeapon = weaponName;

        metalBat?.SetActive(false);
        hammer?.SetActive(false);
        broom?.SetActive(false);

        switch (weaponName)
        {
            case "Weapon 1": hammer?.SetActive(true); break;
            case "Weapon 2": metalBat?.SetActive(true); break;
            case "Weapon 3": broom?.SetActive(true); break;
        }

        FindObjectOfType<Player>().UpdateWeaponStats();

        uiPanel.SetActive(false);
        isUIOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
