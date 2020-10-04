using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComparer : IComparer<PlayerCar>
{
    public int Compare(PlayerCar x, PlayerCar y)
    {
        if (!x.Alive) return -1;
        if (!y.Alive) return 1;

        return (int)(x.transform.position.z - y.transform.position.z);
    }
}
