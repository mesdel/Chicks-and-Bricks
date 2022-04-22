using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaceSpace : MonoBehaviour
{
    public bool isVacant { get; private set; }

    void Awake()
    {
        isVacant = true;
    }

    public void PickUp()
    {
        isVacant = true;
    }

    public void Place()
    {
        isVacant = false;
    }

    abstract public void ChickInteract(GameObject chick);
}
