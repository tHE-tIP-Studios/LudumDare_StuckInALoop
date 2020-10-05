using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCar : MonoBehaviour, IPlaneKillable
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private Color _mainColor;
    public Vector3 Position => transform.position;
    public bool Alive {get; set;}
    public bool ActivePlayer {get; set;}
    public bool Dead => !Alive;
    public Sprite Icon => _icon;
    public Color MainColor => _mainColor;

    private void Awake() {
        ActivePlayer = true;
    }

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
        StartCoroutine(HelpRoutines.CallAfterTime(1f, ()=> gameObject.SetActive(false)));
        Alive = false;
    }

    public UnityEvent onKill;
}
