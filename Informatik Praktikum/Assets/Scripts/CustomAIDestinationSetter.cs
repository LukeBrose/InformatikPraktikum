using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; 

public class CustomAIDestinationSetter : AIDestinationSetter
{
    public AI_Patrol ai_patrol; 

    protected override void Update()
    {
        if (target != null && ai != null && ai_patrol.insight)
            ai.destination = target.position;
    }
}
