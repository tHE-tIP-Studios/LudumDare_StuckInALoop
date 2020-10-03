using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private int _matchStartTime;
    private WaitForSeconds _countDownWait;

    public static bool Started;

    private void Awake() 
    {
        _countDownWait = new WaitForSeconds(1);
        Started = false;
    }

    private void Start() 
    {
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
