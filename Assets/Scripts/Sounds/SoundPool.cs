using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundPool
{
    private List<AudioSource> playingSources;
    private Queue<AudioSource> stoppedSources;

    GameObject gameObject;

    SoundPool instance;

    public SoundPool(GameObject gameObject)
    {
        SetUp(gameObject);
    }

    private void SetUp(GameObject gameObject)
    {
        this.gameObject = gameObject;

        playingSources = new List<AudioSource>();
        stoppedSources = new Queue<AudioSource>();
    }

    public IEnumerator PlayCorroutine(AudioClip clip, float volume, bool loop, float spacing3D)
    {
        if (stoppedSources.Count <= 0)
        {
            bool firstSourceCreated = false;
            if(playingSources.Count <= 0)
            {
                stoppedSources.Enqueue(gameObject.AddComponent(typeof(AudioSource)) as AudioSource);
                firstSourceCreated = true;
//                Debug.Log("Fuente inicial creada");
            }

            if (!firstSourceCreated)
            {
                if (!loop)
                {
                    stoppedSources.Enqueue(gameObject.AddComponent(typeof(AudioSource)) as AudioSource);
//                    Debug.Log("Fuente creada");
                }
                else
                {
                    yield break;
                }
            }
        }

        //      Reproduzca una fuente detenida.
        var source = stoppedSources.Dequeue();
        source.loop = loop;
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = spacing3D;
        playingSources.Add(source);
        source.Play();
//        Debug.Log("Fuente reproduciendo");

        if (loop) yield break;

        yield return new WaitForSecondsRealtime(source.clip.length);

        //      Devuelva la fuente a la cola de fuentes detenidas.
        source.Stop();
        playingSources.Remove(source);
        stoppedSources.Enqueue(source);
//        Debug.Log("Fuente detenida");
    }

    public void StopAll()
    {
        
        if (NullCatcher())
        {
            try
            {
                SetUp(gameObject);
                Debug.Log($"Null catched. Reseted");
                return;
            }
            catch
            {
                Debug.Log($"Game Object is null = {gameObject == null}");
            }
        }
        
        Debug.Log($"Queue = {playingSources.Count}");
        for(int psI = 0; psI < playingSources.Count; psI++)
        {
            if (playingSources.Count > 0)
            {
                var ps = playingSources[psI];
                if (ps == null)
                {
                    SetUp(gameObject);
                    Debug.Log("JUEPUTAAA");
                }
                else ps.Stop();
                stoppedSources.Enqueue(ps);
            }
        }
    }

    private bool NullCatcher()
    {
        //return playingSources == null || stoppedSources == null;
        bool diagnostic = default;

        int playingNulls = 0, stoppedNulls = 0;

        for(int i = 0; i < playingSources.Count; i++)
        {
            if(playingSources[i] == null) playingNulls++;
        }

        var backUpQueue = stoppedSources;
        List<AudioSource> laLista = new List<AudioSource>();

        for(int i = backUpQueue.Count; i > 0; i--)
        {
            laLista.Add(backUpQueue.Dequeue());
        }

        Debug.Log($"Lista = {laLista.Count} | Queue = {stoppedSources.Count} | Back Up queue = {backUpQueue.Count}");

        for (int i = 0; i < stoppedSources.Count; i++)
        {
            if (laLista[i] == null) stoppedNulls++;
        }

        diagnostic = (stoppedNulls == laLista.Count && stoppedNulls > 0) || (playingNulls == playingSources.Count && playingNulls > 0);

        return diagnostic;
    }
}
