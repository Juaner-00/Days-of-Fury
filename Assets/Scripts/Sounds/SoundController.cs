using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SoundController : MonoBehaviour
{
    [SerializeField] private ActionClips[] actionClips;

    private Dictionary<string, SoundPool> sources;

    private void Awake()
    {
        sources = new Dictionary<string, SoundPool>();

        for(int acI = 0; acI < actionClips.Length; acI++)
        {
            SoundPool sPool = new SoundPool(gameObject);
            sources.Add(actionClips[acI].ActionName, sPool);
        }

        SetUp(true);
    }

    protected abstract void SetUp(bool child);

    protected void PlayActionByName(string actionName, bool random = false, bool loop = false)
    {
        for(int acI = 0; acI < actionClips.Length; acI++)
        {
            if(actionClips[acI].ActionName == actionName)
            {
                SoundPool sPool = default;
                if(sources.TryGetValue(actionName, out sPool))
                {
                    AudioClip[] clips = actionClips[acI].Clips;
                    int clipI = random? UnityEngine.Random.Range(0, clips.Length) : 0;
                    StartCoroutine(sPool.PlayCorroutine(clips[clipI], loop));
                }
            }
        }
    }
    
    protected void StopActionByName(string actionName)
    {
        for(int acI = 0; acI < actionClips.Length; acI++)
        {
            if(actionClips[acI].ActionName == actionName)
            {
                SoundPool sPool = default;
                if (sources.TryGetValue(actionName, out sPool))
                {
                    sPool.StopAll();
                }
            }
        }
    }
}
