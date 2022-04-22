using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    private Animator animator;

    private float turnHeadChance = 0.005f;
    private float eatChance = 0.003f;
    [SerializeField]
    private bool onGrass = false;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Idle", -1, Random.Range(0.0f, 1.0f));
    }

    void FixedUpdate()
    {
        // every frame, there is a small chance to queue a "turn head" animation
        if (Random.Range(0.0f, 1.0f) < turnHeadChance)
            animator.SetTrigger("Turn Head");
        // same with eating animation
        if (onGrass && Random.Range(0.0f, 1.0f) < eatChance)
            animator.SetTrigger("Eat");
    }
}
