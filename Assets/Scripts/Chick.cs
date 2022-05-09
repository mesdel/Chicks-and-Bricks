using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chick : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject heartBlastPrefab;
    [SerializeField]
    private AudioClip arriveSound;
    private AudioSource audioSource;

    private bool toDelete;

    Vector3 theVoid = new Vector3(0, -100.0f, 0);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        toDelete = false;
    }

    void FixedUpdate()
    {
        if (!toDelete)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (toDelete && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Chick Collision");
        if(collision.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.ChickArrives();
            audioSource.PlayOneShot(arriveSound);
            Instantiate(heartBlastPrefab, transform.position, transform.rotation);
            // delete after audio is finished playing
            toDelete = true;
            transform.position = theVoid;
        }
        else if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.instance.ChickFails();
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Tutorial"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chick Trigger Enter");
        if(other.gameObject.CompareTag("ChickenTrigger"))
        {
            other.gameObject.GetComponentInParent<Chicken>().ChickInteract(this.gameObject);
        }
    }
}
