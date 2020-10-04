using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class HelpRoutines
{
    public static IEnumerator CallAfterTime(float t, UnityEvent functionToCall)
    {
        yield return new WaitForSeconds(t);
        functionToCall?.Invoke();
    }

    public static IEnumerator CallAfterTime(float t, System.Action functionToCall)
    {
        yield return new WaitForSeconds(t);
        functionToCall?.Invoke();
    }
}
