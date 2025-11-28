using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "LoadedPatrol", story: "[Self] patrol Points on the Map", category: "Action/Navigation", id: "35f765323a9904b1be94efa227c33fbc")]
public partial class LoadedPatrolAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;

    // List to store found patrol points
    private System.Collections.Generic.List<GameObject> patrolPoints;

    protected override Status OnStart()
    {
        // Find all GameObjects with the tag "PatrolPoints"
        var foundPoints = GameObject.FindGameObjectsWithTag("PatrolPoints");
        patrolPoints = new System.Collections.Generic.List<GameObject>(foundPoints);
        Debug.Log($"Found {patrolPoints.Count} patrol points.");
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

