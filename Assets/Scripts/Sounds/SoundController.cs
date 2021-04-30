using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// CONVERTIR EL FUNCIONAMIENTO AL USO APROPIADO DEL DICCIONARIO;   se propone la creación de una clase "SourcesUtilities", 
///                                                                 quien controle la reproducción de las listas de AudioSources.
/// </summary>
public abstract class SoundController : MonoBehaviour
{
    private static float volumeMultiplier = 1f;
    private static float musicVolumeMultiplier = 1f;

    public static float _VolumeMultiplier { get => volumeMultiplier;  set => volumeMultiplier = Mathf.Clamp(value, 0f, 1f); }
    public static float _MusicVolumeMultiplier { get => musicVolumeMultiplier; set => musicVolumeMultiplier = Mathf.Clamp(value, 0f, 1f); }

    public bool isMusicController;

    [SerializeField] protected ActionClips[] actionClips;
    protected Dictionary<string, List<AudioSource>> sources;



    public virtual void Awake() {
        sources = new Dictionary<string, List<AudioSource>>();
        List<AudioSource> actionSources = new List<AudioSource>();
        isMusicController = false;

        for(int i = 0; i < actionClips.Length; i++) {
            Debug.Log("Source Added");
            var source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            source.loop = false;
            source.playOnAwake = false;
            if(actionClips[i].Clips.Length > 0) source.clip = actionClips[i].Clips[0];
            actionSources.Add(source);

            sources.Add(actionClips[i].ActionName, actionSources);
        }

        ChildAwake();
    }

        // Reproduce un sonido
    public void Play(string actionName, bool? randomClip = null, bool? loop = null) {
        for(int i = 0; i < actionClips.Length; i++)
        {
            if(actionClips[i].ActionName == actionName && actionClips[i].Clips.Length > 0) { 
                sources[actionName][0].clip = randomClip == null? 
                sources[actionName][0].clip : actionClips[i].Clips[UnityEngine.Random.Range(0, actionClips[i].Clips.Length)];
                sources[actionName][0].loop = loop == null? false : (bool)loop;
                sources[actionName][0].volume = actionClips[i].ActionVolume /* (isMusicController == true? _MusicVolumeMultiplier : _VolumeMultiplier)*/;

                sources[actionName][0].Play();
            }
        }
    }

    // Detiene el sonido que se está reproduciendo
    public void Stop(string actionName) {
        sources[actionName][0].Stop();
    }
    
    // Detiene todos los sonidos que se reproducen
    public void StopAll() {
        for(int i = 0; i < sources.Count; i++) sources[i].Stop();
    }

    // Agrega un sonido al un audiosource
    public void SetClip(string actionName, AudioClip newClip) {
        sources[actionName][0].clip = newClip;
    }

    protected void PlaySourceByName(string sourceName, bool? randomClip = null, bool? loop = null)
    {
        bool isRandom = randomClip != null ? (bool)randomClip : false;
        bool isLoop = loop != null ? (bool)loop : false;

        //  Crear un diccionario para cada tipo de audio que contenga su respectiva lista de AudioSources.
        for (int i = 0; i < actionClips.Length; i++)
        {
            if (actionClips[i].ActionName == sourceName)
            {
                for(int j = 0; j < sources.Count; j++)
                {
                    if (sources[j] != null)
                    {
                        if (sources[j].isPlaying == false)
                        {
                            if (gameObject.activeSelf == true)
                            {
                                Debug.Log("Entré");
                                Play(j, isRandom, isLoop);
                            }
                        }
                        else if (isMusicController == false)
                        {

                            Debug.Log("x2");
                            if (isLoop == false)
                            {
                                Debug.Log("x3");
                                if (j < sources.Count - 2) continue;
                                AddAudioSource(sourceName, randomClip, loop);
                            }
                        }
                    }
                    else
                    {
                        //AddAudioSource(sourceName, randomClip, loop);
                        Debug.Log("*********************** CATCHED");
                    }
                }
            }
        }
    }

    private IEnumerator PlaySourceCorroutine(AudioSource source, float duration)
    {
        source.Play();

        yield return new WaitForSecondsRealtime(duration);

        source.Stop();
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

    private void AddAudioSource(string sourceName, bool? randomClip = null, bool? loop = null)
    {
        bool isRandom = randomClip != null ? (bool)randomClip : false;
        bool isLoop = loop != null ? (bool)loop : false;

        AudioClip clip = default;

        for (int i = 0; i < actionClips.Length; i++)
        {
            if (actionClips[i].ActionName == sourceName)
            {
                clip = actionClips[i].Clips[isRandom == true ? UnityEngine.Random.Range(0, actionClips[i].Clips.Length) : 0];
            }
        }


        AudioSource newAudioSource = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        newAudioSource.playOnAwake = false;
        newAudioSource.loop = isLoop;
        newAudioSource.clip = clip;

        //StartCoroutine(PlaySourceCorroutine(newAudioSource, clip.length));

        sources.Add(newAudioSource);

        PlaySourceByName(sourceName, isRandom, isLoop);
    }

    protected abstract void ChildAwake();
}
