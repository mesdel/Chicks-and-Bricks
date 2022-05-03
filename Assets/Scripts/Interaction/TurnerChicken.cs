using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoostSpace : Chicken
{
    [SerializeField]
    private bool preoccupied = false;

    private new void Awake()
    {
        base.Awake();
        if (preoccupied)
            Place();
    }

    override public void ChickInteract(GameObject chick)
    {
        if(isActive)
        {
            chick.transform.eulerAngles = this.transform.eulerAngles;
        }
    }
}
