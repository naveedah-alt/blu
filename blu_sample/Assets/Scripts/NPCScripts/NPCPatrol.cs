using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    //This script manages position tracking between patrol points
    //for player interactable NPCs.It uses Waypoints to
    //to return indexing and Vector 3 values. [See NPC Patrol]

    public List<Transform> pathNodes = new List<Transform>();
    public float pathReachingRadius = 1f;

    //Function for checking if Pathnodes List has a node
    private bool IsPathValid()
    {
        return this && pathNodes.Count > 0;
    }

    //Returns the position of a single pathNode at the next index once a waypoint is reached
    public Vector3 GetPositionOfPathNodes(int NodeIndex)
    {
        if (NodeIndex < 0 || NodeIndex >= pathNodes.Count || pathNodes[NodeIndex] == null)
        {
            return Vector3.zero;
        }

        return pathNodes[NodeIndex].position;
    }

    //Determines when to increment the next index
    public Vector3 GetDestinationOnPath(Transform agent, int pathDestinationNodeIndex)
    {
        if (IsPathValid())
        {
            return GetPositionOfPathNodes(pathDestinationNodeIndex);
        }
        else
        {
            return agent.position;
        }
    }

    public int UpdatePathDestination(Transform agent, int pathDestinationNodeIndex, bool inverseOrder = false)
    {
        //A Checking Function for if the NPC has made it to the next Node/Waypoint, will increment if so
        if (IsPathValid())
        {
            if ((agent.position - GetDestinationOnPath(agent, pathDestinationNodeIndex)).magnitude <= pathReachingRadius)
            {
                //Actual iteration for movement, will go in inverse order once it excedes number of set Path Points
                pathDestinationNodeIndex = inverseOrder ? (pathDestinationNodeIndex - 1) : (pathDestinationNodeIndex + 1);


                if (pathDestinationNodeIndex < 0)
                {
                    pathDestinationNodeIndex += pathNodes.Count;
                }

                if (pathDestinationNodeIndex >= pathNodes.Count)
                {
                    pathDestinationNodeIndex -= pathNodes.Count;
                }

            }
        }
        return pathDestinationNodeIndex;
    }
}
