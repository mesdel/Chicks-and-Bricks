using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXVolumer : MonoBehaviour
{
    private float currVolume;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadVolume());
    }

    private void Update()
    {
        if(currVolume != DataSaver.instance.sfxVolume)
            StartCoroutine(LoadVolume());
    }

    private IEnumerator LoadVolume()
    {
        yield return StartCoroutine(DataSaver.WaitForData());
        AudioSource[] sfx = GetComponents<AudioSource>();
        currVolume = DataSaver.instance.sfxVolume;
        foreach (AudioSource soundEffect in sfx)
        {
            soundEffect.volume = currVolume;
        }
    }
}
