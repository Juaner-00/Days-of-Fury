using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSoundManager : IndctBuildingSoundManager
{
    void Start()
    {
        PlayWorking();
    }

    public void PlayWorking()
    {
        PlayActionByName("Working", SPACING_3D, false, true);
    }
}
