using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{

//The Door Controller is used for holding information and behavior of doors across the map

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public string destScene; //Destination scene
    public string originScene; //Origin scene
    public string nextCameraPos; //Next CameraPosition
    public float destX; //X destination for player
    public float destY; //Y destination for player
    public string direction; //Direction for door

    public GameObject fadeShade; //Fading layer
    public GameObject player; //Reference to Player
    public AudioSource doorSFX; //Door sound effect

//------------------------------------------------------------------------
//Main User defined functions
    void nextScene(){ //Function to go to next screen
        doorSFX.Play(); //Play door sound effect
        //Grabs cam size because pixelperfect camera is weird
        //Set initial player information for spawn in next scene
        sceneController.initPlayerPos[0] = destX;
        sceneController.initPlayerPos[1] = destY;
        sceneController.origin = originScene;
        sceneController.cameraPos = nextCameraPos;
        StartCoroutine(UIController.fadeOut(destScene)); //Start the fade out transition
        // StartCoroutine(fadeTransition(destScene));
    }

//------------------------------------------------------------------------
//Unity defined functions
    void Awake(){
        //Set the appropriate values
        fadeShade = GameObject.FindWithTag("fadeShade");
        player = GameObject.FindWithTag("Player");
        doorSFX = gameObject.GetComponent<AudioSource>();
    }
}

