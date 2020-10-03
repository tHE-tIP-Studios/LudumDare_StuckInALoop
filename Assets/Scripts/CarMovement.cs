﻿using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private GameObject _carModel;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelFactor;
    [SerializeField] private float _speedFactor;
    [SerializeField] private float _turnFactor;
    private Rigidbody _rb;
    private Vector3 _moveVector;
    private float _acceleration;
    private float _angAccel;
    private float _velocity;

    public bool IsGrounded
    {
        get
        {
            Collider[] cols = Physics.OverlapSphere(
                new Vector3(transform.position.x,
                transform.position.y - 0.5f, transform.position.z),
                0.2f, LayerMask.GetMask("Track"));
            return cols != null;
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        NoiseManager.AddAudioSource(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    /// <summary>
    /// Method that is comprised of the acceleration methods
    /// </summary>
    private void Movement()
    {
        if (IsGrounded)
        {
            VroomVroom();
            Skrrrrt();
        }
        else
        {
            _moveVector = transform.forward * _velocity;
            _moveVector.y = _rb.velocity.y * 1.03f;
            _moveVector += (Vector3.forward * -StripMovement.StripSpeed);
            _rb.velocity = _moveVector;
        }
    }


    /// <summary>
    /// Car does "vroom vroom" when moving
    /// </summary>
    private void VroomVroom()
    {
        _acceleration = Input.GetAxisRaw("Accelerator") * _accelFactor;

        _velocity += (_speedFactor * _acceleration * Time.deltaTime);
        _velocity = _velocity >= _maxSpeed ? _maxSpeed : _velocity;

        if (_acceleration <= 0.05f && _velocity >= 0.05f)
        {
            _velocity -= _speedFactor * 0.65f * Time.deltaTime;
        }

        _moveVector = transform.forward * _velocity;
        _moveVector.y = _rb.velocity.y * 1.03f;
        _moveVector += (Vector3.forward * -StripMovement.StripSpeed);
        _rb.velocity = _moveVector;
    }

    /// <summary>
    /// Car go "SKRRRRT" when turning
    /// </summary>
    private void Skrrrrt()
    {
        _angAccel = Input.GetAxisRaw("Horizontal");

        if (_angAccel < 0)
        {

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(new Vector3(0.0f, -45f, 0.0f)),
                _turnFactor * Time.deltaTime);
        }
        else if (_angAccel > 0)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(new Vector3(0.0f, 45f, 0.0f)),
                _turnFactor * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)),
                _turnFactor * Time.deltaTime);
        }
    }
}
