using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
     GameObject targetObject;
    [SerializeField]
    GameObject targetObject2;

    int counter = 0; // number of times player has seen the dialogue

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        targetObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (counter == 0)
        {
            targetObject.SetActive(true);
            this.gameObject.SetActive(false);
            counter++;
        }
        if (counter >= 1)
        {
            targetObject2.SetActive(true);
            this.gameObject.SetActive(false);
        }
        }
}
