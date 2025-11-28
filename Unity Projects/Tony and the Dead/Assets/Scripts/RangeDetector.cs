using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    [SerializeField] float detectionRadius = 10f;
    [SerializeField] LayerMask detectionMask;

    [SerializeField] private bool showDebugGizmos = true;
    public GameObject DetectedTarget
    {
        get;
        set;

    }

    public GameObject UpdateDetector()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
        if (hitColliders.Length > 0)
        {
            DetectedTarget = hitColliders[0].gameObject;
        }
        else
        {
            DetectedTarget = null;
        }   
        return DetectedTarget;
    }

    //debug gizmos
    private void OnDrawGizmos()
    {
        if (showDebugGizmos)
        {
            Gizmos.color = DetectedTarget ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
