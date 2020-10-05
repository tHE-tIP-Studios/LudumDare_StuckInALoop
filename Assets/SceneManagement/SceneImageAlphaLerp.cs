using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SceneImageAlphaLerp : MonoBehaviour
{
    private Image _img;
    private Color _initialColor;
    [SerializeField] private float _fadeIn = 1f;
    [SerializeField] private float _fadeOut = 1f;
    [SerializeField] private float _timeToWaitBetween = 0.2f;

    private WaitForSeconds _wait;
    void Start()
    {
        _img = GetComponent<Image>();
        _img.color = new Color(1, 1, 1, 0);
        _initialColor = _img.color;

        _wait = new WaitForSeconds(0.2f);
        StartCoroutine(LerpColor());
    }

    private IEnumerator LerpColor()
    {
        float t = 0;
        while(t < _fadeIn)
        {
            t += Time.deltaTime / _fadeIn;
            _img.color = Color.Lerp(_img.color, Color.black, t);
            yield return null;
        }
        t = 0;
        
        yield return _wait;

        while (_img.color.a > 0f)
        {
            t += Time.deltaTime / _fadeIn;
            _img.color = Color.Lerp(_img.color, _initialColor, t);
            yield return null;
        }
    }
}
