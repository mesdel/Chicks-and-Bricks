using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerChicken : Chicken
{
    [SerializeField]
    private AudioClip speedUpSound;

    protected new void Awake()
    {
        base.Awake();
        matIndex = 1;
    }

    override public void ChickInteract(GameObject chick)
    {
        if (isActive)
        {
            chick.GetComponent<Chick>().SpeedUp();
            effects.Play(speedUpSound);
        }
    }
}
