using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoundPool
{
    private List<AudioSource> playingSources;
    private Queue<AudioSource> stoppedSources;

    GameObject gameObject;


    public SoundPool(GameObject gameObject)
    {
        this.gameObject = gameObject;

        playingSources = new List<AudioSource>();
        stoppedSources = new Queue<AudioSource>();
    }

    public IEnumerator PlayCorroutine(AudioClip clip, bool loop)
    {
        if (stoppedSources.Count <= 0)
        {
            bool firstSourceCreated = false;
            if(playingSources.Count <= 0)
            {
                stoppedSources.Enqueue(gameObject.AddComponent(typeof(AudioSource)) as AudioSource);
                firstSourceCreated = true;
                Debug.Log("Fuente inicial creada");
            }

            if (!firstSourceCreated)
            {
                if (!loop)
                {
                    stoppedSources.Enqueue(gameObject.AddComponent(typeof(AudioSource)) as AudioSource);
                    Debug.Log("Fuente creada");
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
        playingSources.Add(source);
        source.Play();
        Debug.Log("Fuente reproduciendo");

        if (loop) yield break;

        yield return new WaitForSecondsRealtime(source.clip.length);

        //      Devuelva la fuente a la cola de fuentes detenidas.
        source.Stop();
        playingSources.Remove(source);
        stoppedSources.Enqueue(source);
        Debug.Log("Fuente detenida");
    }

    public void StopAll()
    {
        for(int psI = 0; psI < playingSources.Count; psI++)
        {
            var ps = playingSources[psI];
            ps.Stop();
            stoppedSources.Enqueue(ps);
        }
    }
}
