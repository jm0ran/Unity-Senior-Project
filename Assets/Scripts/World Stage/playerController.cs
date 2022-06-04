using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

//------------------------------------------------------------------------
//Main Variables used in the Script
    public float moveSpeed = 3f; //Used to control the player's movement speed
    public bool turnLocked; //Condition can be used to block movement
    public Rigidbody2D rb; //Allows script access to objects rigid body
    public Animator animator; //Allows script access to animator
    public Vector2 movement;
    public string lastKey; //String to store the last key you pressed
    public float totSpeed;
    public int lastDirection; //This variable will be used for storing the last direction of the player, up:1, down:2, right:3, left:4
    public Vector2 continuedDirection;
    public bool continuedMovement = false;

    //Used for interaction
    public GameObject currentInterObj;
    public bool locked = false;

    //Used for passing Camera values
    public GameObject camObj;
    public Camera cam;

    //Walking SFX
    public AudioSource walkingSFX;

//------------------------------------------------------------------------
//Main User Defined Functions used in the Script
    void triggerInteract(){ //This is how I am going to handle player actions with the new interact system
        if(!locked && currentInterObj != null){
            switch (currentInterObj.tag)
            {
                case "betterNPC":
                    currentInterObj.SendMessage("startDia");
                    break;
                case "chest":
                    locked = true;
                    currentInterObj.SendMessage("openChest");
                    animator.SetBool("collecting", true);
                    animator.SetTrigger("collect");
                    lastDirection = 2;
                    StartCoroutine(finishCollect(2));
                    //Need to add a timeout that re disables chest case
                    //Temporarily Going to Implement Collect animation here to get it working
                    break;
            }
        }
    }

     IEnumerator finishCollect(float seconds){ //Might want to make this into an anonymous function or whatever at some point
        yield return new WaitForSeconds(seconds);
        animator.SetBool("collecting", false);
        locked = false;
    }

    void continueInDirection(string direction){ //Need to implement animation stuff with all of these because it will bite me later
        switch(direction){
            case "left":
                continuedDirection = new Vector2(-1, 0);
                break;
            case "right":
                continuedDirection = new Vector2(1, 0);
                break;
            case "down":
                continuedDirection = new Vector2(0, -1);
                break;
            case "up":
                continuedDirection = new Vector2(0, 1);
                break;
            case "na":
                continuedDirection = new Vector2(0,0);
                break;
        }
        continuedMovement = true;
    }

    void updateAnimation(){ //This is where all my animation logic is stored and managed I hate animation controller thingy dumb stupid thing
        if(locked && !continuedMovement){
            Debug.Log("Not Moving");
            animator.SetFloat("horizontalSpeed", 0);
            animator.SetFloat("verticalSpeed", 0);
            animator.SetFloat("totSpeed", 0);
            walkingSFX.Pause();
        }else if(continuedMovement){
            animator.SetFloat("horizontalSpeed", continuedDirection.x);
            animator.SetFloat("verticalSpeed", continuedDirection.y);
            animator.SetFloat("totSpeed",   continuedDirection.x + continuedDirection.y);
        }else{ //If not locked
            if(movement.x > 0){
                lastDirection = 3;
            }else if(movement.x < 0){
                lastDirection = 4;
            }else if (movement.y > 0){
                lastDirection = 1;
            }else  if(movement.y < 0){
                lastDirection = 2;
            }
            if(!locked){
                animator.SetFloat("horizontalSpeed", movement.x);
                animator.SetFloat("verticalSpeed", movement.y);
                animator.SetFloat("totSpeed", movement.x + movement.y);
                animator.SetInteger("lastDirection", lastDirection);
            }
        }

    }


    void checkMovement() //Main movement logic called by the update function, over complicated to prevent 2 inputs at same time, this function is massive, I might want to split it up later on 
    {
        if(!locked && !continuedMovement){
            //I want to be careful of this code here, dont want it to cause jitters, it should but idk
            movement = new Vector2(0,0);         
            //This is the entrance point for movement, the system is designed so you can only move in one direction at a time
            if(!turnLocked){
                if(Input.GetKey("up")){
                    lastKey = "up";
                }else if(Input.GetKey("down")){
                    lastKey = "down";
                }else if(Input.GetKey("left")){
                    lastKey = "left";
                }else if(Input.GetKey("right")){
                    lastKey = "right";
                }
                //Locks turning
                if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right")){
                    turnLocked = true;
                    walkingSFX.Play();
                }
            }
            //Triggered once a key has been held down, just lets you continue in that general direction
            else{
                if(Input.GetKey(lastKey)){
                    Debug.Log("IS Moving");
                    if(lastKey == "up"){
                        movement.y = 1;
                    }else if(lastKey == "down"){
                        movement.y = -1;
                    }else if(lastKey == "left"){
                        movement.x = -1; 
                    }else if(lastKey == "right"){
                        movement.x = 1;
                    }
                
                }else{
                    turnLocked = false;
                    walkingSFX.Pause();
                }
            }
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        else if(continuedMovement){ //Continued movement used for the door controller 
            rb.MovePosition(rb.position + continuedDirection * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void lockPlayer(bool state){ //Locks player movement, used when triggering dialougue and other events that player needs to be frozen for
        gameObject.GetComponent<inputController>().playerLocked = state;
        locked = state;
        animator.SetFloat("horizontalSpeed", 0);
        animator.SetFloat("verticalSpeed", 0);
        animator.SetFloat("totSpeed", 0);
    }

//------------------------------------------------------------------------
//Main Unity Functions
    //Slighly different from start but also runs before the game starts
    void Awake(){
        animator = GetComponent<Animator>(); //Grabs the animator component from the object that this script is attached to
        camObj = GameObject.FindWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();
        walkingSFX = GameObject.Find("walkingSoundEffect").GetComponent<AudioSource>();
    }

    void FixedUpdate(){
        checkMovement(); //Movement controller is kept in fixed update to keep movespeed consistent, prob gonna implement physics based movement instead of micro teleports at one point but yeah
        updateAnimation();
    }

    void OnTriggerEnter2D(Collider2D other){ //Runs when there is a collision between a trigger and regular collider
        if (other.tag == "betterNPC"){
            currentInterObj = other.gameObject; //Stores collided object assuming it is tagged "Interactable"
        }else if(other.tag == "chest"){
            currentInterObj = other.gameObject;
        }else if(other.tag == "doorBlocker"){
            other.gameObject.SendMessage("recievePlayer");
        }
        if (other.tag == "door"){
            locked = true;
            continueInDirection(other.GetComponent<doorController>().direction);
            sceneController.camSize = cam.orthographicSize;
            sceneController.camAspect = cam.aspect;
            other.gameObject.SendMessage("nextScene");
        }

    }

    void OnTriggerExit2D(Collider2D other){ //On exit with a trigger collision area
        if (/*other.tag == "Interactable" && */other.gameObject == currentInterObj){
            currentInterObj = null; //Resets the object assuming it's equal to the current game object
        }
    }
}

