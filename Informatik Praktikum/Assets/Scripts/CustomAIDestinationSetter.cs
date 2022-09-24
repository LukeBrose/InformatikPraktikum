using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; 

public class CustomAIDestinationSetter : AIDestinationSetter
{
    //Abwandlung des AIDestinationSetter (vom A* Projekt): https://arongranberg.com/astar/ 
    public AI_Patrol ai_patrol; 

    protected override void Update()
    {
        // Nur wenn der Gegner den Spieler in Sicht hat, wird bei dem Schleim und der Fledermaus das A*-Pathfinding genutzt, um zu verfolgen
        if (target != null && ai != null && ai_patrol.insight)
            ai.destination = target.position;
    }
}
