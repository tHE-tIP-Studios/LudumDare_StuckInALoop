﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _startSmoke;
    [SerializeField] private ParticleSystem _moveParticles;
    [SerializeField] private ParticleSystem _deathParticles;

    [Header("Car explosion variables")]
    [SerializeField] private float _shakeStrength;
    public UnityEvent onCarExplosion;
    public ParticleSystem MoveParticles => _moveParticles;
    private ShakeBehaviour _shaker;
    private MusicManager _music;

    private void Awake() {
        _shaker = FindObjectOfType<ShakeBehaviour>();
        _music = FindObjectOfType<MusicManager>();
        NoiseManager.AddAudioSource(this.gameObject);
    }

    public void StartMovementSmoke()
    {
        _startSmoke.Play();
    }

    public void StartDeath()
    {
        _deathParticles.Play();
        NoiseManager.PlaySound(this.gameObject, "car_fall" + Random.Range(1, 4));
        StartCoroutine(CallAfterTime(.7f, onCarExplosion));
        StartCoroutine(CallAfterTime(.7f, () => 
        {
            _music.Explosion(); 
            NoiseManager.PlaySound(this.gameObject, "car_explosion" + Random.Range(1, 3));
        }));
    }

    private IEnumerator CallAfterTime(float t, UnityEvent functionToCall)
    {
        yield return new WaitForSeconds(t);
        _shaker.InduceStress(_shakeStrength);
        functionToCall?.Invoke();
    }

    private IEnumerator CallAfterTime(float t, System.Action functionToCall)
    {
        yield return new WaitForSeconds(t);
        _shaker.InduceStress(_shakeStrength);
        functionToCall?.Invoke();
    }
}
