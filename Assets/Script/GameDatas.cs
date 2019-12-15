using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameDatas : ScriptableObject
{
    public bool GuarReachGamePlay;
    public bool ThiefReachGamePlay;

    void inizialize()
    {
        GuarReachGamePlay = false;
        ThiefReachGamePlay = false;
    }

}
