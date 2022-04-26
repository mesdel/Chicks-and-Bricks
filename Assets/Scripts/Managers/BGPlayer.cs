using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGPlayer : MonoBehaviour
{
    public static BGPlayer instance { get; private set; }

    private AudioSource[] audioSources;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider ambiSlider;

    // todo: remove singleton/dontdestroy status
    // and instead use different music/ambiance from main menu

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        audioSources = GetComponents<AudioSource>();
        Play();
    }

    private void Play()
    {
        // todo: query volume setting
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.Play();
        }
    }

    public void AdjustVolume()
    {
        audioSources[0].volume = musicSlider.value;
        audioSources[1].volume = ambiSlider.value;
    }
}
