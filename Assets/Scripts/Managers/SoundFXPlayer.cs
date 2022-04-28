using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFXPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadVolume());
    }

    private IEnumerator LoadVolume()
    {
        yield return StartCoroutine(DataSaver.WaitForData());
        GetComponent<AudioSource>().volume = DataSaver.instance.sfxVolume;
    }
}
