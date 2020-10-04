using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeChildren : MonoBehaviour
{
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        foreach(Transform t in transform)
        {
            Gizmos.DrawSphere(t.position, 0.2f);
        }        
    }
}
