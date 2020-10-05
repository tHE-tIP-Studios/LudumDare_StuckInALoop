using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    [SerializeField] private string _sceneName = default;

    public void Load()
    {
        SceneManager.LoadScene(_sceneName);
    }
}