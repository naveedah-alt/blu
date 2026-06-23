using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    public Collider boxCollider;
    public Collider topCollider;
    // Start is called before the first frame update
    void Start()
    {
        //boxCollider = gameObject.GetComponent<Collider>();
        //boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided!");
        if (collision.gameObject.tag != "Feet")
        {
            topCollider.excludeLayers = 3;
            boxCollider.enabled = false;
        } else
        {
            boxCollider.enabled = true;
        }
    }
}
