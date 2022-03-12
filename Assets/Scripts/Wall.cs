using Photon.Pun;
using UnityEngine;

public class Wall : MonoBehaviour, IChangeColorable
{
    PhotonView PV;
   //TODO  replace with collision
    void Awake()
   {
       	PV = GetComponent<PhotonView>();
   }
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     this.ChangeColor();
        // }
    }

    public void ChangeColor()
    {
        Debug.Log(Color.black);
        // Colorcolors = new Color[] {Color.black};
        PV.RPC("RPC_ChangeColor", RpcTarget.AllBuffered ,Color.black.r,Color.black.g,Color.black.b);
    }

    [PunRPC]
    public virtual void RPC_ChangeColor(float r,float g  ,float b)
    { 
        Color color =new Color (r,g,b);
        // Debug.Log("RPC_ChangeColor "+color);
        //  if (!PV.IsMine) return;
        this.gameObject.GetComponent<Renderer>().material.color = color;
    }
}
