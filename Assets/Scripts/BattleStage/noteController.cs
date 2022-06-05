using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteController : MonoBehaviour
{
//Every note uses an instance of the NoteController for it's movement and triggering
//------------------------------------------------------------------------
//Predefined Variables
    public float triggerTime; //Time for note to trigger
    public string button; //Type of button note is
    public Rigidbody2D rb; //Reference to note rigid body for physics
    public float speed = 1; //Speed of note
    public float timeToTarget; //Time expected to target
    public float trackDistance; //Distance of track
    public bool isMoving = true; //Bool for movement

    //Sprites for the arrows, Assined in unity
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite rightSprite;
    public Sprite leftSprite;
    
    public SpriteRenderer spriteRenderer;//Reference to sprite renderer

//------------------------------------------------------------------------
//User Defined Functions
    public void moveNote(){ //Function for moving note
        if(rb.position.x < -7.5){ //If note has passed x cutoff
            Destroy(gameObject); //Destroy note
        }
    }

    public void triggerNote(){ //Function to trigger note
        StartCoroutine(delayDestroy()); //Start the delay destroy coroutine
    }


    IEnumerator delayDestroy(){ //This is the function I use to delay the destruction of the note a small amount while also pausing movement of note
        isMoving = false; //Disable isMoving bool
        yield return new WaitForSeconds(0.1f); //Wait for a tenth of a seconds
        Destroy(gameObject); //Destroy the note
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    public void FixedUpdate(){ //Fixed update runs 60 times a second for consistent physics
        moveNote(); //Runs moveNote function
    }

    public void Awake(){
        //Assigns necessary values
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    public void Start(){
        //Case statement to set the correct sprite for the object based on button argument
        switch(button){
            case "up":
                spriteRenderer.sprite = upSprite;
                break;
            case "down":
                spriteRenderer.sprite = downSprite;
                break;
            case "right":
                spriteRenderer.sprite = rightSprite;
                break;
            case "left":
                spriteRenderer.sprite = leftSprite;
                break;
        }
    }

    void Update(){ //Update function to manage exact moevemtn of note
        if(isMoving){ //If is moving
            transform.Translate(Vector3.left * (trackDistance / timeToTarget) * Time.deltaTime); //Uses Time.deltaTime to level movement speed
        }
    }


}
