using UnityEngine;

public class LoadSceneHelper : MonoBehaviour
{
    [SerializeField] private string _sceneName = default;

    public void Load()
    {
        SceneLoader.Load(_sceneName);
    }
}