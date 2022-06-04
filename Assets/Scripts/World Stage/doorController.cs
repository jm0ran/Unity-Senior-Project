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
    public string direction;

    public GameObject fadeShade;
    public GameObject player;
    public AudioSource doorSFX;

//------------------------------------------------------------------------
//Main User defined functions
    void nextScene(){
        doorSFX.Play();
        //Grabs cam size because pixelperfect camera is weird
        sceneController.initPlayerPos[0] = destX;
        sceneController.initPlayerPos[1] = destY;
        sceneController.origin = originScene;
        sceneController.cameraPos = nextCameraPos;
        StartCoroutine(UIController.fadeOut(destScene));
        // StartCoroutine(fadeTransition(destScene));
    }



//------------------------------------------------------------------------
//Unity defined functions
    void Awake(){
        fadeShade = GameObject.FindWithTag("fadeShade");
        player = GameObject.FindWithTag("Player");
        doorSFX = gameObject.GetComponent<AudioSource>();
    }
}

