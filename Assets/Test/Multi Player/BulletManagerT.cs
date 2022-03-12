using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerT : MonoBehaviour
{
    public GameObject env;
    public Color myColor;

    void Start()
    {
        Destroy(gameObject, 3);
    }

    void FixedUpdate()
    {
    }

    void OnCollisionEnter(Collision collision)
    {
        var collider = collision.collider;
        switch (collision.collider.gameObject.tag)
        {
            case "Scene":
                Debug.Log($"OnCollisionEnter Scene With id ={collider.gameObject.GetInstanceID()}");

                Environment
                    .Instance
                    .ChangeColor(myColor,
                    collider.gameObject.GetInstanceID());
                Destroy(this.gameObject);
                break;
            case "Player":
            Debug.Log("OnCollisionEnter +" + collision);
                collider
                    .gameObject
                    .GetComponent<PlayerStatusManger>()
                    .TakeDamage(1);
                Destroy(this.gameObject);
                break;
        }
    }
}
