using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmStateController : MonoBehaviour
{
    static public string currentState;
    private GameObject audioController;
    private GameObject rythmInputController;
    private GameObject[] arrows;
    private GameObject [] arrowTargets;
    private GameObject actionUI;
    private GameObject rythmUI;

    //This is where I want to control the state of the game and manage transitioning between the stages of battle and Rythm
    //States are going to include
    // -entry: Music and notes are both not currently active, not sure if or when I'll implement this state
    // -rythm: Music and notes are currently playing
    // -action: Music is still playing but notes are pause and player is met with a menu that will let them take action

    // Start is called before the first frame update

    void startBattle(){
        actionUI.SetActive(false);
        //Temporarily starting in action state while I work in it
        //enterActionState();
    }

    void enterActionState(){
        audioController.SendMessage("prepAction");
        triggerRemainingNotes();
        toggleRythmElements(false);
        toggleActionElements(true);
        currentState = "action";
    }
    
    void triggerRemainingNotes(){
        arrows = GameObject.FindGameObjectsWithTag("arrow");
        for(int i = 0; i < arrows.Length; i++){
            arrows[i].SendMessage("triggerNote");
        }
        arrows = null;
    }
    
    void toggleRythmElements(bool state){ //This is where I want to disable all the objects exclusive to the rythm stage
        arrowTargets = GameObject.FindGameObjectsWithTag("arrowTarget");
        for(int i = 0; i < arrowTargets.Length; i++){
            arrowTargets[i].SetActive(state);
        }
        rythmUI.SetActive(state);
    }

    void toggleActionElements(bool state){
        actionUI.SetActive(state);
        //Heres where I want to enable the Action UI
    }
   
   
    void Start()
    {
        currentState = "rythm";
        audioController = GameObject.FindWithTag("audioController");
        rythmUI = GameObject.FindWithTag("rythmUI");
        actionUI = GameObject.FindWithTag("actionUI");
        startBattle();

    }
}
