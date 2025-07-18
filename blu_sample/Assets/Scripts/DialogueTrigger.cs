using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;
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
        targetObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
