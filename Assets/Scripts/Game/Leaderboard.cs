using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Transform _menu;
    [SerializeField] private Transform _barHolder;
    [SerializeField] private ComeIn _leaderboardAnim;
    [SerializeField] private ComeIn _backgroundAnim;
    private LeaderboardBar[] _bars;
    private MatchManager _match;
    private MusicManager _music;

    private void Awake() 
    {
        _match = FindObjectOfType<MatchManager>();
        _bars =_barHolder.GetComponentsInChildren<LeaderboardBar>();
        _music = FindObjectOfType<MusicManager>();
    }

    public void OpenMenu()
    {
        _music.Leaderboard();
        
        StartCoroutine(HelpRoutines.CallAfterTime(1f, () => 
            {
                _menu.gameObject.SetActive(true);
                SetLeaderboard();
            }));
    }

    public void CloseMenu()
    {
        _music.InGame();
        _leaderboardAnim.Close();
        _backgroundAnim.Close();
        StartCoroutine(HelpRoutines.CallAfterTime(1f, () => 
        {
            _menu.gameObject.SetActive(false);
        }));
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
        // Open Background
        _leaderboardAnim.Open();
        // Open tabs
        _backgroundAnim.Open();
    }
}
