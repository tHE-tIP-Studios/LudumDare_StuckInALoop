using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaneKillable
{
    Vector3 Position {get;}
    bool Dead{get;}
    void Kill();
}
