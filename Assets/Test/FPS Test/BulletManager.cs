using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Scene")
        {
            other.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            Destroy(this.gameObject);

        }
    }
}
