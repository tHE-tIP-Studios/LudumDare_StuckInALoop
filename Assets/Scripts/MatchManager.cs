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
    [SerializeField] private AudioClip _matchTrack;

    // Match control variables
    private WaitForSeconds _countDownWait;
    private List<PlayerCar> _looserOrder;
    private PlayerCar[] _cars;
    private PlayerComparer _sorter;
    private Transform[] _startPoints;
    private System.Random _rnd;
    private int _deadCars;

    public int DeadCars => _deadCars;

    // Audio stuff
    private MusicManager _music;

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
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        NoiseManager.AddAudioSource(this.gameObject);

        _cars = FindObjectsOfType<PlayerCar>();
        _music = FindObjectOfType<MusicManager>();
        _startPoints = new Transform[_startPositions.childCount];

        for (int i = 0; i < _startPositions.childCount; i++)
        {
            _startPoints[i] = _startPositions.GetChild(i);
        }

        foreach (PlayerCar car in _cars)
        {
            car.onKill.AddListener(() => AddCarToKilled(car));
        }

        _countDownWait = new WaitForSeconds(1f);
        _sorter = new PlayerComparer();
        _looserOrder = new List<PlayerCar>(_cars.Length);

        // add music stuff
        _music.CurrentMusic = _matchTrack;
        onMatchStart.AddListener(() => _music.Play(1f));
    }

    private void Start()
    {
        InitMatch();
    }

    private void AddCarToKilled(PlayerCar car)
    {
        if (car.ActivePlayer)
        {
            _looserOrder.Add(car);
        }

        _deadCars++;
    }

    private void Update()
    {
        if (_deadCars >= _cars.Length - 1 && Started)
        {
            onMatchEnd?.Invoke();
        }
    }

    private void InitMatch()
    {
        Started = false;
        Cursor.visible = false;

        List<int> availablePositions = new List<int>() { 1, 2, 3, 4 };

        // Set cars in respective position
        for (int i = 0; i < _cars.Length; i++)
        {
            int position = availablePositions[UnityEngine.Random.Range(0, availablePositions.Count)];
            availablePositions.Remove(position);

            _cars[i].transform.position = _startPoints[position - 1].position;
        }
        onMatchInit?.Invoke();
        StartCoroutine(MatchCountdown(_matchStartTime));
    }

    public void ResetMatch()
    {
        _looserOrder.Clear();
        _deadCars = 0;
        
        foreach (PlayerCar car in _cars)
        {
            car.gameObject.SetActive(car.ActivePlayer);
            car.Alive = true;
        }

        InitMatch();
    }

    public void CloseMatch()
    {
        
        Cursor.visible = true;

        foreach (PlayerCar c in _cars)
        {
            if (!_looserOrder.Contains(c))
            {
                _looserOrder.Add(c);
            }
        }

        Started = false;
    }

    private IEnumerator MatchCountdown(int time)
    {
        CountdownTimerUI countdown = FindObjectOfType<CountdownTimerUI>();
        // Wait 1 second before the match actually starts
        yield return _countDownWait;

        NoiseManager.PlaySound(this.gameObject, "321GO");
        for (int i = time; i > 0; i--)
        {
            onMatchStartTimer?.Invoke(i);
            yield return _countDownWait;
        }

        countdown.Disable();
        yield return _countDownWait;

        onMatchStart?.Invoke();
        Started = true;
    }

    public UnityEvent onMatchInit;
    public UnityEvent onMatchStart;
    public UnityEvent onMatchEnd;
    public event Action<int> onMatchStartTimer;
}
