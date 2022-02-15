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
        currentState = "action";
        audioControllerObj.SendMessage("isAction", true);
        triggerRemainingNotes();
        toggleRythmElements(false);
        toggleActionElements(true);
        //Should prob just make variables to store these references which I might do later
        GameObject.Find("enemyObject").GetComponent<SpriteRenderer>().enabled = true;
        scoreController.updateAP();        
    }

    void enterRythmState(){
        currentState = "rythm"; //I know I'm spelling it wrong idc I'm spelling it wrong consistently
        toggleActionElements(false);
        toggleRythmElements(true);
        GameObject.Find("enemyObject").GetComponent<SpriteRenderer>().enabled = false; //Should move this somewhere else but I am lazy
        scoreController.updateAP();
        audioControllerObj.SendMessage("pruneNotesFrom", audioController.mainSong.time + 3f);
        audioControllerObj.SendMessage("isAction", false);
    }
    
    void triggerRemainingNotes(){
        arrows = GameObject.FindGameObjectsWithTag("arrow");
        for(int i = 0; i < arrows.Length; i++){
            arrows[i].SendMessage("triggerNote");
        }
        arrows = null;
    }
    
    void toggleRythmElements(bool state){ //This is where I want to disable all the objects exclusive to the rythm stage
        for(int i = 0; i < arrowTargets.Length; i++){
            arrowTargets[i].SetActive(state);
        }
        rythmUI.SetActive(state);
    }

    void toggleActionElements(bool state){
        actionUI.SetActive(state);
        //Heres where I want to enable the Action UI
    }
   
    void Awake(){
        arrowTargets = GameObject.FindGameObjectsWithTag("arrowTarget");
        audioControllerObj = GameObject.FindWithTag("audioController");
        rythmUI = GameObject.FindWithTag("rythmUI");
        actionUI = GameObject.FindWithTag("actionUI");
    }

    void Start()
    {
        currentState = "rythm";
        startBattle();
    }

    
}
