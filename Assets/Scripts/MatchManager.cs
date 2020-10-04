using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MatchManager : MonoBehaviour
{
    public static bool Started;
    [SerializeField] private int _matchStartTime;
    [SerializeField] private Transform _startPositions;
    private WaitForSeconds _countDownWait;
    private List<PlayerCar> _looserOrder;
    private PlayerCar[] _cars;
    private PlayerComparer _sorter;
    private Transform[] _startPoints;
    private System.Random _rnd;

    public List<PlayerCar> SortedCarsLoosersToWinners 
    {
        get
        {
            _looserOrder.Sort(_sorter);
            return _looserOrder;
        }
    }

    private void Awake() 
    {
        _cars = FindObjectsOfType<PlayerCar>();
        _startPoints = new Transform[_startPositions.childCount];

        for (int i = 0; i < _startPositions.childCount; i++)
        {
            _startPoints[i] = _startPositions.GetChild(i); 
        }
        
        foreach(PlayerCar car in _cars)
        {
            car.onKill.AddListener(() => _looserOrder.Add(car));
        }

        _countDownWait = new WaitForSeconds(1);
        _sorter = new PlayerComparer();
        _looserOrder = new List<PlayerCar>(_cars.Length);
    }

    private void Start()
    {
        InitMatch();
    }

    private void AddCarToKilled()
    {

    }

    private void InitMatch()
    {
        Started = false;

        List<int> availablePositions = new List<int>(){1, 2, 3, 4};

        // Set cars in respective position
        for (int i = 0; i < _cars.Length; i++)
        {
            int position = availablePositions[ UnityEngine.Random.Range(0, availablePositions.Count)];
            availablePositions.Remove(position);
            Debug.Log(position);

            _cars[i].transform.position = _startPoints[position - 1].position;
        }
        
        StartCoroutine(MatchCountdown(_matchStartTime));
    }

    public void ResetMatch()
    {
        _looserOrder.Clear();
        foreach(PlayerCar car in _cars)
        {
            car.gameObject.SetActive(true);
        }

        InitMatch();
    }

    private IEnumerator MatchCountdown(int time)
    {
        // Wait 1 second before the match actually starts
        yield return _countDownWait;

        for (int i = time; i > 0; i--)
        {
            onMatchStartTimer?.Invoke(i);
            yield return _countDownWait;
        }

        onMatchStart?.Invoke();
        Started = true;
    }

    public UnityEvent onMatchStart;
    public UnityEvent onMatchEnd;
    public event Action<int> onMatchStartTimer;
}
