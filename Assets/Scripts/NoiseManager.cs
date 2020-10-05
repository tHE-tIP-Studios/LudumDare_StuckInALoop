using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

/// <summary>
/// Class that manages the noise
/// </summary>
public class NoiseManager : MonoBehaviour
{
    //* Class variables
    private static NoiseManager _instance;
    private static AudioMixerGroup _mixerGroup;
    [SerializeField] private SoundDB _soundDB = default;
    [SerializeField] private AudioMixerGroup _sfxMixer = default;
    private static Dictionary<string, AudioClip> _audioDictionary = default;

    public NoiseManager Instance => _instance;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        DontDestroyOnLoad(_instance);
        CreateSoundCollection();
        _mixerGroup = _sfxMixer;
    }

    /// <summary>
    /// Method to create a dictionary of audio clips
    /// </summary>
    private void CreateSoundCollection()
    {
        _audioDictionary = new Dictionary<string, AudioClip>();

        for (int i = 0; i < _soundDB.GetLength(); i++)
        {
            _audioDictionary.Add(
                _soundDB.GetClip(i).name, _soundDB.GetClip(i));
        }

    }

    /// <summary>
    /// Method to add a Audio Source to a game object.
    /// </summary>
    /// <param name="source">
    /// Object to add audio source to.
    /// </param>
    public static void AddAudioSource(GameObject source)
    {
        //! IF NEEDED ADD RECURSIVE SOLUTION TO CHECK CHILDREN FOR AUDIO SOURCES

        if (!source.TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            source.AddComponent<AudioSource>().playOnAwake = false;
            ChangeAudioSourceParams(source, 1f, 0f, false);
        }
    }

    /// <summary>
    /// Method to personalize the audio source settings in code.
    /// </summary>
    /// <param name="source">
    /// Game object where audio source is.</param>
    /// <param name="volume">
    /// Audio source volume. Default is 1.</param>
    /// <param name="playOnAwake">
    /// Should audio play on awake? Default is false.</param>
    /// <param name="minDistance">
    /// Audio source minimum range. Default is 1.</param>
    /// <param name="maxDistance">
    /// Audio source maximum range. Default is 500.</param>
    public static void ChangeAudioSourceParams(
        GameObject source, float volume = 1f, float spatialBlend = 0.0f,
        bool playOnAwake = false, float minDistance = 1f,
        float maxDistance = 500f)
    {
        if (source.TryGetComponent<AudioSource>(out AudioSource audioSource))
        {
            audioSource.volume = volume;
            audioSource.spatialBlend = spatialBlend;
            audioSource.playOnAwake = playOnAwake;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.outputAudioMixerGroup = _mixerGroup;
        }
    }

    /// <summary>
    /// Method to play a audio clip from game object audio source
    /// </summary>
    /// <param name="obj">
    /// Object which has the audio source.</param>
    /// <param name="audioName">
    /// Audio clip's name.</param>
    /// <param name="pitchShift">
    /// If the sound should pitch shift or not. Default is false.</param>
    /// <param name="loop">
    /// If the sound should loop or not. Default is false.</param>
    /// <param name="checkIfIsPlaying">
    /// Check if the audio source is playing or not. Default is false.</param>
    public static void PlaySound(
        GameObject obj, string audioName, bool pitchShift = false,
        bool loop = false, bool checkIfIsPlaying = false)
    {
        //* Nested method to play a audio clip and not repeat code
        void Play(AudioSource aSource, AudioClip value)
        {
            if (!loop)
            {
                if (pitchShift)
                {
                    aSource.pitch = Random.Range(0.9f, 1.1f);
                    aSource.PlayOneShot(value);
                }
                aSource.PlayOneShot(value);
            }
            else
            {
                aSource.clip = value;
                aSource.loop = true;
                aSource.Play();
            }
        }

        //* Actual method function
        if (obj.TryGetComponent<AudioSource>(out AudioSource source) &&
            _audioDictionary.TryGetValue(audioName, out AudioClip clip))
        {
            if (checkIfIsPlaying)
            {
                if (!source.isPlaying)
                {
                    Play(source, clip);
                }
            }
            else
            {
                Play(source, clip);
            }
        }
    }

    /// <summary>
    /// Method to stop sound from playing.
    /// </summary>
    /// <param name="obj">
    /// Object where the audio source is.
    /// </param>
    public static void StopSound(GameObject obj)
    {
        if (obj.TryGetComponent<AudioSource>(out AudioSource source))
        {
            if (source.isPlaying)
            {
                source.Stop();
            }
        }
    }

    #region DEBUG METHODS
    private void DebugSoundMethod()
    {
        Dictionary<string, AudioClip>.KeyCollection keys =
            _audioDictionary.Keys;

        foreach (string s in keys)
        {
            print($"Audio name: {s}");
        }
    }

    #endregion
}
