using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperChicken : Chicken
{
    [SerializeField]
    private AudioClip jumpSound;
    private float jumpForce = 20.0f;

    protected new void Awake()
    {
        base.Awake();
        matIndex = 2;
    }

    override public void ChickInteract(GameObject chick)
    {
        if(isActive)
        {
            // make chick jump
            chick.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            effects.Play(jumpSound);
        }
    }
}
