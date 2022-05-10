using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenEffects : MonoBehaviour
{
    private Animator animator;

    private float turnHeadChance = 0.005f;
    private float eatChance = 0.003f;
    [SerializeField]
    private bool onGrass = false;

    [SerializeField]
    private GameObject smokePrefab;

    // todo: refactor soundfxvolumer to soundfxplayer
    private AudioSource soundEffectPlayer;
    [SerializeField]
    private AudioClip placeSound;
    [SerializeField]
    private AudioClip pickUpSound;
    [SerializeField]
    private AudioClip[] bockSounds;
    private float bockChance = 0.005f;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        animator.Play("Idle", -1, Random.Range(0.0f, 1.0f));
        soundEffectPlayer = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // every frame, there is a small chance to queue a "turn head" animation
        if (Random.Range(0.0f, 1.0f) < turnHeadChance)
            animator.SetTrigger("Turn Head");
        // same with eating animation
        if (onGrass && Random.Range(0.0f, 1.0f) < eatChance)
            animator.SetTrigger("Eat");
        // and with making random noises
        if (Random.Range(0.0f, 1.0f) < bockChance)
            PlayBock();
    }

    public void Play(AudioClip clip)
    {
        soundEffectPlayer.PlayOneShot(clip);
    }

    public void Pickup()
    {
        onGrass = false;
        soundEffectPlayer.PlayOneShot(pickUpSound);
    }

    public void Place()
    {
        onGrass = true;
        Instantiate(smokePrefab, transform.position, transform.rotation);
        soundEffectPlayer.PlayOneShot(placeSound);
    }

    private void PlayBock()
    {
        int bockIndex = Random.Range(0, bockSounds.Length);
        soundEffectPlayer.PlayOneShot(bockSounds[bockIndex]);
    }
}
