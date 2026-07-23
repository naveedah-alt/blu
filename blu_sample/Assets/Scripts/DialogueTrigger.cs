using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> triggerObjects = new List<GameObject>();
    public bool interactable; //used to determine if this trigger is interactable or automatic

    public int counter = 0; // number of times player has seen the dialogue
    bool nearMe = false;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (triggerObjects.Count != 0 && triggerObjects != null && counter < triggerObjects.Count && interactable)

        {
            //test for if enter is pressed, the nearMe flag is true and if this is iteractable
            if (Input.GetKeyDown(KeyCode.Return) && nearMe)
            {
                Debug.Log("Inside");
                triggerObjects[counter].SetActive(true);
                //triggerObjects[counter].SetActive(false);
                counter++;
            }

        }
        else
        {
            counter = 0;
        }
    }
    private void OnTriggerEnter(Collider trigger)
    {
        //We'll use this simply to flag that the player is now nearby
        nearMe = true;
        if (!interactable)
        {
            Debug.Log("made it");
            triggerObjects[counter].SetActive(true);
            //triggerObjects[counter].SetActive(false);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //The player has left the active zone
        nearMe = false;
    }

}
