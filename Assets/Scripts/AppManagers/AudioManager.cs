using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource masterAudioSource, musicAudioSource, sfxAudioSource; //For the AudioMixer Channels
    public UnityEngine.Audio.AudioMixerGroup _sfxAudioSource { get => sfxAudioSource.outputAudioMixerGroup; }
    
    private void Start() {
        
    }
    public void PlayMusic(){
        musicAudioSource.Play(); //there is no music yet
    }
}
