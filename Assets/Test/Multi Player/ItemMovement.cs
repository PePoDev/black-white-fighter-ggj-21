using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    private float translation = 0;
    bool isMovingUpwards = true;
    private float startY;
    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        transform.Rotate(0, 40 * Time.deltaTime, 0);
        translation = isMovingUpwards? translation + Time.deltaTime: translation - Time.deltaTime;
        if(translation > 1)
        {
            translation = 1;
            isMovingUpwards = false;
        }  
        if (translation < 0)
        {
            translation = 0;
            isMovingUpwards = true;
        }
        transform.position = new Vector3(transform.position.x, startY + translation, transform.position.z);
    }
}
