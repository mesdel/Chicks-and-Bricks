using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chicken : MonoBehaviour
{
    public bool isActive { get; private set; }

    protected void Awake()
    {
        isActive = false;
    }

    public void PickUp()
    {
        isActive = false;
    }

    public void Place()
    {
        Debug.Log("Placing");
        isActive = true;
    }

    abstract public void ChickInteract(GameObject chick);
}
