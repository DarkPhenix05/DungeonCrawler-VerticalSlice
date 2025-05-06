using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEnenmy : MonoBehaviour
{
    public AudioSource _voiceLines;
    public AudioSource _swordSource;
    public AudioSource _soundEffects;

    public List<AudioClip> _gruntsClips = new List<AudioClip>();
    public List<AudioClip> _walk = new List<AudioClip>();
    public List<AudioClip> _sword = new List<AudioClip>();

    public void PlayWalkSound()
    {
        if (!_soundEffects.isPlaying)
        {
            _soundEffects.volume = 20.0f;
            _soundEffects.pitch = Random.Range(0.9f, 1.1f);
            _soundEffects.clip = _walk[Random.Range(0, _walk.Count)];
            _soundEffects.Play();
        }
    }

    public void StopWalkSound()
    {
        if (_soundEffects.isPlaying)
        {
            _soundEffects.clip = null;
            _soundEffects.Pause();
        }
    }

    public void PlayAttackSound()
    {
        if (!_voiceLines.isPlaying)
        {
            _voiceLines.pitch = Random.Range(0.9f, 1.1f);
            _voiceLines.clip = _gruntsClips[Random.Range(0, _gruntsClips.Count)];
            _voiceLines.Play();
        }

        _swordSource.Pause();
        _swordSource.pitch = Random.Range(0.9f, 1.1f);
        _swordSource.clip = _sword[Random.Range(0, _sword.Count)];
        _swordSource.Play();

    }
}
