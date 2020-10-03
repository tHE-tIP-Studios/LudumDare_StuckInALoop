using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    private Rigidbody _rb;
    private Vector3 _moveVector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _moveVector = Vector3.zero;
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
        if (Input.GetButton("Accelerator"))
        {
            Accelerate();
        }
        else
        {
            Decelerate();
        }
    }

    /// <summary>
    /// Method to accelerate the car
    /// </summary>
    private void Accelerate()
    {
 
    }

    /// <summary>
    /// Method to decelerate the car
    /// </summary>
    private void Decelerate()
    {

    }
}
