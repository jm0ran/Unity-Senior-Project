using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float moveSpeed = 3f; //Used to control the player's movement speed
    public bool turnLocked; //Condition can be used to block movement
    public Rigidbody2D rb; //Allows script access to objects rigid body
    public Animator animator; //Allows script access to animator
    public Vector2 movement;
    public string lastKey; //String to store the last key you pressed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Fixed refresh time seperate from framerate
    void FixedUpdate()
    {
        checkMovement();
    }

    //Move function accepts different directions
    void checkMovement()
    {
        //I want to be careful of this code here, dont want it to cause jitters, it should but idk
        movement.y = 0;
        movement.x = 0;

        //Heres the basic movement functions, very simple move controller, I want to lock into one direction, but am going to work with animator first
        
        //This is the entrance point for movement, the system is designed so you can only move in one direction at a time
        if(!turnLocked){
            if(Input.GetKey("up"))
            {
                movement.y = 1;
                lastKey = "up";
            }else if(Input.GetKey("down"))
            {
                movement.y = -1;
                lastKey = "down";
            }else if(Input.GetKey("left"))
            {
                movement.x = -1;
                lastKey = "left";
            }else if(Input.GetKey("right"))
            {
                movement.x = 1;
                lastKey = "right";
            }

            //Locks turning
            if (Input.GetKey("up") || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right")){
                turnLocked = true;
            }
        }
        //Triggered once a key has been held down, just lets you continue in that general direction
        else{
            if(Input.GetKey(lastKey)){
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
            }
        }
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
    }
}

