using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject berryObject;
    public GameObject noBerryObject;
    private Material berryMaterial;
    private Material noBerryMaterial;

    private float berryInfluence = 1f;
    // Start is called before the first frame update
    void Start()
    {
        berryMaterial = berryObject.GetComponent<Renderer>().material;
        noBerryMaterial = noBerryObject.GetComponent<Renderer>().material;
    }

    void Update() {
        if ((GameController.Instance.IsOnBerry() && berryInfluence < 1f) || (!GameController.Instance.IsOnBerry() && berryInfluence > 0f)) {
            if (GameController.Instance.IsOnBerry()) {
                berryInfluence += 0.01f;
            } else {
                berryInfluence -= 0.01f;
            }

            berryMaterial.SetFloat("_appear", berryInfluence);
            noBerryMaterial.SetFloat("_appear", 1f - berryInfluence);
        }
    }
}
