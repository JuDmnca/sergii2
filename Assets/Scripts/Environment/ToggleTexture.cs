using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTexture : MonoBehaviour
{
    private Material objectMaterial;
    public Material objectMaterialDefault;

    private float berryInfluence = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && renderer.material != null) {
            objectMaterial = renderer.material;
        } else {
            objectMaterial = objectMaterialDefault;
        }

        objectMaterial.SetFloat("_BerryInfluence", berryInfluence);
    }

    void Update() {
        if ((GameController.Instance.IsOnBerry() && berryInfluence < 1f) || (!GameController.Instance.IsOnBerry() && berryInfluence > 0f)) {
            if (GameController.Instance.IsOnBerry()) {
                berryInfluence += 0.01f;
            } else {
                berryInfluence -= 0.01f;
            }

            if (objectMaterial != null) {
                objectMaterial.SetFloat("_BerryInfluence", berryInfluence);
            }
        }
    }
}
