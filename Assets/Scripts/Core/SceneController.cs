using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public delegate void SceneControllerEvent(string scene);
    public event SceneControllerEvent OnOpenScene;
    public event SceneControllerEvent OnCloseScene;
    public delegate void SceneControllerTransitionEvent(string[] openScenes, string[] closeScenes);
    public event SceneControllerTransitionEvent OnTransitionScene;

    public void OpenScene(string scene, LoadSceneMode mode = LoadSceneMode.Additive){
        SceneManager.LoadSceneAsync(scene, mode);
        OnOpenScene?.Invoke(scene);
    }

    public void CloseScene(string scene){
        SceneManager.UnloadSceneAsync(scene);
        OnCloseScene?.Invoke(scene);
    }

    public void OpenSceneSync(string scene, LoadSceneMode mode = LoadSceneMode.Additive) {
        OnOpenScene?.Invoke(scene);
        SceneManager.LoadScene(scene, mode);
    }

    public void TransitionScene(string[] closeScenes, string[] openScenes){
        OnTransitionScene?.Invoke(closeScenes, openScenes);
    }
}