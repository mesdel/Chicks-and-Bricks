using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chicken : MonoBehaviour
{
    public bool isActive { get; private set; }

    protected void Awake()
    {
        isActive = true;
    }

    public void PickUp()
    {
        isActive = false;
        transform.Find("Full Cell").gameObject.SetActive(false);
    }

    public void Place()
    {
        Debug.Log("Placing Chicken");
        isActive = true;
    }

    abstract public void ChickInteract(GameObject chick);
}
