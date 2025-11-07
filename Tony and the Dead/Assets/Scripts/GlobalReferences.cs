using System.Collections.Generic;
using Unity.Behavior.Example;
using UnityEngine;
using TMPro;

public class GlobalReferences : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GlobalReferences instance { get; set; }

    private float points = 0;
    private WaveSpawner waveSpawner;


    public GameObject bulletImpactEffect;
    public TextMeshProUGUI pointsText;

    public List<GameObject> weaponSlots;

    public GameObject activeWeaponSlot;

    private void Start()
    {
        activeWeaponSlot = weaponSlots[0];
        ResetPoints();
    }

    private void Update()
    {
        // Set active weapon slot
        foreach (GameObject weaponSlot in weaponSlots)
        {
            if (weaponSlot == activeWeaponSlot)
            {
                weaponSlot.SetActive(true);

            }
            else
            {
                weaponSlot.SetActive(false);
            }
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void AddPoints(float amount)
    {
        points += amount;
        //Debug.Log("Points: " + points);
        // Update UI or other systems as needed
        UpdatePointsUI();

    }

    public float GetPoints()
    {
        return points;
    }

    public void ResetPoints()
    {
        points = 0;
        UpdatePointsUI();
    }

    public void UpdatePointsUI()
    {
        pointsText.text = "Points: " + GlobalReferences.instance.GetPoints();
    }

}
