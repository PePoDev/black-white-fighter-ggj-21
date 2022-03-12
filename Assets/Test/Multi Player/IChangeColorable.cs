using Photon.Pun;
using UnityEngine;

public interface IChangeColorable 
{
    public void ChangeColor(Color color,int id){}

    [PunRPC]
    public  void RPC_ChangeColor(float r,float g  ,float b){}
}
