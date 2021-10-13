using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public static string origin;
    void Start()
    {
        if(sceneController.origin != null){
            //Going to use the origin scene to determine player and camera position in the next scene
            Debug.Log(sceneController.origin);
        }
    }
}
