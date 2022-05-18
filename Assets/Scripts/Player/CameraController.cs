using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camSensitivity;
    private const float sensitivityScale = 2.0f;
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
        StartCoroutine(LoadSensitivity());
    }

    private IEnumerator LoadSensitivity()
    {
        yield return StartCoroutine(DataSaver.WaitForData());

        camSensitivity = DataSaver.instance.sensitivity;
        Debug.Log(camSensitivity);
    }

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        yRotation += horizontalInput * camSensitivity * sensitivityScale;
        xRotation -= verticalInput * camSensitivity * sensitivityScale;
        xRotation = Mathf.Clamp(xRotation, xRotationMin, xRotationMax);

        transform.eulerAngles = new Vector3(0.0f, yRotation, 0.0f);
        playerCam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}
