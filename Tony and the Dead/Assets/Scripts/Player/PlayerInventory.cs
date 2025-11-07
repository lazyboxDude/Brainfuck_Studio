using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInventory : MonoBehaviour
{
    //public GameObject activeWeaponSlot = GlobalReferences.instance.activeWeaponSlot;
    public Transform[] weaponSlots;
    public BasicWeapon currentWeapon;
    public Transform weaponParent;
    public BasicWeapon weaponPrefab;
    
    private InputManager inputManager;
    public string[] weaponSlotsInventory = new string[] { "WeaponSlot1", "WeaponSlot2", "WeaponSlot3" };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputManager = GetComponent<InputManager>();
        instanceWeaponToActiveSlot();
    }

    // Update is called once per frame
    void Update()
    {
        OnEnable();
    }
    public void instanceWeaponToActiveSlot()
    {
        currentWeapon = Instantiate(weaponPrefab, transform.position, Quaternion.Euler(weaponPrefab.spawnRotation));

        //set the weapon as a child of the weapon parent
        currentWeapon.transform.SetParent(weaponParent);

        //reset the local position and rotation
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localEulerAngles = Vector3.zero;

        //disable the collider and this script
        currentWeapon.GetComponent<Collider>().enabled = false;
        this.enabled = false;
        //turn on the weapon
        currentWeapon.isActiveWeapon = true;
    }

    private void OnEnable()
    {
        if (inputManager == null)
            inputManager = GetComponent<InputManager>();
        SwitchWeapon(new InputAction.CallbackContext());
    }


    private void SwitchWeapon(InputAction.CallbackContext context)
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Hier deine Waffenwechsel-Logik
            Debug.Log("Switching Weapon");
        }
    }
}
