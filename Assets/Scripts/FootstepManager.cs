using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    /* A good portion of the below code is credited to the Unity Creative
       Core Tutorial for Audio management. */

    private AudioSource source;
    [SerializeField]
    private List<AudioClip> grassSteps = new List<AudioClip>();

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayStep()
    {
        AudioClip clip = grassSteps[Random.Range(0, grassSteps.Count)];
        source.PlayOneShot(clip);
    }
}
