using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SceneTransition : MonoBehaviour
{
    private Volume volume;
    private Circle vignette;
    private bool isHidden = false;
    private string[] openScenes;
    private string[] closeScenes;

    void Start()
    {
        SceneController.Instance.OnTransitionScene += HandleTransition;
        volume = GetComponent<Volume>();

        Circle tmp;
        if (volume.profile.TryGet<Circle>(out tmp))
        {
            vignette = tmp;
        }
    }

    void HandleTransition(string[] close, string[] open)
    {
        isHidden = true;
        openScenes = open;
        closeScenes = close;
    }

    void OpenAndCloseScenes()
    {
        if (openScenes != null && closeScenes != null) {
            foreach (string scene in closeScenes)
            {
                SceneController.Instance.CloseScene(scene);
            }
            foreach (string scene in openScenes)
            {
                SceneController.Instance.OpenSceneSync(scene);
            }

            openScenes = null;
            closeScenes = null;
            isHidden = false;
        }
    }

    void Transition() {
        if (vignette == null)
            return;

        if (isHidden == true && vignette.intensity.value < 1f){
            vignette.setIntensity(vignette.intensity.value + 0.01f);
            return;
        }

        if (isHidden == true && vignette.intensity.value >= 1f) {
            vignette.setIntensity(1f);
            OpenAndCloseScenes();
            return;
        }

        if (isHidden == false && vignette.intensity.value > 0f) {
            vignette.setIntensity(vignette.intensity.value - 0.01f);
            return;
        }
    }

    void Update() {
        Transition();
    }
}
