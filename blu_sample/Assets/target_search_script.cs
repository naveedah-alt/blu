using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class target_search_script : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform lockOnTarget; //target of player lock on

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Target Found");
            lockOnTarget = collision.gameObject.GetComponent(typeof(Transform)) as Transform;
            //  lockOnTarget = collision.gameObject; 
        }
    }
}
