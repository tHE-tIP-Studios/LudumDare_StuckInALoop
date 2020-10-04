using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCar : MonoBehaviour, IPlaneKillable
{
    public Vector3 Position => transform.position;
    public bool Alive {get; private set;}
    public bool Dead => !Alive;

    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Kill()
    {
        onKill?.Invoke();
        StartCoroutine(HelpRoutines.CallAfterTime(6f, ()=> gameObject.SetActive(false)));
        Alive = false;
    }

    public UnityEvent onKill;
}
