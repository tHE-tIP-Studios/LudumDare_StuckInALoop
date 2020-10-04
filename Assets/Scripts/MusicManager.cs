using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MusicManager : MonoBehaviour
{
    private AudioClip _currentMusic;
    private AudioSource _player;
    private bool _wasPlaying;

    public bool Playing => _player.isPlaying;
    public AudioClip CurrentMusic {get; set;}

    private void Awake() 
    {
        _player = gameObject.AddComponent<AudioSource>();

    }

    public void Play(float volume)
    {
        _player.volume = volume;
        _player.clip = _currentMusic;
        _player.Play();
    }

    public void Play(bool loop)
    {
        _player.loop = loop;
        _player.volume = 1f;
        _player.clip = _currentMusic;
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
    

    public UnityEvent onCurrentMusicEnd;
}
