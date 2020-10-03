using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private GameObject _carModel;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelFactor;
    [SerializeField] private float _speedFactor;
    private Rigidbody _rb;
    private Vector3 _moveVector;
    private float _acceleration;
    private float _angAccel;
    private float _velocity;
    private float _turnVelocity;

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
        VroomVroom();
        Skrrrrt();
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

        _moveVector = new Vector3(0.0f, _rb.velocity.y * 1.03f, _velocity);
        _moveVector += (Vector3.forward * - StripMovement.StripSpeed);
        _rb.velocity = _moveVector;
    }

    /// <summary>
    /// Car go "SKRRRRT" when turning
    /// </summary>
    private void Skrrrrt()
    {
        if (_rb.velocity.z >= 0.1f)
        {
            _angAccel = Input.GetAxisRaw("Horizontal") * _accelFactor;

            _turnVelocity += ((_speedFactor * 2f) * _angAccel * Time.deltaTime);
            _turnVelocity = Mathf.Clamp(_turnVelocity, -_maxSpeed, _maxSpeed);

            if (_angAccel <= 0.05f)
            {
                if (_turnVelocity >= 0.05f)
                {
                    _turnVelocity -= _speedFactor * 0.5f * Time.deltaTime;
                }
                else if (_turnVelocity <= -0.05f)
                {
                    _turnVelocity += _speedFactor * 0.5f * Time.deltaTime;
                }
            }


            _moveVector.x = _turnVelocity;

            _carModel.transform.localEulerAngles = Quaternion.Slerp(
                Quaternion.Euler(_carModel.transform.localEulerAngles),
                Quaternion.Euler(new Vector3(0.0f, Mathf.Atan2(
                    _turnVelocity, _moveVector.z) * 180 / Mathf.PI, 0.0f)),
                _speedFactor * Time.deltaTime).eulerAngles;

            _rb.velocity = _moveVector;
        }
    }
}
