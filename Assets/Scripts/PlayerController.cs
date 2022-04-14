using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxVelocity;
    [SerializeField]
    private float airDrag;
    private Rigidbody playerRb;

    [SerializeField]
    private float jumpForce;
    private bool isGrounded;

    void Awake()
    {
        // move below line to more applicable script
        Application.targetFrameRate = 60;

        playerRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        HandleKeyMovement();
        HandleJump();
    }

    private void HandleKeyMovement()
    {
        float forceScalar = isGrounded ? 1.0f : (1.0f - airDrag);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 toMove = new Vector3(horizontalInput, 0, verticalInput);
        toMove = toMove.normalized * speed * forceScalar;

        playerRb.AddRelativeForce(toMove, ForceMode.Force);
    }

    private void HandleJump()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
