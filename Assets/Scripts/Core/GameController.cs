using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    private bool menuOpen = false;
    private bool startedGame = false;
    private bool hasInstructions = false;
    private bool isOnBerry = true;
    private int taskIndex = 0;
    private Vector3 sergePosition = new Vector3(-25.8f, 0f, -78.6f);
    public delegate void GameControllerEvent();
    public event GameControllerEvent OnEatBerry;
    public event GameControllerEvent OnNoBerry;

    public void TakeBerry(){
        isOnBerry = true;
        OnEatBerry?.Invoke();
    }

    public void CancelBerry(){
        isOnBerry = false;
        OnNoBerry?.Invoke();
    }

    public bool IsOnBerry () {
        return isOnBerry;
    }

    public void FinishTask(){
        taskIndex += 1;
    }

    public int TaskIndex () {
        return taskIndex;
    }

    public bool HasInstructions () {
        return hasInstructions;
    }

    public void HasInstructionsTrue () {
        hasInstructions = true;
    }

    public void SetSergePosition (Vector3 position) {
        sergePosition = position;
    }

    public Vector3 SergePosition () {
        return sergePosition;
    }

    public void SetMenuOpen(bool value) {
        menuOpen = value;
    }

    public bool MenuOpen() {
        return menuOpen;
    }

    public void StartGame() {
        startedGame = true;
    }

    public bool StartedGame() {
        return startedGame;
    }
}