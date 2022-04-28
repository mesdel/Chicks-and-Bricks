using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMover : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Chick Collision");
        if(collision.gameObject.CompareTag("Goal"))
        {
            GameManager.instance.ChickArrives();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.instance.ChickFails();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Chick Trigger Enter");
        if(other.gameObject.CompareTag("PlaceSpace"))
        {
            other.gameObject.GetComponentInParent<PlaceSpace>().ChickInteract(this.gameObject);
        }
    }
}
