using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.Instance.OpenScene("PreviewCity");
        SceneController.Instance.OpenScene("Menu");
    }
}
