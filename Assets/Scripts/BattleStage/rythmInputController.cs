using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmInputController : MonoBehaviour
{
//------------------------------------------------------------------------
//Pre Defined Functions
    private List<GameObject> currentNotes = new List<GameObject>();
    
    //Define in Unity
    public string direction;
    public Sprite invertedArrow;
    public Sprite mainSprite;
    public SpriteRenderer spriteRenderer;
    public GameObject enemyController;


//------------------------------------------------------------------------
//User Defined Functions
    void triggerCurrentNote(){ 
        spriteRenderer.sprite = invertedArrow;
        StartCoroutine(returnToSprite(0.25f));
        if(currentNotes.Count > 0){ //The closest note to the player is going to be 0 in the array, besides for the possible edgecase of a passed note, but thats an edgecase to work on later
            GameObject contactNote = currentNotes[0];
            currentNotes.RemoveAt(0);
            float targetTime = contactNote.GetComponent<noteController>().triggerTime;
            float timeDiff = audioController.songTime - targetTime;
            enemyController.SendMessage("recieveHit");
            contactNote.SendMessage("triggerNote");
        }else{
            
        }
    }

    IEnumerator returnToSprite(float delay){
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = mainSprite;
    }

    

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        enemyController = GameObject.Find("enemyObject");
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "arrow"){
            currentNotes.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.tag == "arrow"){
            int toRemove = other.gameObject.GetInstanceID();
            for(int i = 0; i < currentNotes.Count; i++){
                if(currentNotes[i].GetInstanceID() == toRemove){
                    //This is where I handle a note miss and where I'm going to call my score controller
                    currentNotes.RemoveAt(i);
                    Debug.Log("Missed a note");
                }
            }
        }
    }


    void Update(){
        if(rythmStateController.currentState == "rythm"){ //Allowable inputs during rythm state
            if(Input.GetKeyDown(direction)){ //I made it work I forgot how lol
                triggerCurrentNote();
            }
        }
    }
}
