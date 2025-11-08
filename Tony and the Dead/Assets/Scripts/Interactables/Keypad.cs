using UnityEngine;

public class Keypad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;
    public string regionName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void UnlockRegion()
    {
        doorOpen = true;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        
        // Schalte die Region mit diesem Namen frei
        if (WaveSpawner.Instance != null && !string.IsNullOrEmpty(regionName))
        {
            var region = System.Array.Find(WaveSpawner.Instance.spawnRegions, r => r.regionName == regionName);
            if (region != null)
            {
                region.isUnlocked = true;
                Debug.Log($"Region {regionName} unlocked!");
            }
        }
    }

    // this function is where we design what happens when we interact with this object
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        
        // Region freischalten wenn Tür geöffnet wird
        if (doorOpen)
        {
            UnlockRegion();
        }
    }
}
