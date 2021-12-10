using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreController : MonoBehaviour
{

//------------------------------------------------------------------------
//PREDEFINED VARIABLES   
    int notesHit;
    int streak;
    public TMPro.TextMeshProUGUI streakText;

//------------------------------------------------------------------------
//USER DEFINED FUNCTIONS
    void noteHit(float timeDiff){ //This is where I'm gonna handle note hits
        streak++;
        notesHit++;
        Debug.Log("Streak: " + streak);
        updateStreak();
    }
    void noteMiss(){
        Debug.Log("A Note Was Missed");
        streak = 0;
        updateStreak();
    }
    void updateStreak(){
        streakText.text = "Streak: " + streak;
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Start(){
        streak = 0;
        notesHit = 0;
        streakText = GameObject.FindWithTag("streakCounter").GetComponent<TMPro.TextMeshProUGUI>();
    }

}