using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlaneKillable
{
    Vector3 Position {get;}
    void Kill();
}
