using System.Collections;
using System.Collections.Generic;
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
    int currentIndex;
    void Start()
    {
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

    void FixedUpdate()
    {
        if (currentIndex < patrolPoints.Count)
        {
            if(direction)
            {
                if (transform.position != patrolPoints[currentIndex])
                {
                    patrol(currentIndex);
                } else
                {
                    currentIndex++;
                }
            } else
            {
                if (transform.position != patrolPointsReverse[currentIndex])
                {
                    counterPatrol(currentIndex);
                } else
                {
                    currentIndex++;
                }
            }
        } else
        {
            currentIndex = 0;
            if (direction)
            {
                direction = false;
            } else
            {
                direction = true;
            }
        }
    }

    void patrol(int i)
    {  
        transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPoints[i], step);   
    }
    

    void counterPatrol(int i)
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, patrolPointsReverse[i], step);
    }
}
