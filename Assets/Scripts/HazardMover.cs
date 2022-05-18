using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMover : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifetime;

    void Start()
    {
        InvokeRepeating("Despawn", 5.0f, 1.0f);
    }
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Game Border"))
        {
            Despawn();
        }
    }
}
