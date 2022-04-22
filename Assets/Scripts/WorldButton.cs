using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour
{
    public bool isPressed {get; private set; }
    private bool finishedMove;
    private float speed = 0.5f;

    void Awake()
    {
        isPressed = finishedMove = false;
    }

    void FixedUpdate()
    {
        if(isPressed && !finishedMove)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= 0)
                finishedMove = true;
        }

    }

    public void Press()
    {
        isPressed = true;
    }
}
