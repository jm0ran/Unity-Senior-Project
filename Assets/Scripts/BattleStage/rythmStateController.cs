using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rythmStateController : MonoBehaviour
{
//Rythm State Controller is deprecated, was being used to manage states when battle stage was based on action points
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    static public string currentState; //Current state
    private GameObject audioControllerObj; //Audio Controller Object reference
    private GameObject rythmInputController; //RythmInputController reference
    private GameObject[] arrows; //Arrows storage
    private GameObject [] arrowTargets; //Arrow target storage
    private GameObject actionUI; //ActionUI reference
    private GameObject diaUI; //diaUI reference

    //Historic Comments
    //       v
    //This is where I want to control the state of the game and manage transitioning between the stages of battle and Rythm
    //States are going to include
    // -entry: Music and notes are both not currently active, not sure if or when I'll implement this state
    // -rythm: Music and notes are currently playing
    // -action: Music is still playing but notes are pause and player is met with a menu that will let them take action

//------------------------------------------------------------------------
//User Defined functions
    void triggerRemainingNotes(){ //Triggered the remaining notes
        arrows = GameObject.FindGameObjectsWithTag("arrow"); //Grabs all arrows in scene
        for(int i = 0; i < arrows.Length; i++){ //For each arrow
            arrows[i].SendMessage("triggerNote"); //Trigger note
        }
        arrows = null; //Set arrows to null
    }

    void enterDia(){ //this is where I wanted to enter the dialogue poiny
        //This is where I want to enter the dialogue stage
    }


    void initState(){ //Initialization state functio
        diaUI.SetActive(false); //Disable diaUI
    }


    void Start()
    {
        currentState = "rythm"; //Starting state of rythm
        initState(); //Start init function

    }

    private void Awake() {
        diaUI = GameObject.Find("diaUI"); //Assign following function
    }

    
}
