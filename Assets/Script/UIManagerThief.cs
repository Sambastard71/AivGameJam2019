using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerThief : MonoBehaviour
{
    Mesh mesh;
    public GameObject gameObj;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GameObject.FindGameObjectWithTag("PaintWin").GetComponent<MeshFilter>().mesh;
        MeshRenderer mr = GameObject.FindGameObjectWithTag("PaintWin").GetComponent<MeshRenderer>();
        gameObj.GetComponent<MeshFilter>().mesh = mesh;

        Material mat = mr.material;

        gameObj.GetComponent<MeshRenderer>().material = mat;
       

    }

    // Update is called once per frame
    void Update()
    {
        


    }
}
