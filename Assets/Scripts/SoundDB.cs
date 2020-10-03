using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Sound Database", menuName = "ScriptableObjects/SoundDB", order = 1)]
public class SoundDB : ScriptableObject
{
    [SerializeField] private AudioClip[] _audioClips = default;

    public AudioClip this[int i] => _audioClips[i];

#if UNITY_EDITOR
    private void OnValidate()
    {
        LoadClips();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadClips;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged -= LoadClips;
    }

    private void LoadClips()
    {
        _audioClips = Resources.LoadAll<AudioClip>("Audio/");
    }

#endif
    public AudioClip GetClip(int index) => _audioClips[index];
    public int GetLength() => _audioClips.Length;
}
