using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KitchenController : Singleton<KitchenController>
{
    public delegate void KitchenControllerEvent();
    public event KitchenControllerEvent OnCutOver;

    public void CutOver(){
      OnCutOver?.Invoke();
    }
}