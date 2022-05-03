using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Pickup currentHeld;
    [SerializeField]
    private float pickUpRange;
    [SerializeField]
    private float placeRange;

    private GameManager gameManager;
    [SerializeField]
    private GridHandler gridHandler;

    [SerializeField]
    private Transform handTransform;
    private Transform cameraTrans;

    void Awake()
    {
        currentHeld = null;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gridHandler.Activate(false);

        cameraTrans = transform.Find("Main Camera");
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (currentHeld != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
                gridHandler.RotateGhost();
            gridHandler.ProjectGhost(placeRange, cameraTrans);
        }

        
            gridHandler.ProjectGhost(placeRange, cameraTrans);
    }

    private void Interact()
    {
        Ray viewRay = new Ray(cameraTrans.position, cameraTrans.forward);
        bool didHit = Physics.Raycast(viewRay, out RaycastHit hitData, pickUpRange);

        if (!didHit)
            return;

        // pick up near object
        if (currentHeld == null && hitData.collider.CompareTag("Pickupable"))
        {
            Pickup(hitData);
        }
        // press world button
        else if (hitData.collider.CompareTag("WorldButton"))
        {
            PressButton(hitData);
        }
        // attempt place held object at ghost location
        else
        {
            // todo: rework
            // todo: query grid
            // disregard viewray

            // attempt place
            /* else if (currentHeld != null && hitData.collider.CompareTag("PlaceSpace")
            && hitData.collider.GetComponent<Chicken>().isActive)
            {
                Place(hitData);
            }
             */
        }
    }

    private void Pickup(RaycastHit hitData)
    {
        // todo: rework

        Debug.Log("Picking up item");
        currentHeld = hitData.collider.gameObject.GetComponent<Pickup>();
        currentHeld.gameObject.GetComponent<BoxCollider>().enabled = false;

        gridHandler.Activate(true);
        currentHeld.PickUp();

        // replace this with updating grid
        //Chicken previousRoost = currentHeld.GetComponentInParent<Chicken>();
        //if (previousRoost != null)
        //    previousRoost.PickUp();

        currentHeld.transform.SetParent(handTransform);
        currentHeld.transform.localPosition = Vector3.zero;
        currentHeld.transform.localEulerAngles = Vector3.zero;
    }

    private void Place(RaycastHit hitData)
    {
        // todo: rework
        Debug.Log("Placing item");
        Transform heldTransform = currentHeld.transform;

        heldTransform.SetParent(hitData.collider.gameObject.transform.Find("Roost Top"));
        heldTransform.localPosition = Vector3.zero;
        heldTransform.localEulerAngles = Vector3.zero;
        currentHeld.gameObject.GetComponent<BoxCollider>().enabled = true;
        currentHeld.Place();
        hitData.collider.GetComponent<Chicken>().Place();
        currentHeld = null;

        gridHandler.Activate(false);
    }

    private void PressButton(RaycastHit hitData)
    {
        // if adding other buttons, check button type here

        Debug.Log("Pressing button");
        gameManager.StartButton();

        hitData.collider.gameObject.GetComponent<WorldButton>().Press();
    }
}
