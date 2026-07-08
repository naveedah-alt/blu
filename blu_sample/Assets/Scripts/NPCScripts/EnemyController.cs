using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public NPCPatrol patrolPath;
    [SerializeField]
    int currentPathIndex;
    int targetPathIndex;
    public int speed;
    float step;
    [SerializeField]
    private bool direction;
    public SphereCollider roundCollider;
    Vector3 origin;
    [SerializeField]
  
    RaycastHit obstacleInFront;
    Vector3 movingDirection;
    Vector3 destination;
    public bool following;
    // Start is called before the first frame update
    void Start()
    {
        roundCollider.radius = 5.0f;
        following = false;
        destination = transform.position;
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

        // if (Physics.Raycast())
        // {
        //     if (obstacleInFront.collider.gameObject.layer == 7)
        //     {
                
        //         Debug.Log("hit player");
        //         follow(obstacleInFront.collider.gameObject);
        //     }
        // }
        if (!following)
        {
            roundCollider.radius = 5.0f;
                //Collision Detection, won't move if object present, otherwise proceeds with movement code
            if (Physics.Raycast(transform.position, movingDirection, out obstacleInFront, 2f))
            {
                //|| !obstacleInFront.collider.CompareTag("Wall")
                if (obstacleInFront.collider.CompareTag("Wall"))
                // || obstacleInFront.collider.gameObject.layer != 6 || obstacleInFront.collider.gameObject.layer != 7 )
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
                // } else if (obstacleInFront.collider.gameObject.layer == 7)
                // {
                //     Debug.DrawRay(transform.position, movingDirection, Color.green);
                //     Debug.Log("hit player");
                //     destination = obstacleInFront.collider.gameObject.transform.position;
                //     // follow(obstacleInFront.collider.gameObject);
                // }
            }
            else
            {
                Debug.DrawRay(transform.position, movingDirection, Color.red);
                //This Indexing relies on [NPCPatrol] which accounts for out of bounds and reverse order
                currentPathIndex = patrolPath.UpdatePathDestination(gameObject.transform, currentPathIndex);
                Vector3 nextDestination = patrolPath.GetDestinationOnPath(gameObject.transform, currentPathIndex);
                // Quaternion goalRotation = Quaternion.FromToRotation(transform.position, transform.forward);
                // transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, 1.0f);
                destination = nextDestination;
                patrol();
            } 
        } else
        {
            if (Physics.Raycast(transform.position, movingDirection, out obstacleInFront, 2f))
            {
                if (!obstacleInFront.collider.CompareTag("Player"))
                // || obstacleInFront.collider.gameObject.layer != 6 || obstacleInFront.collider.gameObject.layer != 7 )
                {
                    Debug.DrawRay(transform.position, movingDirection, Color.blue);
                    // Debug.Log("hit something! rotating now!");
                    Vector3 perpendicularMovement = new Vector3(-movingDirection.z, movingDirection.y, movingDirection.x);
                    transform.Translate(perpendicularMovement.normalized*0.05f);
                }
            } else
            {
                Debug.DrawRay(transform.position, movingDirection, Color.magenta);
                roundCollider.radius = 10.0f;
                patrol();
            }
            
        }
        
    }

    void patrol()
    {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, step);
            movingDirection = (destination - transform.position).normalized;
    }

    // void follow(GameObject playerObject)
    // {
    //     transform.position = Vector3.MoveTowards(gameObject.transform.position, playerObject.transform.position, step);
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            following = true;
            destination = other.gameObject.transform.position;
            //patrol();
            
        }
    }

    void OnTriggerStay(Collider other)
    {
         if (other.gameObject.tag == "Player")
        {
            following = true;
            destination = other.gameObject.transform.position;
            //patrol();
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            following = false;
        }
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
