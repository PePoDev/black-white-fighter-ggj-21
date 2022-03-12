using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

using Hashtable = ExitGames.Client.Photon.Hashtable;

// TODO ad IDamageable
public class PlayerController : MonoBehaviour
{


    [SerializeField]
    GameObject wall;

    [SerializeField]
    float

            mouseSensitivity,
            sprintSpeed,
            walkSpeed,
            jumpForce,
            smoothTime;

    int itemIndex;

    int previousItemIndex = -1;

    float verticalLookRotation;

    bool grounded;

    Vector3 smoothMoveVelocity;

    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;

    // PlayerManager playerManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        // playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    void Update()
    {
        if (!PV.IsMine) return;

        if (Input.GetMouseButtonDown(0))
        {
            // wall.GetComponent<Wall>().ChangeColor();
        }

        Move();
    }

    void Move()
    {
        Vector3 moveDir =
            new Vector3(Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical")).normalized;
        moveAmount =
            Vector3
                .SmoothDamp(moveAmount,
                moveDir *
                (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed),
                ref smoothMoveVelocity,
                smoothTime);
    }

    void FixedUpdate()
    {
        if (!PV.IsMine) return;

        rb
            .MovePosition(rb.position +
            transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
