using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MatchManager : MonoBehaviour
{
    public static bool Started;
    [SerializeField] private int _matchStartTime;
    private WaitForSeconds _countDownWait;
    private List<PlayerCar> _looserOrder;
    private PlayerCar[] _cars;
    private PlayerComparer _sorter;

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
        _countDownWait = new WaitForSeconds(1);
        Started = false;
        _cars = FindObjectsOfType<PlayerCar>();

        _sorter = new PlayerComparer();
        _looserOrder = new List<PlayerCar>();
    }

    private void Start()
    {
        foreach(PlayerCar car in _cars)
        {
            car.onKill.AddListener(() => _looserOrder.Add(car));
        }

        StartCoroutine(MatchCountdown(_matchStartTime));
    }

    private IEnumerator MatchCountdown(int time)
    {
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
