using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //public GameObject activeWeaponSlot = GlobalReferences.instance.activeWeaponSlot;
    public Transform[] weaponSlots;
    public BasicWeapon currentWeapon;
    public Transform weaponParent;
    public BasicWeapon weaponPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instanceWeaponToActiveSlot();
    }

    // Update is called once per frame
    void Update()
    {

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
}
