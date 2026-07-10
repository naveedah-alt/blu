using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> triggerObjects = new List<GameObject>();


    int counter = 0; // number of times player has seen the dialogue

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider trigger)
    {
        if (counter >= 0 && triggerObjects.Count != 0 && triggerObjects != null)
        {
            triggerObjects[counter].SetActive(true);
            triggerObjects[counter].SetActive(false);
            counter++;
        }
        else
        {
            counter = 0;
        }
    }

}
