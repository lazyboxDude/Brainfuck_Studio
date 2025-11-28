using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Range Detector", story: "Update Range of [Detector] and assing [Target]", category: "Action", id: "cc5f4490d88fc6e8b91e7de8ab5a4cd8")]
public partial class RangeDetectorAction : Action
{
    [SerializeReference] public BlackboardVariable<RangeDetector> Detector;
    [SerializeReference] public BlackboardVariable<GameObject> Target;

   
    protected override Status OnUpdate()
    {
        Target.Value = Detector.Value.UpdateDetector();
        return Detector.Value.UpdateDetector() != null ? Status.Success : Status.Failure;
    }


}

