using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreController : MonoBehaviour
{

//------------------------------------------------------------------------
//PREDEFINED VARIABLES   
    int notesHit;
    int streak;
    public TMPro.TextMeshProUGUI streakText;
    public Slider progressSlider;
    public Image fillImage;

//------------------------------------------------------------------------
//USER DEFINED FUNCTIONS
    void noteHit(float timeDiff){ //This is where I'm gonna handle note hits
        streak++;
        notesHit++;
        Debug.Log("Streak: " + streak);
        updateStreak();
        gaugeUp();
    }
    void noteMiss(){
        Debug.Log("A Note Was Missed");
        streak = 0;
        updateStreak();
    }
    void updateStreak(){
        streakText.text = "Streak: " + streak;
    }
    void gaugeUp(){
        if(progressSlider.value < 1f){
            progressSlider.value += 0.1f;
        }
    }
    void checkGauge(){
        if(progressSlider.value >= 1){
            fillImage.color = new Color(255, 255, 255, 100);
        }
    }
    
    void triggerGauge(){
        progressSlider.value = 0;
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Start(){
        streak = 0;
        notesHit = 0;
        streakText = GameObject.FindWithTag("streakCounter").GetComponent<TMPro.TextMeshProUGUI>();
        progressSlider = GameObject.FindWithTag("progressSlider").GetComponent<Slider>();
        fillImage = GameObject.FindWithTag("sliderFill").GetComponent<Image>();
    }
    void Update(){
        checkGauge();
        if(Input.GetKeyDown(KeyCode.Space) && progressSlider.value >= 1){
            triggerGauge();
        }
    }

}