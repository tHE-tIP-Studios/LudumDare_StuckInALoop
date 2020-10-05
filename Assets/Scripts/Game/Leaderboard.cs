using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Transform _barHolder;
    private LeaderboardBar[] _bars;
    private MatchManager _match;

    private void Awake() 
    {
        _match = FindObjectOfType<MatchManager>();
        _bars =_barHolder.GetComponentsInChildren<LeaderboardBar>();
    }

    public void OpenMenu()
    {
        SetLeaderboard();
    }

    public void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void SetLeaderboard()
    {
        List<PlayerCar> order =  _match.SortedCarsLoosersToWinners;
        for (int i = 0; i < order.Count ; i++)
        {
            _bars[_bars.Length - (i + 1)].SetBar(order[i], _bars.Length - i);
            print($"Is {order[i].name} active: {order[i].ActivePlayer}");

            if (!order[i].ActivePlayer)
            {
                _bars[_bars.Length - (i + 1)].gameObject.SetActive(false);
            }
        }
    }
}
