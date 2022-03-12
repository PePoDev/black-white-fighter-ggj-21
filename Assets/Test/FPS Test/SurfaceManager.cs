using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SurfaceManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ammo")
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            Destroy(other.gameObject);

        }
    }
}
