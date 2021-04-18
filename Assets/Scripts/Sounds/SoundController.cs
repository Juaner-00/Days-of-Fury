using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundController : MonoBehaviour
{
    private static float volumeMultiplier = 1f;
    private static float musicVolumeMultiplier = 1f;

    public static float _VolumeMultiplier { get => volumeMultiplier;  set => volumeMultiplier = Mathf.Clamp(value, 0f, 1f); }
    public static float _MusicVolumeMultiplier { get => musicVolumeMultiplier; set => musicVolumeMultiplier = Mathf.Clamp(value, 0f, 1f); }

    public bool isMusicController = false;

    [SerializeField] protected ActionClips[] actionClips;
    protected AudioSource[] sources;

    protected virtual void Awake() {
        sources = new AudioSource[actionClips.Length];

        for(int i = 0; i < sources.Length; i++){
            var source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            sources[i] = source;
            sources[i].loop = false;
            sources[i].playOnAwake = false;

            if(actionClips[i].Clips.Length > 0) sources[i].clip = actionClips[i].Clips[0];
        }

        ChildAwake();
    }

    // Reproduce un sonido
    public void Play(int index, bool? randomClip = null, bool? loop = null) {
        if(actionClips[index].Clips.Length > 0) { 
            sources[index].clip = randomClip == null? 
                sources[index].clip : actionClips[index].Clips[UnityEngine.Random.Range(0, actionClips[index].Clips.Length)];
            sources[index].loop = loop == null? false : (bool)loop;
            sources[index].volume = actionClips[index].ActionVolume /* (isMusicController == true? _MusicVolumeMultiplier : _VolumeMultiplier)*/;

            sources[index].Play();
        }
    }

    // Detiene el sonido que se está reproduciendo
    public void Stop(int index) {
        sources[index].Stop();
    }
    
    // Detiene todos los sonidos que se reproducen
    public void StopAll() {
        for(int i = 0; i < sources.Length; i++) sources[i].Stop();
    }

    // Agrega un sonido al un audiosource
    public void SetClip(int index, AudioClip newClip) {
        sources[index].clip = newClip;
    }

    protected void PlaySourceByName(string sourceName, bool? randomClip = null, bool? loop = null)
    {
        bool isRandom = randomClip != null ? (bool)randomClip : false;
        bool isLoop = loop != null ? (bool)loop : false;

        for (int i = 0; i < actionClips.Length; i++)
        {
            if (actionClips[i].ActionName == sourceName)
            {
                if (sources[i].isPlaying == false && gameObject.activeSelf == true) Play(i, isRandom, isLoop);
            }
        }
    }
    protected void StopSourceByName(string sourceName)
    {
        for (int i = 0; i < actionClips.Length; i++)
        {
            if (actionClips[i].ActionName == sourceName)
            {
                Stop(i);
            }
        }
    }

    protected abstract void ChildAwake();
}
