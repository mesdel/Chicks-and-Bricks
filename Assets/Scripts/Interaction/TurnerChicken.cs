using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnerChicken : Chicken
{
    [SerializeField]
    AudioClip turnSound;
    override public void ChickInteract(GameObject chick)
    {
        if(isActive)
        {
            chick.transform.eulerAngles = this.transform.eulerAngles;
            effects.Play(turnSound);
        }
    }
}
