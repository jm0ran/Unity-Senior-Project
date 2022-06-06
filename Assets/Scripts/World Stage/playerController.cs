using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

//The player controller script is all about player behavior and player movement

//------------------------------------------------------------------------
//Main Variables used in the Script
    public float moveSpeed = 3f; //Used to control the player's movement speed
    public bool turnLocked; //Condition can be used to block movement
    public Rigidbody2D rb; //Allows script access to objects rigid body
    public Animator animator; //Allows script access to animator
    public Vector2 movement; //Movement storage
    public string lastKey; //String to store the last key you pressed
    public float totSpeed; //total speed storage
    public int lastDirection; //This variable will be used for storing the last direction of the player, up:1, down:2, right:3, left:4
    public Vector2 continuedDirection; //Continue direction storage
    public bool continuedMovement = false; //Continue movement state
    //Used for interaction
    public GameObject currentInterObj; //Store current interaction object
    public bool locked = false; //State for locked player
    //Used for passing Camera values
    public GameObject camObj; //Reference to camera object
    public Camera cam; //Reference to camera component
    public AudioSource walkingSFX; //Reference to walking SFX

//------------------------------------------------------------------------
//Main User Defined Functions used in the Script
    void triggerInteract(){ //This is how I am going to handle player actions with the new interact system
        if(!locked && currentInterObj != null){ //If not locked and there is a currentInterObj picked up from collisions
            switch (currentInterObj.tag){ //Switch based off tag
                case "betterNPC": //if betterNPC
                    currentInterObj.SendMessage("startDia"); //Start dialogue
                    break;
                case "chest": //if chest
                    locked = true; //Lock player
                    currentInterObj.SendMessage("openChest"); //Open chest
                    animator.SetBool("collecting", true); //Set animation to collection
                    animator.SetTrigger("collect"); //Set animation trigger to colelct
                    lastDirection = 2; //Set last direction to 2 (down)
                    StartCoroutine(finishCollect(2)); //Finish collection
                    break;
            }
        }
    }

     IEnumerator finishCollect(float seconds){ //Finish collection process
        yield return new WaitForSeconds(seconds); //Wait delay and continue
        animator.SetBool("collecting", false); //Set collecting back to false
        locked = false; //Unlock player movement
    }

    void continueInDirection(string direction){ //Function to lock player into a direction during movement to emulate old pokemon games
        switch(direction){ //Switch statement based off direction
        //Depending on direction set a continued movement direction
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
        continuedMovement = true; //set continued movement to true to lock player in direction
    }

    void updateAnimation(){ //This is where all my animation logic is stored and managed I hate animation controller thingy dumb stupid thing
        if(locked && !continuedMovement){ //If player is locked and not continuing movement
            //Set all movement values to zero
            animator.SetFloat("horizontalSpeed", 0);
            animator.SetFloat("verticalSpeed", 0);
            animator.SetFloat("totSpeed", 0);
            walkingSFX.Pause(); //Pause walking sound effect
        }else if(continuedMovement){ //If moving continuosly
        //Set animation values to the continued direction values determined in previous function
            animator.SetFloat("horizontalSpeed", continuedDirection.x);
            animator.SetFloat("verticalSpeed", continuedDirection.y);
            animator.SetFloat("totSpeed",   continuedDirection.x + continuedDirection.y);
        }else{ //If not locked and not moving
            //Based on movement set last direction so when player
            if(movement.x > 0){
                lastDirection = 3;
            }else if(movement.x < 0){
                lastDirection = 4;
            }else if (movement.y > 0){
                lastDirection = 1;
            }else  if(movement.y < 0){
                lastDirection = 2;
            }
            //If player is not locked
            if(!locked){
                //Update animator values based on movements
                animator.SetFloat("horizontalSpeed", movement.x);
                animator.SetFloat("verticalSpeed", movement.y);
                animator.SetFloat("totSpeed", movement.x + movement.y);
                animator.SetInteger("lastDirection", lastDirection);
            }
        }
    }

    void checkMovement() //Main movement logic called by the update function, over complicated to prevent 2 inputs at same time, this function is massive, I might want to split it up later on 
    {
        if(!locked && !continuedMovement){ //If not locked and not in continuos movement
            movement = new Vector2(0,0); //Player is not moving    
            //This is the entrance point for movement, the system is designed so you can only move in one direction at a time
            if(!turnLocked){ //if not turn locked
                //Set lastKey based on first button pressed as this is entry point to movement
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
                    turnLocked = true; //Enable turn lock
                    walkingSFX.Play(); //Start walkingSFX
                }
            }
            //Triggered once a key has been held down, just lets you continue in that general direction
            else{ //If fails previous check
                if(Input.GetKey(lastKey)){ //If input is the same as last key
                    //Continue movement as assign values to movement storage
                    if(lastKey == "up"){
                        movement.y = 1;
                    }else if(lastKey == "down"){
                        movement.y = -1;
                    }else if(lastKey == "left"){
                        movement.x = -1; 
                    }else if(lastKey == "right"){
                        movement.x = 1;
                    }
                
                }else{ //If not the last key
                    turnLocked = false; //Diable turn locked
                    walkingSFX.Pause(); //Stop the walking sound effect
                }
            }
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //Update player position
        }
        else if(continuedMovement){ //Continued movement used for the door controller 
            rb.MovePosition(rb.position + continuedDirection * moveSpeed * Time.fixedDeltaTime); //Continue movement for door
        }
    }

    void lockPlayer(bool state){ //Locks player movement, used when triggering dialougue and other events that player needs to be frozen for
        gameObject.GetComponent<inputController>().playerLocked = state; //Update player locked status
        locked = state; //Update state
        //Set animator values back to 0
        animator.SetFloat("horizontalSpeed", 0);
        animator.SetFloat("verticalSpeed", 0);
        animator.SetFloat("totSpeed", 0);
    }

//------------------------------------------------------------------------
//Main Unity Functions
    //Slighly different from start but also runs before the game starts
    void Awake(){
        //Assigns the appropriate values
        animator = GetComponent<Animator>();
        camObj = GameObject.FindWithTag("MainCamera");
        cam = camObj.GetComponent<Camera>();
        walkingSFX = GameObject.Find("walkingSoundEffect").GetComponent<AudioSource>();
    }

    void FixedUpdate(){
        //Checks movement and updates animations 60 times a second
        checkMovement();
        updateAnimation();
    }

    void OnTriggerEnter2D(Collider2D other){ //Runs when there is a collision between a trigger and regular collider
        //Store collided object in currentInterObj
        if(other.tag == "betterNPC"){
            currentInterObj = other.gameObject; //Stores collided object assuming it is tagged "Interactable"
        }else if(other.tag == "chest"){
            currentInterObj = other.gameObject;
        }else if(other.tag == "doorBlocker"){
            other.gameObject.SendMessage("recievePlayer"); //Recieve player on collision with door blocker
        }
        if (other.tag == "door"){ //If collision with door
            locked = true; //Lock player control
            continueInDirection(other.GetComponent<doorController>().direction); //Continue movement through door
            //pass camera size and aspect ratio to next scene
            sceneController.camSize = cam.orthographicSize;
            sceneController.camAspect = cam.aspect;
            other.gameObject.SendMessage("nextScene"); //Send message to load next scene
        }
    }

    void OnTriggerExit2D(Collider2D other){ //On exit with a trigger collision area
        if (other.gameObject == currentInterObj){ //If the exit is equal to currentInterObj
            currentInterObj = null; //Resets the object assuming it's equal to the current game object
        }
    }
}

