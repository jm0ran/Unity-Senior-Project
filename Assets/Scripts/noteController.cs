using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noteController : MonoBehaviour
{
    //Predefined Variables
    public float triggerTime;
    public string button;
    public Rigidbody2D rb;
    public float speed = 1;

    //User Defined Functions
    public void moveNote(){
        if(rb.position.x < -7.5){
            Destroy(gameObject);
        }
        //rb.MovePosition(new Vector2(rb.position.x - 0.1f,rb.position.y) * Time.deltaTime);
    }



    //Unity Defined Functions\
    public void FixedUpdate(){
        moveNote();
    }
    public void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update(){
        transform.Translate(Vector3.left * (10.5f / 3f) * Time.deltaTime);
    }


}
