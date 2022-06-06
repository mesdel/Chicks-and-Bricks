using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chicken : MonoBehaviour
{
    public bool isActive { get; private set; }
    protected ChickenEffects effects;
    public int matIndex { get; protected set; }

    protected void Awake()
    {
        isActive = true;
        effects = GetComponent<ChickenEffects>();
    }

    public void PickUp()
    {
        isActive = false;
        transform.Find("Full Cell").gameObject.SetActive(false);
        effects.Pickup();
    }

    public void Place()
    {
        Debug.Log("Placing Chicken");
        isActive = true;
        effects.Place();
    }

    abstract public void ChickInteract(GameObject chick);
}
