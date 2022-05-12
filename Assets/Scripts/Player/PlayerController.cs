using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxVelocity;
    private Rigidbody playerRb;

    private Animator animator;

    private Vector3 spawnPosition = new Vector3(-3.5f, 0.1f, -8.3f);

    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        HandleKeyMovement();
    }

    private void HandleKeyMovement()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 toMove = new Vector3(horizontalInput, 0, verticalInput);
        toMove = toMove.normalized * speed;

        playerRb.AddRelativeForce(toMove, ForceMode.Force);

        animator.SetFloat("verticalInput", verticalInput);
        animator.SetFloat("horizontalInput", horizontalInput);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn"))
            transform.position = spawnPosition;
    }
}
