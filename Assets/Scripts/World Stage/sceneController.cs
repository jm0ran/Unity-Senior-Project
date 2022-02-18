using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneController : MonoBehaviour
{

//------------------------------------------------------------------------
//Main variables used in Scripts
    //Static objects, accessible from any script
    public static string origin;
    public static string cameraPos;
    public static float camSize;
    public static float camAspect;
    public static float[] initPlayerPos = new float[2];
    //Script specific object
    public GameObject cam; ///This is dumb because it's finding it's own gameObject, something to clean up later 
    public GameObject player;
    //Add a boolean to determine if there is starting dialogue for this scene
    public bool triggered = false;
    

//------------------------------------------------------------------------
//User defined functions

//------------------------------------------------------------------------
//Unity defined functions
    void Start(){ //Runs on start of any scene and makes initial call to Camera in order to set it's initial position, this could be improved/removed later on
        player = GameObject.FindWithTag("Player");
        cam = GameObject.FindWithTag("MainCamera"); //locates the camera using the tag
        if(sceneController.origin != null){ //Prevents from running on startup of program
            if(cameraPos != null){
                cam.SendMessage("setCamera", cameraPos);
            }else{
                //Camera will just lock to player
            }
        }
        
    }
    
}
