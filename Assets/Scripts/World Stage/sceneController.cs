using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneController : MonoBehaviour
{

//The Scene Controller is used to store information used for scene transitions

//------------------------------------------------------------------------
//Main variables used in Scripts
    //Static objects, accessible from any script
    public static string origin; //origin scene storage
    public static string cameraPos; //Camera position storage
    public static float camSize; //Cam Size Storage
    public static float camAspect; //Cam Aspect storage
    public static float[] initPlayerPos = new float[2]; //Empty float array for initial player position
    public GameObject cam; //Reference to camera object
    public GameObject player; //Referebce to player object
    public bool triggered = false; //Bool for triggered state

//------------------------------------------------------------------------
//Unity defined functions
    void Start(){ //Runs on start of any scene and makes initial call to Camera in order to set it's initial position, this could be improved/removed later on
        player = GameObject.FindWithTag("Player"); //Gets player reference
        cam = GameObject.FindWithTag("MainCamera"); //Gets camera reference
        if(sceneController.origin != null){ //Prevents from running on startup of program
            if(cameraPos != null){ //If camera position is set to something
                cam.SendMessage("setCamera", cameraPos); //Send new camera position
            }else{ //If camera position is null
                //Camera will just lock to player
            }
        }
    }
    
}
