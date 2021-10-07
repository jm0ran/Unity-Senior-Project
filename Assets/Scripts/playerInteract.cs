using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    public GameObject currentInterObj;
    public bool talks;
    public bool item;
    
    void renderSpeech(){
        //This will be what creates and renders the dialougue, want to seperate the speech function

        //Steps:

        //Enter a locked state where player cannot make any actions
        //Intend to take a list with strings of text split up for dialouge differences and cycle through waiting for the next keyprompt to continue
        //After cycling through speech, check if the interactable object has an item and if so give it to the player
        //Reenable player movement



    }

    void lockPlayer(bool state){
        //Will take true or false, will allow player movement to be locked by using send message to the 
    }

    void OnTriggerEnter2D(Collider2D other){ //Runs when there is a collision between a trigger and regular collider
        if (other.tag == "Interactable"){
            currentInterObj = other.gameObject; //Stores collided object assuming it is tagged "Interactable"
        }
    }
    void OnTriggerExit2D(Collider2D other){ //On exit with a trigger collision area
        if (other.tag == "Interactable" && other.gameObject == currentInterObj){
            currentInterObj = null; //Resets the object assuming it's equal to the current game object
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.E) && currentInterObj != null){ //Checks for keypress as well as that you are in range of an interactable object
            currentInterObj.SendMessage("printHi", "Hello");
        }
    }

    void Start(){
        Debug.Log("playerInteract Script Started");
    }
}
