using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Line of Sight Check", story: "Check [Target] with [LineofSightDetector]", category: "Conditions", id: "50fe2c2da9e9fcdc687a55e0cd343e21")]
public partial class LineOfSightCheckCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<LineOfSight> LineofSightDetector;

    public override bool IsTrue()
    {
        return LineofSightDetector.Value.CanSeeTarget(Target.Value);
    }


}
