using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenHelper : MonoBehaviour
{
    private void Awake() {
        Screen.fullScreen = true;
    }
    public void SetFullScreen(bool value)
    {
        Screen.fullScreen = value;
        Debug.Log("Changed Screen");
    }
}
