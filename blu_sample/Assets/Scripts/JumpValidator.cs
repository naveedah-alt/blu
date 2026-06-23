using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Ignore this file
*/
public class JumpValidator : MonoBehaviour
{
    // Start is called before the first frame update
    public bool jumpAbility;
    [SerializeField]
    private Animator anim;
    private int groundContacts = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            groundContacts++;
            if (groundContacts > 0)
            {
                jumpAbility = true;
                anim.SetBool("Jump", false);
                Debug.Log("Grounded");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            groundContacts--;
            if (groundContacts <= 0)
            {
                jumpAbility = false;
                Debug.Log("Airborne");
            }
        }
    }
}
