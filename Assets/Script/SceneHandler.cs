using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public AsyncOperation operation;
    public Animator anim;
    public GameDatas datas;

    int Counter=0;
    bool load=false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {


            Counter++;
            anim.SetTrigger("Tutorial1");
            if (!load)
            {
                operation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Single);
                load = true;
            }
                operation.allowSceneActivation = false;
        }

        if (Counter >= 3) LoadAScene();
    }

    public void LoadAScene()
    {
        datas.ThiefReachGamePlay = true;
        if(datas.GuarReachGamePlay)
        operation.allowSceneActivation = true;
    } 

    

}
