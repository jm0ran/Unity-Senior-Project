using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    public GameObject currentInterObj;

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
        if(Input.GetKeyDown(KeyCode.E) && currentInterObj != null){ //Checks for 
            Debug.Log("Interacted with something");
        }
    }

    void Start(){
        Debug.Log("playerInteract Script Started");
    }
}
