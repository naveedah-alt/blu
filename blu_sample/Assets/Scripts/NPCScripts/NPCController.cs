using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

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
        origin = gameObject.transform.forward;
        direction = true;
        step = speed * Time.deltaTime;
        currentPathIndex = 0;
        targetPathIndex = 0;
        this.transform.LookAt(this.transform.position + this.transform.forward);
    }

    void FixedUpdate()
    {
        
        //Collision Detection, won't move if object present, otherwise proceeds with movement code
        if (Physics.Raycast(transform.position, movingDirection, out obstacleInFront, 2f))
        {
            //|| !obstacleInFront.collider.CompareTag("Wall")
            if (obstacleInFront.collider.CompareTag("Wall") || obstacleInFront.collider.gameObject.layer != 6 )
            {
                Debug.DrawRay(transform.position, movingDirection, Color.blue);
                // Debug.Log("hit something! rotating now!");
                Vector3 perpendicularMovement = new Vector3(-movingDirection.z, movingDirection.y, movingDirection.x);
                transform.Translate(perpendicularMovement.normalized*0.05f);
                // movingDirection = transform.position;
                // transform.rotation = new Quaternion(0.0f, gameObject.transform.rotation.y, 0.0f, 2f);
                // movingDirection = transform.position
                // transform.rotation = new Quaternion(0.0f, gameObject.transform.position.y, 0.0f, 1f);
                // Vector3 nextDestination = patrolPath.GetDestinationOnPath(gameObject.transform, currentPathIndex);
                // float goalAngle = Mathf.MoveTowardsAngle(gameObject.transform.position.x, nextDestination.x, speed * Time.deltaTime);
                
                // if (goalAngle > 0.0f)
                // {
                //     Debug.Log("rotating positive way");
                //     transform.rotation = new Quaternion(0.0f, gameObject.transform.position.y, 0.0f, 2f);
                // } else
                //     Debug.Log("rotating negative way");
                //     transform.rotation = new Quaternion(0.0f, gameObject.transform.position.y, 0.0f, -2f);
                // transform.rotation = new Quaternion(gameObject.transform.position.x, 0.0f, 0.0f, 1f);
                
                
                // Vector3 nextDestination = patrolPath.GetDestinationOnPath(gameObject.transform, currentPathIndex);
                // float goalAngle = Mathf.MoveTowardsAngle(gameObject.transform.eulerAngles.y, nextDestination.y, speed * Time.deltaTime);
                // if (goalAngle > 0)
                // {
                //     transform.rotation = new Quaternion(gameObject.transform.position.x, 0.0f, 0.0f, 2f);
                // } else
                //     transform.rotation = new Quaternion(gameObject.transform.position.x, 0.0f, 0.0f, -2f);
                // if (Physics.Raycast(transform.position, movingDirection, out obstacleInFront, 5f)){
                //     Debug.Log("hit something! rotating now!");

                    
                // }

                
            } 
        }
        else
        {
            Debug.DrawRay(transform.position, movingDirection, Color.red);
            //This Indexing relies on [NPCPatrol] which accounts for out of bounds and reverse order
            currentPathIndex = patrolPath.UpdatePathDestination(gameObject.transform, currentPathIndex);
            Vector3 nextDestination = patrolPath.GetDestinationOnPath(gameObject.transform, currentPathIndex);
            // Quaternion goalRotation = Quaternion.FromToRotation(transform.position, transform.forward);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, 1.0f);
            patrol(currentPathIndex, nextDestination);
        }
    }

    void patrol(int i, Vector3 destination)
    {
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