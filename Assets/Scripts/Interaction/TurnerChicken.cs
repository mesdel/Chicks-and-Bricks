using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnerChicken : Chicken
{
    override public void ChickInteract(GameObject chick)
    {
        if(isActive)
        {
            chick.transform.eulerAngles = this.transform.eulerAngles;
        }
    }
}
