using UnityEngine;

public class RampFunction : MonoBehaviour
{
    [SerializeField] private float _timeToLive;
    private float _timer;
    private RampPool _pool;

    public RampPool Pool => _pool;

    private void Awake() 
    {
        _timer = _timeToLive;
        _pool = FindObjectOfType<RampPool>();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        _timer -= Time.deltaTime;

        transform.Translate(
            Vector3.forward * -StripMovement.StripSpeed * Time.deltaTime);

        if (_timer <= 0f)
        {
            _pool.ReturnToPool(this);
            _timer = _timeToLive;
        }
    }
}
