using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeIn : MonoBehaviour
{
    [SerializeField] private float _moveValue;
    [SerializeField] private float _timeToMove;
    private RectTransform t;
    private bool _open = false;
    private void Awake() {
        t = GetComponent<RectTransform>();
    }

    public void Open()
    {
        if (_open) return;
        Vector3 move = t.position;
        move.x = 0;
        move.z = 0;
        move.y = 0;
        LeanTween.cancel(gameObject);
        LeanTween.move(t, move, _timeToMove).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() => _open = true);
    }
    
    public void Close()
    {
        if (!_open) return;

        Vector3 move = t.position;
        move.x = -_moveValue;
        move.z = 0;
        move.y = 0;

        LeanTween.cancel(gameObject);
        LeanTween.move(t, move, _timeToMove).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() => _open = false);
    }
}
