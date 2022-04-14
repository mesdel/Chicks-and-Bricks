using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Pickup currentHeld;
    private Pickup currentNear;

    void Awake()
    {
        currentNear = null;
        currentHeld = null;
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ChickenInteract();
        }
    }

    private void ChickenInteract()
    {
        // pick up near object
        if(currentHeld == null && currentNear != null)
        {
            Debug.Log("Picking up item");
            currentHeld = currentNear;
            currentHeld.gameObject.GetComponent<BoxCollider>().enabled = false;
            currentHeld.transform.SetParent(this.transform);
        }
        // place held object
        else if(currentHeld != null)
        {
            Debug.Log("Placing item");
            currentHeld.transform.SetParent(null);
            currentHeld.gameObject.GetComponent<BoxCollider>().enabled = true;
            currentHeld = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickupable"))
        {
            Debug.Log("Item in range");
            currentNear = other.gameObject.GetComponentInParent<Pickup>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickupable"))
        {
            Debug.Log("Item not in range");
            currentNear = null;
        }
    }
}
