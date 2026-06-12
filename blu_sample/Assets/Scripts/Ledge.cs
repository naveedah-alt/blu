using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private Vector3 handPos, bodyPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LedgeChecker")
        {
            BluEngine player = other.transform.parent.GetComponent<BluEngine>();
            player.LedgeGrabbed(handPos, gameObject.GetComponent<Ledge>());
        }
    }

    public Vector3 StandUpProgress()
    {
        return bodyPos;
    }
}
