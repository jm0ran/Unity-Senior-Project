using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteController : MonoBehaviour
{
    //Predefined Variables
    public float triggerTime;
    public string button;
    public Rigidbody2D rb;

    //User Defined Functions
    public void moveNote(){
        rb.MovePosition(new Vector2(rb.position.x - 0.1f,rb.position.y));
    }



    //Unity Defined Functions\
    public void FixedUpdate(){
        moveNote();
    }
    public void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }


}
