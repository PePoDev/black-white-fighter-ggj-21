using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class FPSCameraManagerT : MonoBehaviour
{
    Vector2 _mouseLook;

    Vector2 _smoothV;

    public float _sensitivity = 0.02f;

    public float _smoothing = 4.0f;

    GameObject _character;

    void Start()
    {
        _character = this.transform.parent.gameObject;
        if (!transform.parent.gameObject.GetComponent<PhotonView>().IsMine)
            gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 md =
            new Vector2(Input.GetAxisRaw("Mouse X"),
                Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(_sensitivity, _sensitivity));

        _smoothV.x = Mathf.Lerp(_smoothV.x, md.x, 1f / _smoothing);
        _smoothV.y = Mathf.Lerp(_smoothV.y, md.y, 1f / _smoothing);
        _mouseLook += _smoothV;
        _mouseLook.y = Mathf.Clamp(_mouseLook.y, -90f, 90f);

        transform.localRotation =
            Quaternion.AngleAxis(-_mouseLook.y, Vector3.right);
        _character.transform.localRotation =
            Quaternion.AngleAxis(_mouseLook.x, _character.transform.up);
    }
}
