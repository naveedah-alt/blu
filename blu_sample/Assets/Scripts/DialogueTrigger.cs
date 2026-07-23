using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DialogueTrigger : MonoBehaviour
{
    //This script manages triggering the Dialogue Game
    //Objects and cycling through them. Does not handle actual
    //on screen text visuals. See [Dialouge]

    [SerializeField]
    public List<GameObject> triggerObjects = new List<GameObject>();
    public bool interactable; //used to determine if this trigger is interactable or automatic

    public int counter = 0; // number of times player has seen the dialogue
    bool nearMe = false;

    // Update is called once per frame
    void Update()
    {
        if (triggerObjects.Count != 0 && triggerObjects != null && counter < triggerObjects.Count && interactable)

        {
            //test for if enter is pressed, the nearMe flag is true and if this is iteractable
            if (Input.GetKeyDown(KeyCode.Return) && nearMe && !activeListItem())
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

    public bool activeListItem()
    {
        bool activeStatus = false;
        for (int i = 0; i < triggerObjects.Count; i++)
        {
            if (triggerObjects[i].activeSelf)
            {
                activeStatus = true;
            }
        }
        return this && activeStatus;
    } 
}
