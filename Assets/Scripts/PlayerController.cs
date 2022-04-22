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

    private Animator animator;

    private Vector3 spawnPosition = new Vector3(-3.5f, 0.1f, -8.3f);

    void Awake()
    {
        // move below line to more applicable script
        Application.targetFrameRate = 60;

        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        isGrounded = true;
    }

    void FixedUpdate()
    {
        HandleKeyMovement();
        HandleJump();
        UpdateAnimator();
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

    private void UpdateAnimator()
    {
        
        animator.SetFloat("velocity", playerRb.velocity.magnitude);
        animator.SetBool("isGrounded", isGrounded);
    }

    private void HandleJump()
    {
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("jump");
            isGrounded = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
            transform.position = spawnPosition;
    }
}
