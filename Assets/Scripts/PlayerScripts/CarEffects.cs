using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _startSmoke;

    public void StartMovementSmoke()
    {
        _startSmoke.Play();
    }
}
