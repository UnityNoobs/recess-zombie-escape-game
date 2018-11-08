using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Object Entered the trigger");
        transform.parent.GetComponent<CameraShake>().enabled = true;
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Object is withing the trigger");
        
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Object Exited the trigger");
        transform.parent.GetComponent<CameraShake>().enabled = false;
    }
}
