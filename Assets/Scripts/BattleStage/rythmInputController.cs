using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmInputController : MonoBehaviour
{
//The rythm input controller exists on each of the arrow targets
//------------------------------------------------------------------------
//Pre Defined Functions
    private List<GameObject> currentNotes = new List<GameObject>(); //Storage for current notes
    public string direction; //Direction of arrow
    public Sprite invertedArrow; //Inverted arrow sprite
    public Sprite mainSprite; //Main arrow sprite
    public SpriteRenderer spriteRenderer; //Reference to sprite renderer
    public GameObject enemyController; //Reference to enemy controller

//------------------------------------------------------------------------
//User Defined Functions
    public void triggerCurrentNote(){ //Trigger the current note
        spriteRenderer.sprite = invertedArrow; //Invert arrow sprite to indicate action
        StartCoroutine(returnToSprite(0.25f)); //Trigger function to revert sprite in a fourth of a second
        if(currentNotes.Count > 0){ //The closest note to the player is going to be 0 in the array, besides for the possible edgecase of a passed note, but thats an edgecase to work on later
            GameObject contactNote = currentNotes[0]; //Grabs contact note
            currentNotes.RemoveAt(0); //Remove note from current notes
            float targetTime = contactNote.GetComponent<noteController>().triggerTime; //Checks target time of note
            float timeDiff = audioController.songTime - targetTime; //Calculates time difference
            enemyController.SendMessage("recieveHit"); //Sends notification of hit to enemy controller
            contactNote.SendMessage("triggerNote"); //Triggers the conraxted note
        }
    }

    IEnumerator returnToSprite(float delay){ //Returns sprite to normal
        yield return new WaitForSeconds(delay); //Wait for defined delay
        spriteRenderer.sprite = mainSprite; //Sets sprite back to normal
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        //Assigns necessary values
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyController = GameObject.Find("enemyObject");
    }

    void OnTriggerEnter2D(Collider2D other){ //Triggered on collisions
        if(other.tag == "arrow"){ //if collided with an arrow
            currentNotes.Add(other.gameObject); //Add arrow to current notes list
        }
    }

    void OnTriggerExit2D(Collider2D other){ //On collision exit
        if(other.tag == "arrow"){ //If arrow exited collision
            int toRemove = other.gameObject.GetInstanceID(); //Grab ID of arrow to remove
            for(int i = 0; i < currentNotes.Count; i++){ //Search through current notes to match
                if(currentNotes[i].GetInstanceID() == toRemove){ //Upon match
                    currentNotes.RemoveAt(i); //Remove note from current notes
                    //The player had now missed this note
                }
            }
        }
    }


    void Update(){   
        if(Input.GetKeyDown(direction)){ //If input of key corresponding to direction
            triggerCurrentNote(); //Trigger the current note
        }
    }
}
