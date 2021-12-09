using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public string destScene; //Destination scene
    public string originScene; //Origin scene
    public string nextCameraPos; //Next CameraPosition
    public float destX; //X destination for player
    public float destY; //Y destination for player

//------------------------------------------------------------------------
//Main User defined functions
    void nextScene(){
        //Grabs cam size because pixelperfect camera is weird
        sceneController.initPlayerPos[0] = destX;
        sceneController.initPlayerPos[1] = destY;
        sceneController.origin = originScene;
        sceneController.cameraPos = nextCameraPos;
        SceneManager.LoadScene(destScene);
    }
    //TEMPORARY CODE TO TRANSITION TO MUSIC SECTION IN BUILD
    void Update(){
        if(Input.GetKeyDown("m")){
            SceneManager.LoadScene(4);
        }
    }

//------------------------------------------------------------------------
//Unity defined functions
    
}
