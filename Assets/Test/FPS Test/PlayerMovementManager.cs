using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovementManager : MonoBehaviour
{
    private const float _runningForce = 1f;
    private const float _walkingForce = 0.6f;
    private const float _jumpingForce = 12000;
    private const float _maximumSpeed = 1200;
    private const float _airFriction = 0.6f;

    bool _isGrounded;

    Animator _anim;


    void Awake()
    {
        _isGrounded = CheckIsGrounded();
        _anim = GetComponentInChildren<Animator>();

    }

    void FixedUpdate()
    {

        //handle left/right/up/down/walk/run/jump       
        _anim.SetBool("isWalk", false);
        _anim.SetBool("isRun", false);
        handleMoveMent();

        //check grounded
        _isGrounded = CheckIsGrounded();
        if (_isGrounded)
            _anim.SetBool("isJump", false);
    }

    void handleMoveMent()
    {
        
        Rigidbody rb = GetComponent<Rigidbody>();

        if (_isGrounded)
        {
            if (Input.GetAxis("Jump") > 0)
            {
                //Jump
                rb.AddForce(Vector3.up * _jumpingForce);
                _isGrounded = false;
                _anim.SetBool("isJump", true);
                _anim.SetTrigger("doJump");
            }
        }

        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity.normalized * (_maximumSpeed - rb.velocity.magnitude);

        if (Input.GetAxis("Walking") > 0)
        {
            _anim.SetBool("isWalk", true);
            //walking
            if (!_isGrounded)
                rb.AddForce(targetVelocity * _walkingForce * _airFriction);
            else 
                rb.AddForce(targetVelocity * _walkingForce);
        }
        else if (targetVelocity != Vector3.zero)
        {
            _anim.SetBool("isRun", true);
            //running
            if (!_isGrounded)
                rb.AddForce(targetVelocity * _runningForce * _airFriction);
            else 
                rb.AddForce(targetVelocity * _runningForce);
        }
        rb.AddForce(Vector3.down * 981f);
    }

    bool CheckIsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, transform.localScale.y / 2 + 0.1f);
    }
}
