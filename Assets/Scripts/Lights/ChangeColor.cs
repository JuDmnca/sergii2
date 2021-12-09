using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private GameObject _pointlight;
    private GameObject _spotlight;
    private GameObject _sphere;
    private Renderer myRenderer;
    private Light pointlight;
    private Light spotlight;

    private GameObject[] objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = GameObject.FindGameObjectsWithTag("Light");
        SceneController.Instance.OnCloseScene += RemoveListeners;

        GameController.Instance.OnEatBerry += UpdateColorNormal;
        GameController.Instance.OnNoBerry += UpdateColorRed;

        if (GameController.Instance.IsOnBerry()) {
            UpdateColorNormal();
        } else {
            UpdateColorRed();
        }
    }

    void UpdateColorRed() {
        foreach(GameObject light in objects) {
            _spotlight = light.transform.GetChild(0).gameObject;
            _pointlight = light.transform.GetChild(1).gameObject;
            _sphere = light.transform.GetChild(2).gameObject;

            myRenderer = _sphere.GetComponent<Renderer>();
            pointlight = _pointlight.GetComponent<Light>();
            spotlight = _spotlight.GetComponent<Light>();

            myRenderer.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.red * 10000f);
            pointlight.color = Color.red;
            spotlight.color = Color.red;
        }
    }
    void UpdateColorNormal() {
        foreach(GameObject light in objects) {
            _spotlight = light.transform.GetChild(0).gameObject;
            _pointlight = light.transform.GetChild(1).gameObject;
            _sphere = light.transform.GetChild(2).gameObject;

            myRenderer = _sphere.GetComponent<Renderer>();
            pointlight = _pointlight.GetComponent<Light>();
            spotlight = _spotlight.GetComponent<Light>();

            myRenderer.GetComponent<Renderer>().material.SetColor("_EmissiveColor", Color.yellow * 10000f);
            pointlight.color = Color.yellow;
            spotlight.color = Color.yellow;
        }
    }

    void RemoveListeners(string scene) {
        GameController.Instance.OnEatBerry -= UpdateColorNormal;
        GameController.Instance.OnNoBerry -= UpdateColorRed;
    }
}
