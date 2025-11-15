using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private float distance = 200f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playaerUI;
    private InputManager inputManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playaerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playaerUI.UpddateText(string.Empty);
        //create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);
        RaycastHit hitInfo; //variable to Store Raycast Hit information
        
        //check if the ray hits an interactable object within distance
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playaerUI.UpddateText(interactable.promptMessage);
                
                LookAtObject();

                if (inputManager.onFootActions.Interact.triggered)
                {
                    interactable.BaseInteract();
                }

            }
        }
    }


    void LookAtObject()
    {
       //Debug.Log("Looking at interactable object");
        
        // Add highlight, show UI, etc.
    }
}
