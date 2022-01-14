using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmStateController : MonoBehaviour
{
    public string currentState;
    private GameObject audioController;
    private GameObject rythmInputController;
    private GameObject[] arrows;

    //This is where I want to control the state of the game and manage transitioning between the stages of battle and Rythm
    //States are going to include
    // -entry: Music and notes are both not currently active, not sure if or when I'll implement this state
    // -rythm: Music and notes are currently playing
    // -action: Music is still playing but notes are pause and player is met with a menu that will let them take action

    // Start is called before the first frame update
    void enterActionState(){
        audioController.SendMessage("prepAction");
        Debug.Log("Attempted to enter action state");
        triggerRemainingNotes();
    }
    
    void triggerRemainingNotes(){
        arrows = GameObject.FindGameObjectsWithTag("arrow");
        for(int i = 0; i < arrows.Length; i++){
            arrows[i].SendMessage("triggerNote");
        }
    }
   
   
    void Start()
    {
        currentState = "rythm";
        audioController = GameObject.FindWithTag("audioController");

    }
}
