using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    void Awake()
    {
        
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ChickenInteract();
        }
    }

    private void ChickenInteract()
    {

    }
}
