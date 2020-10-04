using System.Collections;
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

    private void Awake() {
        _shaker = FindObjectOfType<ShakeBehaviour>();
    }

    public void StartMovementSmoke()
    {
        _startSmoke.Play();
    }

    public void StartDeath()
    {
        _deathParticles.Play();
        StartCoroutine(CallAfterTime(.7f, onCarExplosion));
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
