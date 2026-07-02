using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    //A simplified and modifiable version of the [EnemyScript]
    //works in tandem with [NPCPatrol], but this file handles
    //actual NPC movement from the returned tracking in [NPCPatrol]

    // Start is called before the first frame update
    public NPCPatrol patrolPath;
    [SerializeField]
    int currentPathIndex;
    int targetPathIndex;
    public int speed;
    float step;
    [SerializeField]
    private bool direction;
    Vector3 origin;
    [SerializeField]
  
    RaycastHit obstacleInFront;
    Vector3 movingDirection;

    void Start()
    {
        movingDirection = new Vector3();
        obstacleInFront = new RaycastHit();
        origin = gameObject.transform.position;
        direction = true;
        step = speed * Time.deltaTime;
        currentPathIndex = 0;
        targetPathIndex = 0;
    }

    void FixedUpdate()
    {
        //Collision Detection, won't move if object present, otherwise proceeds with movement code
        if (Physics.Raycast(transform.position, movingDirection, out obstacleInFront, 2f))
        {
            if (obstacleInFront.collider.gameObject.layer != 3 && obstacleInFront.collider.gameObject.layer != 6)
            {
                Debug.Log("hit something! rotating now!");
                transform.rotation = new Quaternion(gameObject.transform.position.x, 0.0f, 0.0f, 2f);
                Debug.DrawRay(transform.position, movingDirection, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, movingDirection, Color.red);
            //This Indexing relies on [NPCPatrol] which accounts for out of bounds and reverse order
            currentPathIndex = patrolPath.UpdatePathDestination(gameObject.transform, currentPathIndex);
            Vector3 nextDestination = patrolPath.GetDestinationOnPath(gameObject.transform, currentPathIndex);
            
            patrol(currentPathIndex, nextDestination);
        }
    }

    void patrol(int i, Vector3 destination)
    { //currentIndex = 0;
            transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, step);
            movingDirection = (destination - transform.position).normalized;
    }


    //Uneccessary thanks to [NPC Patrol]
//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.collider.gameObject.GetComponent<Trigger>() == true)
//        {
//            if (currentPathIndex < points.Count)
//            {
//                Debug.Log("incrementing currentindex");
//                currentIndex++;
//            }
//            if (currentIndex >= points.Count)
//            {
//                Debug.Log("zeroing out currentindex");
//                currentIndex = 0;
//            }

//        }
//    }
//}
}