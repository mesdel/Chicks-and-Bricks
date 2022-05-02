using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Material defaultMat;
    [SerializeField]
    private Material pickupMat;

    public bool mousedOver { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // todo: set up toon chicken's renderer to also have a semi-transparent pickup material
    public void PickUp()
    {
        gameObject.GetComponent<MeshRenderer>().material = pickupMat;
    }

    public void Place()
    {
        gameObject.GetComponent<MeshRenderer>().material = defaultMat;
    }
}
