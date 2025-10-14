using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class ClickToMove : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent BaseAgent;
    [SerializeField]
    private NavMeshAgent ShortAgent;
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private LayerMask LayerMask;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Mouse.current.leftButton.wasPressedThisFrame 
            && Physics.Raycast(ray, out RaycastHit hit, maxDistance: float.MaxValue, LayerMask))
        {
            BaseAgent.SetDestination(hit.point);
        }
        if (Mouse.current.rightButton.wasPressedThisFrame 
            && Physics.Raycast(ray, out RaycastHit hit2, maxDistance: float.MaxValue, LayerMask))
        {
            ShortAgent.SetDestination(hit2.point);
        }
    }
}
