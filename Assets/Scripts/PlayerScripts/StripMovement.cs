using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StripMovement : MonoBehaviour
{
    public static float StripSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    private Material _stripMat;
    private Vector2 _trackOffset;
    private PlayerCar[] _carts;
    private Transform _backEnd, _frontEnd;
    private float _stripLength;
    private float t;

    private void Awake()
    {
        StripSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, 0.5f);
        _stripMat = transform.Find("Mesh").GetComponentInChildren<MeshRenderer>().material;

        if(!_stripMat) Debug.LogError("Material not found");

        _backEnd = transform.Find("StripBackEnd");
        _frontEnd = transform.Find("StripFrontEnd");
        _stripLength = Vector3.Distance(_backEnd.position, _frontEnd.position);
    }

    private void Start() 
    {
        _carts = FindObjectsOfType<PlayerCar>();
        if (_carts.Length <= 0) Debug.LogError("No object in scene is of type PlayerCar");
    }

    private void Update()
    {
        if (!MatchManager.Started)
        {
            StripSpeed = 0.0f;
            return;
        }
        
        _trackOffset.y += Time.deltaTime * StripSpeed;
        _stripMat.SetTextureOffset("_MainTex", -_trackOffset * .2f);

        CalculateStripSpeed();
    }

    private void CalculateStripSpeed()
    {
        float velocity = 0f;
        Transform front = GetFarthestFront();
        Transform back = GetFarthestBack();

        // quanto mais perto o ultimo lugar esta da parte de tras mais lento a cassete roda,
        // quanto mais perto o primeiro lugar esta da parte da frente mais rapido.

        if (!front && !back)
        {
            t = Mathf.Lerp(t, 1f, Time.deltaTime);
        }
        else
        {
            t = Vector3.Distance(front.position, back.position) / _stripLength;
        }

        StripSpeed = Mathf.Lerp(_minSpeed, _maxSpeed, 1 - t);
    }

    private Transform GetFarthestFront()
    {
        float farthest = float.NegativeInfinity;
        int cart = -1;

        for (int i = 0; i < _carts.Length; i++)
        {
            if (!_carts[i].Alive) continue;
            float z = _carts[i].transform.position.z;

            if (z > farthest)
            {
                farthest = z;
                cart = i;
            }
        }

        if (cart == -1) return null;
        return _carts[cart].transform;
    }

    private Transform GetFarthestBack()
    {
        float farthest = float.PositiveInfinity;
        int cart = -1;

        for (int i = 0; i < _carts.Length; i++)
        {
            if (!_carts[i].Alive) continue;
            float z = _carts[i].transform.position.z;

            if (z < farthest)
            {
                farthest = z;
                cart = i;
            }
        }

        if (cart == -1) return null;
        return _carts[cart].transform;
    }
}
