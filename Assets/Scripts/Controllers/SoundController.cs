using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundController : MonoBehaviour
{
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

    public void Play(int index, bool? randomClip = null, bool? loop = null) {
        if(actionClips[index].Clips.Length > 0) { 
            sources[index].clip = randomClip == null? 
                sources[index].clip : actionClips[index].Clips[UnityEngine.Random.Range(0, actionClips[index].Clips.Length)];
            sources[index].loop = loop == null? false : (bool)loop;
            sources[index].volume = actionClips[index].ActionVolume;

            sources[index].Play();
        }
    }

    public void Stop(int index) {
        sources[index].Stop();
    }
    public void StopAll() {
        for(int i = 0; i < sources.Length; i++) sources[i].Stop();
    }

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
