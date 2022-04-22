using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoostSpace : PlaceSpace
{
    override public void ChickInteract(GameObject chick)
    {
        Debug.Log("yee");
        if(!isVacant)
        {
            chick.transform.eulerAngles = this.transform.eulerAngles;
        }
    }
}
