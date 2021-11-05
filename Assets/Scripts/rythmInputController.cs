using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rythmInputController : MonoBehaviour
{
    private List<GameObject> currentNotes = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "arrow"){
            currentNotes.Add(other.gameObject);
            Debug.Log(currentNotes.Count);
        }
    }
}
