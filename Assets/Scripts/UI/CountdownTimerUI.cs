using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimerUI : MonoBehaviour
{
    private MatchManager _matchManager;
    private TextMeshProUGUI _uiElement;
    private Image _countdownCircle;
    private Transform _goObj;
    private Coroutine _fillRoutine;

    private float _currentCircleValue;

    private void Awake() {
        _matchManager = FindObjectOfType<MatchManager>();
        _countdownCircle = GetComponent<Image>();
        _uiElement = GetComponentInChildren<TextMeshProUGUI>();
        _currentCircleValue = _countdownCircle.fillAmount;
        _goObj = transform.Find("Go");
        _goObj.gameObject.SetActive(false);
        _goObj.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }

    private void OnEnable() {
        _matchManager.onMatchStartTimer += SetTime;
    }

    private void OnDisable() {
        _matchManager.onMatchStartTimer -= SetTime;
    }

    private void SetTime(int i)
    {
        _uiElement.text = i.ToString();
        
        if (_fillRoutine == null)
        {
            _fillRoutine = StartCoroutine(Circle());
        }
    }
    
    private IEnumerator Circle()
    {
        float t = 0f;
        float endValue = _currentCircleValue <= 0.1f ? 1 : 0;

        while(t < 1f)
        {
            t += Time.deltaTime / 1f;

            _countdownCircle.fillAmount = Mathf.Lerp(_currentCircleValue, endValue, t);
            yield return null;
        }

        _currentCircleValue = _countdownCircle.fillAmount;
        _countdownCircle.fillClockwise = !_countdownCircle.fillClockwise;
        _fillRoutine = null;
    }

    public void Disable()
    {
        _goObj.gameObject.SetActive(true);
        _uiElement.gameObject.SetActive(false);
        _goObj.LeanScale(Vector3.one, .1f).setOnComplete(DisableAfterTime);
    }

    public void DisableAfterTime()
    {
        IEnumerator afterTime(float t)
        {
            yield return new WaitForSeconds(t);
            gameObject.SetActive(false);
        }

        StartCoroutine(afterTime(.5f));
    }
}
