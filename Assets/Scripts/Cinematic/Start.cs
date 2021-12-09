using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Start: MonoBehaviour
{
    private KeyCode key = KeyCode.Return;

    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            SceneController.Instance.TransitionScene(new string[] {"Menu", "PreviewCity"}, new string[] {"Intro"});
        }
    }
}
