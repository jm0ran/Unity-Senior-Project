using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rythmStateController : MonoBehaviour
{
    static public string currentState;
    private GameObject audioControllerObj;
    private GameObject rythmInputController;
    private GameObject[] arrows;
    private GameObject [] arrowTargets;
    private GameObject actionUI;
    private GameObject diaUI;

    //This is where I want to control the state of the game and manage transitioning between the stages of battle and Rythm
    //States are going to include
    // -entry: Music and notes are both not currently active, not sure if or when I'll implement this state
    // -rythm: Music and notes are currently playing
    // -action: Music is still playing but notes are pause and player is met with a menu that will let them take action

    // Start is called before the first frame update
    void triggerRemainingNotes(){
        arrows = GameObject.FindGameObjectsWithTag("arrow");
        for(int i = 0; i < arrows.Length; i++){
            arrows[i].SendMessage("triggerNote");
        }
        arrows = null;
    }

    void enterDia(){ //this is where I want to enter the dialogue poiny
        //This is where I want to enter the dialogue stage
    }


    void initState(){
        diaUI.SetActive(false);
    }


    void Start()
    {
        currentState = "rythm";
        initState();
    }

    private void Awake() {
        diaUI = GameObject.Find("diaUI");
    }

    
}
