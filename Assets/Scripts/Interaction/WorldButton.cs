using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour
{
    public bool isPressed {get; private set; }
    private float speed = 0.5f;

    private AudioSource soundEffectPlayer;
    [SerializeField]
    private AudioClip pressSound;
    [SerializeField]
    private GameObject fireworkPrefab;

    void Awake()
    {
        isPressed = false;
        soundEffectPlayer = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(isPressed)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= 0)
                Destroy(gameObject);
        }
    }

    public void Press()
    {
        isPressed = true;
        transform.Find("Button Prompt").gameObject.SetActive(false);
        soundEffectPlayer.PlayOneShot(pressSound);
        Instantiate(fireworkPrefab, transform.position, transform.rotation);
    }
}
