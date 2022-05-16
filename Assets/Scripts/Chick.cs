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
    private float lifeTime = 5.0f;
    private Vector3 lastPosition;

    private Vector3 theVoid = new Vector3(0, -100.0f, 0);

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        toDelete = false;
        lastPosition = transform.position;
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

        // chick perishes if stuck
        StuckCheck();
    }

    private void ChickPerishes()
    {
        GameManager.instance.ChickFails();
        Destroy(gameObject);
    }

    private void StuckCheck()
    {
        // reduce lifetime left unless the chick is still moving
        float delta = (lastPosition - transform.position).magnitude;
        if(delta < 0.01f)
            lifeTime -= Time.deltaTime;
        lastPosition = transform.position;
        // if stuck for long enough, chick perishes
        if (lifeTime < 0)
        {
            ChickPerishes();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Chick Collision");
        if(collision.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.ChickArrives();
            audioSource.PlayOneShot(arriveSound);
            Instantiate(heartBlastPrefab, transform.position + Vector3.up * 0.3f, transform.rotation);
            // delete after audio is finished playing
            toDelete = true;
            transform.position = theVoid;
        }
        else if (collision.gameObject.CompareTag("Hazard"))
        {
            ChickPerishes();
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

    public void SpeedUp()
    {
        speed *= 4.0f;
    }
}
