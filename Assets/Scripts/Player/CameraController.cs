using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float camSensitivityX;
    [SerializeField]
    private float camSensitivityY;
    private GameObject playerCam;

    private float xRotation;
    private float yRotation;
    private float xRotationMin = -80;
    private float xRotationMax = 55;

    void Awake()
    {
        playerCam = GameObject.Find("Main Camera");
        xRotation = yRotation = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("Camera Controller loaded");
    }

    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        yRotation += horizontalInput * camSensitivityX;
        xRotation -= verticalInput * camSensitivityY;
        xRotation = Mathf.Clamp(xRotation, xRotationMin, xRotationMax);

        transform.eulerAngles = new Vector3(0.0f, yRotation, 0.0f);
        playerCam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}
