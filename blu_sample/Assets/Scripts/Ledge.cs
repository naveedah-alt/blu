using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attached to Ledge objects in scene, specifically invisible ledge component.
/// </summary>
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

    /// <summary>
    /// Detects collision with collider (other) attached to player and starts ledge grabbing sequence
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LedgeChecker")
        {
            BluEngine player = other.transform.parent.GetComponent<BluEngine>();
            player.LedgeGrabbed(handPos, gameObject.GetComponent<Ledge>());
        }
    }

    /// <summary>
    /// Returns the position that the body of the player should be at, determined by placing another
    /// fbx model on the component itself and noting the transform position, and defined in the inspector.
    /// </summary>
    /// <returns></returns>
    public Vector3 StandUpProgress()
    {
        return bodyPos;
    }
}
