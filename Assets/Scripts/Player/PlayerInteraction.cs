using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Chicken currentHeld;
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

    void Update()
    {
        if (Time.timeScale > 0 && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        if (currentHeld != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
                gridHandler.RotateGhost();
            gridHandler.ProjectGhost(placeRange, cameraTrans);
        }
    }

    private void Interact()
    {
        // place held object if ghost location is valid
        if (currentHeld != null && gridHandler.validGhost)
        {
            Place();
            return;
        }

        // if not placing, cast a ray from the camera for pickups or buttons
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
    }

    private void Pickup(RaycastHit hitData)
    {
        Debug.Log("Picking up item");

        // pick up chicken and deactivate it's collision box
        currentHeld = hitData.collider.gameObject.GetComponent<Chicken>();
        currentHeld.gameObject.GetComponent<BoxCollider>().enabled = false;

        // activate grid and call chicken's own pickup function
        gridHandler.Activate(true);
        currentHeld.PickUp();

        // set ghost chicken's rotation to mimic picked up chicken
        gridHandler.ghostTrans.rotation = currentHeld.transform.rotation;

        // set chicken's transform root to the player's hand
        currentHeld.transform.SetParent(handTransform);
        currentHeld.transform.localPosition = new Vector3(0, -0.5f, 0);
        currentHeld.transform.localEulerAngles = Vector3.zero;

        // update grid
        gridHandler.UpdateGrid();
    }

    private void Place()
    {
        Debug.Log("Placing item");
        Transform targetTrans = gridHandler.ghostTrans;
        Transform heldTransform = currentHeld.transform;

        // reset parent, copy ghost chicken's transform
        heldTransform.SetParent(gridHandler.chickens.transform);
        heldTransform.position = targetTrans.position;
        heldTransform.localEulerAngles = targetTrans.eulerAngles;

        // enable chicken's collider and call it's place
        currentHeld.gameObject.GetComponent<BoxCollider>().enabled = true;
        currentHeld.Place();
        currentHeld = null;

        // update grid
        gridHandler.UpdateGrid();

        // deactivate grid UI
        gridHandler.Activate(false);
    }

    private void PressButton(RaycastHit hitData)
    {
        // note: if adding other buttons, check button type here

        Debug.Log("Pressing button");
        gameManager.StartButton();

        hitData.collider.gameObject.GetComponent<WorldButton>().Press();
    }
}
