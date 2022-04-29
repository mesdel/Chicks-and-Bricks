using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceSpace : MonoBehaviour
{
    public bool isVacant { get; private set; }

    protected void Awake()
    {
        isVacant = true;
    }

    public void PickUp()
    {
        isVacant = true;
    }

    public void Place()
    {
        Debug.Log("Placing");
        isVacant = false;
    }

    abstract public void ChickInteract(GameObject chick);
}
