using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other){ //Runs when there is a collision between a trigger and regular collider
        Debug.Log("Collision");
        Debug.Log(other.tag);
    }
    void Start(){
        Debug.Log("playerInteract Script Started");
    }
}
