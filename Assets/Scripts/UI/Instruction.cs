using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    public GameObject tooltip;
    void Start()
    {
        tooltip.SetActive(false);
        SceneController.Instance.OnCloseScene += RemoveListeners;
        GameController.Instance.OnEatBerry += HideInstruction;
        GameController.Instance.OnNoBerry += ShowInstruction;
    }

    void HideInstruction() {
        tooltip.SetActive(false);
    }
    void ShowInstruction() {
        if (!GameController.Instance.HasInstructions()) {
            GameController.Instance.HasInstructionsTrue();
            tooltip.SetActive(true);
        }
    }

    void RemoveListeners(string scene) {
        GameController.Instance.OnEatBerry -= HideInstruction;
        GameController.Instance.OnNoBerry -= ShowInstruction;
    }
}
