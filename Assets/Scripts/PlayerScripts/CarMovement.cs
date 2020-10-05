using Rewired;
using UnityEngine;
using UnityEngine.Events;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _accelFactor;
    [SerializeField] private float _speedFactor;
    [SerializeField] private float _turnFactor;
    [SerializeField] private int _playerNum = default;
    private Rigidbody _rb;
    private PlayerCar _playerCar;
    private Vector3 _moveVector;
    private float _acceleration;
    private float _angAccel;
    private float _velocity;
    private float _lastInput;
    public Player PlayerInputs { get; private set; }

    // VFX
    private CarEffects _carEffects;

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
        _carEffects = GetComponent<CarEffects>();
        _playerCar = GetComponent<PlayerCar>();
        NoiseManager.AddAudioSource(this.gameObject);
        PlayerInputs = ReInput.players.GetPlayer(_playerNum);
    }
    private void Start()
    {
        if (PlayerInputs.controllers.joystickCount == 0 && _playerNum > 1)
        {
            Debug.Log("Player " + _playerNum + " doesn't have a connected controller.");
            _playerCar.ActivePlayer = false;
            _playerCar.Kill();
            gameObject.SetActive(false);
        }
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
        float input = PlayerInputs.GetAxisRaw("Button0");
        if (input > 0.01f && _lastInput <= 0.01f)
        {
            _onMovementStart?.Invoke();
        }

        if (input > 0.1f && _carEffects.MoveParticles.isStopped && IsGrounded)
        {
            _carEffects.MoveParticles.Play();
        }

        if (input < 0.1f && !_carEffects.MoveParticles.isStopped)
        {
            _carEffects.MoveParticles.Stop();
        }

        if (!IsGrounded && !_carEffects.MoveParticles.isStopped)
        {
            print("Stop particles");
            _carEffects.MoveParticles.Stop();
        }

        if (!MatchManager.Started) return;

        if (IsGrounded)
        {
            VroomVroom();
            Skrrrrt();

            _lastInput = input;
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
        _acceleration = PlayerInputs.GetAxisRaw("Button0") * _accelFactor;

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
        _angAccel = PlayerInputs.GetAxisRaw("HorizontalAxisL");

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

    public UnityEvent _onMovementStart;
}