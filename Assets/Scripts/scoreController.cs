using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreController : MonoBehaviour
{

//PREDEFINED VARIABLES   
    int notesHit;
    int streak;


//USER DEFINED FUNCTIONS
    void noteHit(float timeDiff){ //This is where I'm gonna handle note hits
        streak++;
        notesHit++;
        Debug.Log("Streak: " + streak);
    }
    void noteMiss(){
        Debug.Log("A Note Was Missed");
        streak = 0;
    }


//Unity Defined Functions
    void Start(){
        streak = 0;
        notesHit = 0;
    }

}