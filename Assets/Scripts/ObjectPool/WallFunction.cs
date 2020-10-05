using UnityEngine;

public class WallFunction : MonoBehaviour
{
    [SerializeField] private float _timeToLive;
    private float _timer;
    private WallPool _poll;

    public WallPool Poll => _poll;

    private void Awake()
    {
        _timer = _timeToLive;
        _poll = FindObjectOfType<WallPool>();
    }

    // Update is called once per frame
    private void Update()
    {
        _timer -= Time.deltaTime;

        transform.Translate(
            Vector3.forward * -StripMovement.StripSpeed * Time.deltaTime);

        if (_timer <= 0f)
        {
            _poll.ReturnToPool(this);
            _timer = _timeToLive;
        }
    }
}
