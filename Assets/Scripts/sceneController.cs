using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public static string origin;
    public static string cameraPos;
    public static float camSize;
    public static float camAspect;
    public static float[] initPlayerPos = new float[2];

    public GameObject cam;


    void Start()
    {
        cam = GameObject.FindWithTag("MainCamera");

        if(sceneController.origin != null){
            //Going to use the origin scene to determine player and camera position in the next scene
            Debug.Log(sceneController.origin);
            if(cameraPos != null){
                cam.SendMessage("setCamera", cameraPos);
            }else{
                //Camera will just lock to player
            }
        }
    }
}
