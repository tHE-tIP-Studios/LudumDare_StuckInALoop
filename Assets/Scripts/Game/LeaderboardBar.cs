using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeaderboardBar : MonoBehaviour
{
    [SerializeField] private Image _icon = default;
    [SerializeField] private GameObject _deadOverlay = default;
    private Image _bar;
    private TextMeshProUGUI _barText;

    private void Awake()
    {
        _bar = GetComponent<Image>();
        _barText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetBar(PlayerCar car, int position)
    {
        _icon.sprite = car.Icon;
        _bar.color = car.MainColor;
        _barText.text = $"{position}# place";
        _deadOverlay.SetActive(car.Dead);
    }
}
