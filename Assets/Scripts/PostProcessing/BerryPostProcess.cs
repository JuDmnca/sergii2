using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
public class BerryPostProcess : MonoBehaviour
{
    public Volume berryVolume;
    public Volume noBerryVolume;
    private float berryInfluence = 0f;

    // Update is called once per frame
    void Update()
    {
        if (((GameController.Instance.IsOnBerry() && berryInfluence < 1f) || (!GameController.Instance.IsOnBerry() && berryInfluence > 0f)) && GameController.Instance.StartedGame()) {
            if (GameController.Instance.IsOnBerry()) {
                berryInfluence += 0.01f;
            } else {
                berryInfluence -= 0.01f;
            }

            berryVolume.weight = berryInfluence;
            noBerryVolume.weight = 1f - berryInfluence;
        }
    }
}
