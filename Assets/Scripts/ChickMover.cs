using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickMover : MonoBehaviour
{
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Chick Collision");
        if(collision.gameObject.CompareTag("Goal"))
        {
            // TODO: add score

            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Hazard"))
        {
            // TODO: subtract score

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
