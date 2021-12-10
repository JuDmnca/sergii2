using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    [SerializeField]
    public Material berryMaterial1;
    public Material berryMaterial2;
    public Material berryMaterial3;
    public Material noBerryMaterial1;
    public Material noBerryMaterial2;
    public Material noBerryMaterial3;
    private Material[] berryMaterials;
    private Material[] noBerryMaterials;

    private float berryInfluence = 1f;
    // Start is called before the first frame update

    void Start() {
        berryMaterials = new Material[] {berryMaterial1, berryMaterial2, berryMaterial3};
        noBerryMaterials = new Material[] {noBerryMaterial1, noBerryMaterial2, noBerryMaterial3};

        foreach (Material material in berryMaterials) {
            material.SetFloat("_appear", berryInfluence);
        }
        foreach (Material material in noBerryMaterials) {
            material.SetFloat("_appear", 1f - berryInfluence);
        }
    }

    void Update() {
        if ((GameController.Instance.IsOnBerry() && berryInfluence < 1f) || (!GameController.Instance.IsOnBerry() && berryInfluence > 0f)) {
            if (GameController.Instance.IsOnBerry()) {
                berryInfluence += 0.01f;
            } else {
                berryInfluence -= 0.01f;
            }

            foreach (Material material in berryMaterials) {
                material.SetFloat("_appear", berryInfluence);
            }
            foreach (Material material in noBerryMaterials) {
                material.SetFloat("_appear", 1f - berryInfluence);
            }
        }
    }
}
