using UnityEngine;

public class LineOfSight : MonoBehaviour
{

    public Transform target;
    public float viewDistance = 10f;
    public float fieldOfView = 90f;
    public LayerMask obstacleMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public bool CanSeeTarget(GameObject target)
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        // Zeichne den Raycast in der Scene-Ansicht (gelb)
        Debug.DrawRay(transform.position, directionToTarget * distanceToTarget, Color.yellow);

        if (distanceToTarget < viewDistance)

        {
            //Debug.Log("Ziel ist in Reichweite!");
            return true;
        }
         
        else
        {
            //Debug.Log("Ziel ist außerhalb der Reichweite.");
            return false;
        }


    
    }
}
