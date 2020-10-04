using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerSnapshot _normalSnapshot;
    [SerializeField] private AudioMixerSnapshot _explosionSnapshot;
    private AudioSource _player;
    private bool _wasPlaying;

    public bool Playing => _player.isPlaying;
    public AudioClip CurrentMusic {get; set;}

    private void Awake()
    {
        _player = gameObject.AddComponent<AudioSource>();
        _player.outputAudioMixerGroup = _musicGroup;

    }

    public void Play(float volume)
    {
        _player.volume = volume;
        _player.clip = CurrentMusic;
        _player.Play();
    }

    public void Play(bool loop)
    {
        _player.loop = loop;
        _player.volume = 1f;
        _player.clip = CurrentMusic;
        _player.Play();
    }

    private void Update() 
    {
        if (!Playing && _wasPlaying)
        {
            onCurrentMusicEnd?.Invoke();
        }
        _wasPlaying = Playing;
    }
    
    public void Explosion()
    {
        StartCoroutine(Transition());

        IEnumerator Transition()
        {
            _explosionSnapshot.TransitionTo(0.2f);
            yield return new WaitForSeconds(0.2f);
            _normalSnapshot.TransitionTo(0.2f);
        }
    }

    public UnityEvent onCurrentMusicEnd;
}
