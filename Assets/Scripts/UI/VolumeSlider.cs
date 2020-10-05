using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer = default;
    [SerializeField] private string _propertyName;

    public void SetLevel(float value)
    {
        _mixer.SetFloat(_propertyName, Mathf.Log10(value) * 20);
    }
}
