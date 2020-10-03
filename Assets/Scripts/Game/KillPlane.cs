using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KillPlane : MonoBehaviour
{
    [SerializeField] private bool _visualizePlane;
    private List<IPlaneKillable> _killables;

    private void Start() 
    {
        _killables = new List<IPlaneKillable>();

        var group = FindObjectsOfType<MonoBehaviour>().OfType<IPlaneKillable>();
        _killables.AddRange(group);

        Debug.Log(_killables.Count);  
    }

    private void FixedUpdate() 
    {
        if (_killables.Count > 0)
        {
            for (int i = 0; i < _killables.Count; i++)
            {
                float d = _killables[i].Position.y - transform.position.y;

                if (d < 0)
                {
                    _killables[i].Kill();
                }
            }
        }
    }
    
    private void OnDrawGizmosSelected() 
    {
        if (!_visualizePlane) return;
        Color c = Color.red;
        c.a = 0.2f;
        Gizmos.color = c;
        Gizmos.DrawCube(transform.position, new Vector3(100f, 0.1f, 100f));    
    }
}
