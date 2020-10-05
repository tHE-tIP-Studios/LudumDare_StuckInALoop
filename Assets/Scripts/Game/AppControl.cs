
using UnityEngine;
using UnityEngine.Events;

public class AppControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPressStart?.Invoke();
        }
    }

    public UnityEvent OnPressStart;
}
