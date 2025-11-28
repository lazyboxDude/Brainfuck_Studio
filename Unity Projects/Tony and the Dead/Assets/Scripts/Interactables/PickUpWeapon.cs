using Unity.Behavior.Example;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpWeapon : Interactable
{

    public BasicWeapon weaponPrefab;
    public Transform weaponParent;
    private BasicWeapon weapon;

    public int weaponCost;
    private void Start()
    {

    }

    protected override void Interact()
    {
        PickUp();
    }

    private void PickUp()
    {
        var PlayerPoints = GlobalReferences.instance.GetPoints();
        if (PlayerPoints >= weaponCost)
        {
            Debug.Log("Congrats for for your new Weapon!");
            GlobalReferences.instance.AddPoints(-weaponCost);
            instanceWeaponToActiveSlot();
        }

    }
    
    public void instanceWeaponToActiveSlot()
    {
        weapon = Instantiate(weaponPrefab, transform.position, Quaternion.Euler(weaponPrefab.spawnRotation));

        //set the weapon as a child of the weapon parent
        weapon.transform.SetParent(weaponParent);

        //reset the local position and rotation
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localEulerAngles = Vector3.zero;

        //disable the collider and this script
        weapon.GetComponent<Collider>().enabled = false;
        this.enabled = false;
        //turn on the weapon
        weapon.isActiveWeapon = true;
    }

    private void AddWeaponToActiveSlot()
    {
        GlobalReferences.instance.activeWeaponSlot = weapon.gameObject;
    }

 }

