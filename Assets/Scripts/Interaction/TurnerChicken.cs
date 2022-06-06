using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnerChicken : Chicken
{
    [SerializeField]
    private AudioClip turnSound;

    protected new void Awake()
    {
        base.Awake();
        matIndex = 0;
    }

    override public void ChickInteract(GameObject chick)
    {
        if(isActive)
        {
            chick.transform.eulerAngles = this.transform.eulerAngles;
            effects.Play(turnSound);
        }
    }
}
