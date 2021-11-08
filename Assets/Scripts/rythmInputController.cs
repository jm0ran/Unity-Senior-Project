using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmInputController : MonoBehaviour
{
    private List<GameObject> currentNotes = new List<GameObject>();



//User Defined Functions
    void triggerCurrentNote(){
        if(currentNotes.Count > 0){ //The closest note to the player is going to be 0 in the array, besides for the possible edgecase of a passed note, but thats an edgecase to work on later
            Destroy(currentNotes[0]);
            currentNotes.RemoveAt(0);
        }else{
            
        }
    }



//Unity Defined Functions
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "arrow"){
            currentNotes.Add(other.gameObject);
        }
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.UpArrow)){ //This will need to be changed to be dynamic for different directions
            triggerCurrentNote();
        }
    }
}
