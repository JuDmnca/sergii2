using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ChangeSkybox : MonoBehaviour
{
    public Volume volume;
    public Cubemap sunsetMat;
    public Cubemap cloudyMat;

    void Start()
    {
        SceneController.Instance.OnCloseScene += RemoveListeners;

        GameController.Instance.OnEatBerry += UpdateSkybox;
        GameController.Instance.OnNoBerry += UpdateSkybox;

        UpdateSkybox();
    }

    void UpdateSkybox() {
        VolumeProfile profile = volume.sharedProfile;
        if (!profile.TryGet<HDRISky>(out var sky))
        {
            sky = profile.Add<HDRISky>(false);
        }
        if (GameController.Instance.IsOnBerry()) {
            sky.hdriSky.Override(sunsetMat);
            sky.exposure.value = 14f;
        } else {
            sky.hdriSky.Override(cloudyMat);
            sky.exposure.value = 15f;
        }
    }

    void RemoveListeners(string scene) {
        GameController.Instance.OnEatBerry -= UpdateSkybox;
        GameController.Instance.OnNoBerry -= UpdateSkybox;
    }
}