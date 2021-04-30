using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : SoundController
{
    const string MIXER_PATH = "Audio/Music Mixer";

    #region SINGLETON
    private static MusicManager instance;

    public static MusicManager Instance { get => instance; }

    protected override void ChildAwake()
    {
        if (Instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        SubscribeEvents();
        ApplyAudioMixer();
    }
    #endregion

    [Header("Scenes attached to each action")]
    [SerializeField] ScenesAction[] scenesActions;

    private void SubscribeEvents() {
        SceneManager.sceneLoaded -= UpdateMusicToLoadedScene;
        SceneManager.sceneLoaded += UpdateMusicToLoadedScene;
    }
    private void ApplyAudioMixer() {
        for(int i = 0; i < sources.Count; i++) {
            
            sources[actionClips[i].ActionName][0].outputAudioMixerGroup = Resources.Load<AudioMixerGroup>(MIXER_PATH);
        }
    }
    void UpdateMusicToLoadedScene(Scene scene, LoadSceneMode loadMode) {
        Debug.Log("********************************* Scene load detection is correct");

        StopAll();

        for(int i = 0; i < scenesActions.Length; i++) {
            if (scenesActions[i].SceneNames.Contains(scene.name)) {
                PlaySourceByName(scenesActions[i].ActionName, scenesActions[i].PlayRandomClip, true);
            }
        }
    }

    #region EDITOR
    private void OnValidate()
    {
        //  ASIGANR TANTAS scenesAction COMO actionClips HAYA. 

        if(actionClips.Length != scenesActions.Length) {
            //  Revisa que sean los actionClips quienes han cambiado...
                if (actionClips.Length != ActionsQuant) {

                    List<List<string>> ScenesNames = new List<List<string>>();

                    //  Guarda los nombres de las escenas asignadas a cada acción.
                    for (int i = 0; i < scenesActions.Length; i++) 
                        if (ScenesNames.Contains(scenesActions[i].SceneNames) == false) ScenesNames.Add(scenesActions[i].SceneNames);

                    scenesActions = new ScenesAction[actionClips.Length];
                    for (int i = 0; i < ScenesNames.Count; i++)
                        if (i < scenesActions.Length && i < ScenesNames.Count) scenesActions[i].SceneNames = ScenesNames[i];
            }
            //  ...sino, las scenesAction vuelven a ser las mismas.
            else scenesActions = ScenesActions;
        }

        //   Asignar el nombre de los actionClips correnpondientes a cada scenesAction.
        for (int i = 0; i < scenesActions.Length; i++) scenesActions[i].ActionName = actionClips[i].ActionName;
        
        //  Recuerda la cantidad de actionClips.
        ActionsQuant = actionClips.Length;
        //  Recuerda las scenesActions.
        ScenesActions = scenesActions;
    }

    private int ActionsQuant { get; set; }
    private ScenesAction[] ScenesActions { get; set; }
    #endregion
}
