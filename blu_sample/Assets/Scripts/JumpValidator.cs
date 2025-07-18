using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpValidator : MonoBehaviour
{
    // Start is called before the first frame update
    public bool jumpAbility;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        jumpAbility = true;
    }

    void OnCollisionExit(Collision collision)
    {
        jumpAbility = false;
    }
}
