using DG.Tweening;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.ProBuilder.Shapes;

public class DoorUnlocked : Interactable
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject[] Doorlamp;
    [SerializeField]
    public int doorCost;
    [SerializeField]
    private Material unlockedMaterial;
    [SerializeField]
    private Vector3 doorOpenPositionOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void Interact()
    {
        UnlockDoor();
        Debug.Log("Door is unlocked, you can pass through!");

    }
    
    public void UnlockDoor()
    {
        var PlayerPoints = GlobalReferences.instance.GetPoints();
        if (PlayerPoints >= doorCost)
        {
            GlobalReferences.instance.AddPoints(-doorCost);
            foreach (var lamp in Doorlamp)
            {
                lamp.GetComponent<Light>().color = Color.green;
                lamp.GetComponent<MeshRenderer>().material = unlockedMaterial;
            }
            //door.GetComponent<Transform>().position += doorOpenPositionOffset;
            //door.GetComponent<Collider>().enabled = false;
            transform.DOLocalMove(doorOpenPositionOffset, 1f).SetRelative();
            Destroy(this);
        }
        else
        {
            Debug.Log("Not enough points to unlock the door.");
        }
    }
}
