using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Pickup currentHeld;
    [SerializeField]
    private float pickUpRange;

    void Awake()
    {
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
        if(currentHeld == null)
        {
            AttemptPickup();
        }
        // place held object
        else
        {
            AttemptPlace();
        }
    }

    private void AttemptPickup()
    {
        Transform cameraTrans = transform.Find("Main Camera");

        Ray viewRay = new Ray(cameraTrans.position, cameraTrans.forward);

        if (!Physics.Raycast(viewRay, out RaycastHit hitData, pickUpRange)
            || !hitData.collider.CompareTag("Pickupable"))
            return;

        Debug.Log("Picking up item");
        currentHeld = hitData.collider.gameObject.GetComponent<Pickup>();
        currentHeld.gameObject.GetComponent<BoxCollider>().enabled = false;

        currentHeld.PickUp();
        PlaceSpace previousRoost = currentHeld.GetComponentInParent<PlaceSpace>();
        if(previousRoost != null)
            previousRoost.PickUp();
        currentHeld.transform.SetParent(this.transform);
    }

    private void AttemptPlace()
    {
        Transform cameraTrans = transform.Find("Main Camera");

        Ray viewRay = new Ray(cameraTrans.position, cameraTrans.forward);

        if (!Physics.Raycast(viewRay, out RaycastHit hitData, pickUpRange)
            || !hitData.collider.CompareTag("PlaceSpace")
            || !hitData.collider.GetComponent<PlaceSpace>().isVacant)
            return;

        Debug.Log("Placing item");
        Transform heldTransform = currentHeld.transform;

        heldTransform.SetParent(hitData.collider.gameObject.transform.Find("Roost Top"));
        heldTransform.localPosition = Vector3.zero;
        heldTransform.localEulerAngles = Vector3.zero;
        currentHeld.gameObject.GetComponent<BoxCollider>().enabled = true;
        currentHeld.Place();
        hitData.collider.GetComponent<PlaceSpace>().Place();
        currentHeld = null;
    }
}
