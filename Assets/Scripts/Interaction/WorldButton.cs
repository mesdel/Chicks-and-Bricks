using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldButton : MonoBehaviour
{
    public bool isPressed {get; private set; }
    private float speed = 0.5f;

    void Awake()
    {
        isPressed = false;
    }

    void FixedUpdate()
    {
        if(isPressed)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            if (transform.position.y <= 0)
                Destroy(gameObject);
        }
    }

    public void Press()
    {
        isPressed = true;
        transform.Find("Button Prompt").gameObject.SetActive(false);
    }
}
