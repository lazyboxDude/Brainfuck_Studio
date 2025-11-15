using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasicWeapon : MonoBehaviour
{
    //Input System
    private InputManager inputManager;

    #region Weapon Paramters

            //Shooting
    public bool isShooting, readyToShoot;
    bool alllowReset = true;
    public int weaponDamage;
    public float shootingDelay = 2f;
   // public float range;
  

            //Loading the weapon
    public float reloadTime;
    public int magazineSize, bulletLeft;
    public bool isReloading;

        //Burst 
    public int bulletsPerBurst = 3;
    public int burstBulletLeft;

    //Spread
    public float spreadIntensity;

    // Bullet Parameters
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletVelocity = 20f;
    public float bulletPrefabLifetime = 3f;

        //Shooting Mode
    public bool isActiveWeapon;
    public enum ShootingMode { Auto, Burst, Single }
    public ShootingMode currentShootingMode;
    // public bool Akimbo;
        
    #endregion

    #region Weapon Animation Parameters

        //Weapon Animation and Effects
    public GameObject MuzzleEffect;
    private Animator animator;


        //Spawn Position
    [SerializeField]
    private GameObject RightHandPoint;
    private Vector3 spawnPosition;
    private Vector3 spawnRotation;
        
    #endregion

    private void Awake()
    {
        //Get the Input Manager component
        inputManager = GetComponent<InputManager>();

        readyToShoot = true;
        burstBulletLeft = bulletsPerBurst;
        animator = GetComponent<Animator>();

        bulletLeft = magazineSize;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isActiveWeapon)
        {
            Vector3 forward = Camera.main.transform.forward;
            forward.y = 0; // Keep weapon level on the horizontal plane
            transform.forward = forward.normalized;
            OnEnable();
        }
        
    }
    
    private void OnEnable()
    {
        if (currentShootingMode == ShootingMode.Auto)
        {
            //Holing down the left Mouse Button
            isShooting = Input.GetKey(KeyCode.Mouse0);
        }
        else if (currentShootingMode == ShootingMode.Single ||
        currentShootingMode == ShootingMode.Burst)
        {
            // Clicking the left Mouse Button
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < magazineSize && !isReloading)
        {
            Reload();
        }

        //auto reload if we try to shoot with no bullets left
        if (readyToShoot && !isShooting && !isReloading && bulletLeft <= 0)
        {
            Reload();
        }
        //Shooting
        if (readyToShoot && isShooting && bulletLeft > 0 && !isReloading)
        {
            burstBulletLeft = bulletsPerBurst;
            FireWeapon();
        }

        if (AmmoManager.instance.ammoDisplay != null)
        {
            AmmoManager.instance.ammoDisplay.text = $"{bulletLeft / bulletsPerBurst}/{magazineSize / bulletsPerBurst}";
        }
    }
    private void FireWeapon()
    {
        bulletLeft--;

        MuzzleEffect.GetComponent<ParticleSystem>().Play();

        animator.SetTrigger("RECOIL");
        readyToShoot = false;

        SoundManager.instance.gunShotSound.Play();

        Vector3 shootingDirection = CalculateShootingDirectionAndSpread().normalized;
        //Create the bullet from the prefab
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        //Set the bullet damage
        BasicBullet bul = bullet.GetComponent<BasicBullet>();
        bul.bulletDamage = weaponDamage;

        
        
        
        //Rotate the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        //Get the Rigidbody component from the instantiated bullet and set its velocity
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        //Destroy the bullet after a certain time to clean up
        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));

        //checking if we done shooting  
        if (alllowReset)
        {
            Invoke("ResetShot", shootingDelay);
            alllowReset = false;
        }

        //Burst fire mode
        if (currentShootingMode == ShootingMode.Burst && burstBulletLeft > 1) //we already shot one bullet before this check
        {
            burstBulletLeft--;
            Invoke("FireWeapon", shootingDelay);
        }

    }

    private void Reload()
    {
        isReloading = true;
        //animator.SetTrigger("RELOAD");
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletLeft = magazineSize;
        isReloading = false;
    }
    private void ResetShot()
    {
        readyToShoot = true;
        alllowReset = true;
    }
    
    private Vector3 CalculateShootingDirectionAndSpread()
    {
        //Shooting direction is the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            //Hitting Something
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75); //A point far away in the distance
        }

        Vector3 direction = targetPoint - bulletSpawnPoint.position;
        float spreadX = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float spreadY = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        //return the direction with spread
        return direction + new Vector3(spreadX, spreadY, 0);
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
    


}
