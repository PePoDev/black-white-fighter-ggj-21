using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ShootingManager : MonoBehaviour
{
    public GameObject _bulletEmitter;
    public GameObject _bullet;
    public GameObject _camera;
    public float _bulletForwardForce;

    private float _fireRate = 3;
    private float _currentTime;

    Animator _anim;

    void Start()
    {
        _currentTime = 0;
        _anim = GetComponentInChildren<Animator>();
    }
    
    void Update()
    {

        if (Input.GetAxis("Fire1") > 0 && _currentTime >= 1 / _fireRate)
        {
            _anim.SetTrigger("doShot");
            _anim.SetBool("isShot", true);

            GameObject tempBullet = Instantiate (_bullet, _bulletEmitter.transform.position, _bulletEmitter.transform.rotation) as GameObject;

            tempBullet.transform.Rotate(Vector3.left * 90);

            Rigidbody tempRigidBody = tempBullet.GetComponent<Rigidbody>();

            Vector3 shootingDir = _camera.transform.forward + new Vector3(0, 0.2f, 0);

            tempRigidBody.AddForce(shootingDir * _bulletForwardForce);

            Destroy(tempBullet, 2);

            _currentTime = 0;
        }
        else if (Input.GetAxis("Fire1") == 0)
        {
            _anim.SetBool("isShot", false);
        }

        _currentTime += Time.deltaTime;
    }
}
