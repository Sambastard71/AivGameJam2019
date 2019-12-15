using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler1 : MonoBehaviour
{
    
    public Animator anim;
    public SceneHandler Scene;
    public GameDatas datas;

    int Counter = 0;
    bool load = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Tutorial1");
            Counter++;
        }

        if (Counter >= 3) LoadAScene();
    }

    public void LoadAScene()
    {
        datas.GuarReachGamePlay = true;
        if (datas.ThiefReachGamePlay)
            Scene.operation.allowSceneActivation = true;

    }
}

  

