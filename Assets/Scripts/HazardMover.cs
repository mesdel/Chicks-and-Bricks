using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMover : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifetime;

    // Start is called before the first frame update
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
}
