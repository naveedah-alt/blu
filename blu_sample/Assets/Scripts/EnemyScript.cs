using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> points;
    [SerializeField]
    private List<Vector3> patrolPoints = new List<Vector3>();
    [SerializeField]
    private List<Vector3> patrolPointsReverse = new List<Vector3>();
    int targetPoint;
    public int speed;
    float step;
    [SerializeField]
    private bool direction;
    Vector3 origin;
    [SerializeField]
    int currentIndex;
    RaycastHit obstacleInFront;
    Vector3 movingDirection;
    void Start()
    {
        movingDirection = new Vector3();
        obstacleInFront = new RaycastHit();
        currentIndex = 0;
        origin = gameObject.transform.position;
        direction = true;
        step = speed * Time.deltaTime;
        for (int i = 0; i < points.Count; i++)
        {
            Vector3 currentVect = new Vector3(points[i].transform.position.x, gameObject.transform.position.y, points[i].transform.position.z);
            patrolPoints.Add(currentVect);
        }
        targetPoint = 0;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            patrolPointsReverse.Add(patrolPoints[(patrolPoints.Count-1)-i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    // private void OnDrawGizmos()
    // {
    //     // Set the color with custom alpha.
    //     Gizmos.color = new Color(0f, 1f, 0f); // Green with custom alpha

    //     // Draw the ray.
    //     Gizmos.DrawRay(transform.position, Vector3.forward.normalized * 5f);

    //     // Draw a sphere at the end of the ray.
    //     Gizmos.DrawSphere(transform.position + Vector3.forward.normalized * 5f, 0.1f);
    // }

    void FixedUpdate()
    {

        if(Physics.Raycast(transform.position,movingDirection, out obstacleInFront, 1f))             
        {  
            if (obstacleInFront.collider.gameObject.layer != 3 && obstacleInFront.collider.gameObject.layer != 6)                 
            {
                Debug.Log("hit something! rotating now!");
                transform.rotation = 
                //Quaternion.Inverse(Quaternion.FromToRotation(Vector3.forward, movingDirection));
                //Quaternion.FromToRotation(movingDirection, Vector3.forward);
                new Quaternion(gameObject.transform.position.x, 0.0f, 0.0f, 2f);
                Debug.DrawRay(transform.position, movingDirection, Color.blue); 
            }
        } else
        {
            Debug.DrawRay(transform.position, movingDirection, Color.red);
            if (currentIndex < patrolPoints.Count)
            {
                if(direction)
                {
                    patrol(currentIndex);
                    // if (transform.position != patrolPoints[currentIndex])
                    // {
                    //     patrol(currentIndex);
                    // } else
                    // {
                    //     currentIndex++;
                    // }
                } else
                {
                    counterPatrol(currentIndex);
                    // if (transform.position != patrolPointsReverse[currentIndex])
                    // {
                    //     counterPatrol(currentIndex);
                    // } else
                    // {
                    //     currentIndex++;
                    // }
                }
            } 
        }
        // if (currentIndex < patrolPoints.Count)
        // {
        //     if(direction)
        //     {
        //         patrol(currentIndex);
        //         // if (transform.position != patrolPoints[currentIndex])
        //         // {
        //         //     patrol(currentIndex);
        //         // } else
        //         // {
        //         //     currentIndex++;
        //         // }
        //     } else
        //     {
        //         counterPatrol(currentIndex);
        //         // if (transform.position != patrolPointsReverse[currentIndex])
        //         // {
        //         //     counterPatrol(currentIndex);
        //         // } else
        //         // {
        //         //     currentIndex++;
        //         // }
        //     }
        // } 
        // else
        // {
        //     currentIndex = 0;
        //     if (direction)
        //     {
        //         direction = false;
        //     } else
        //     {
        //         direction = true;
        //     }
        // }
    }

    void patrol(int i)
    {  
        if (i == patrolPoints.Count)
        {
            direction = false;
            //currentIndex = 0;
        }
        else {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPoints[i], step);
            movingDirection = (patrolPoints[i]-transform.position).normalized;
        }   
    }
    

    void counterPatrol(int i)
    {
        if (i == patrolPointsReverse.Count)
        {
            direction = true;
            //currentIndex = 0;
        }
        else {
            transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPointsReverse[i], step);
            movingDirection = (patrolPointsReverse[i]-transform.position).normalized;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<Trigger>() == true)
        {
            if (currentIndex < points.Count)
            {
                Debug.Log("incrementing currentindex");
                currentIndex++;
            }
            if (currentIndex >= points.Count)
            {
                Debug.Log("zeroing out currentindex");
                currentIndex = 0;
            }
            
        }
    }
}
//         if (collision.collider.gameObject.layer != 3)
//         {
//             // transform.rotation = new Quaternion(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, 0f);
//             Rigidbody constraints = gameObject.GetComponent<Rigidbody>();
//             constraints.constraints = RigidbodyConstraints.FreezeRotation;

//             speed *=4;

//             if(direction)
//             {
//                 if (transform.position != patrolPoints[currentIndex])
//                 {
//                     patrol(currentIndex);
//                 } else
//                 {
//                     currentIndex++;
//                 }
//             } else
//             {
//                 if (transform.position != patrolPointsReverse[currentIndex])
//                 {
//                     counterPatrol(currentIndex);
//                 } else
//                 {
//                     currentIndex++;
//                 }
//             }
//         }
        
        
//     }

//     void OnCollisionExit(Collision collision)
//     {
//         if (collision.collider.gameObject.layer != 3)
//         {
//             // transform.rotation = new Quaternion(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z, 0f);
//             Rigidbody constraints = gameObject.GetComponent<Rigidbody>();
//             constraints.constraints = RigidbodyConstraints.None;
//             constraints.constraints = RigidbodyConstraints.FreezePositionY;
//             //constraints.constraints = RigidbodyConstraints.FreezeRotationZ;

            

//             if(direction)
//             {
//                 if (transform.position != patrolPoints[currentIndex])
//                 {
//                     patrol(currentIndex);
//                 } else
//                 {
//                     currentIndex++;
//                 }
//             } else
//             {
//                 if (transform.position != patrolPointsReverse[currentIndex])
//                 {
//                     counterPatrol(currentIndex);
//                 } else
//                 {
//                     currentIndex++;
//                 }
//             }

//             speed /=4;
//         }
//     }
// }
