using Photon.Pun;
using UnityEngine;

public class Environment : MonoBehaviour, IChangeColorable
{
    public static Environment Instance;

    public GameObject[] Walls;

    PhotonView _pv;

    GameObject[] objs;

    void Awake()
    {
        Instance = this;
        objs = GameObject.FindGameObjectsWithTag("Scene");
        _pv = GetComponent<PhotonView>();
    }

    public void ChangeColor(Color color, int id)
    {
        // Debug.Log($"ChangeColor Id={id} Color ={color.ToString()}");
        _pv
            .RPC("RPC_ChangeColor",
            RpcTarget.AllBuffered,
            color.r,
            color.g,
            color.b,
            id);
    }

    [PunRPC]
    public virtual void RPC_ChangeColor(float r, float g, float b, int id)
    {
        Color color = new Color(r, g, b);

        // Debug.Log($"RPC_ChangeColor Id={id} Color ={color.ToString()}");
        foreach (GameObject obj in objs)
        {
            if (obj.GetInstanceID() == id)
            {
                Debug.Log("RPC_ChangeColor " + id);

                if (obj.GetComponent<Renderer>().material.color == Color.white)
                {
                    obj.GetComponent<Renderer>().material.color = Color.black;
                    return;
                }
                obj.GetComponent<Renderer>().material.color = Color.white;
                return;
            }
        }
    }
}
