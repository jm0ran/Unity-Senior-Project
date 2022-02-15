using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreController : MonoBehaviour
{

//------------------------------------------------------------------------
//PREDEFINED VARIABLES   
    private int notesHit;
    private int streak;
    private TMPro.TextMeshProUGUI streakText;
    private Slider progressSlider;
    private Image fillImage;
    private GameObject rythmStateController;

    public int startingAPOverride = 0;

    public static int actionPoints = 0;
    public static TextMeshProUGUI rActionPointDisplay;
    public static TextMeshProUGUI bActionPointDisplay;

//------------------------------------------------------------------------
//USER DEFINED FUNCTIONS
    public static void updateAP(){
        rActionPointDisplay.text = actionPoints.ToString();
        bActionPointDisplay.text = actionPoints.ToString();
 
    }
    void noteHit(float timeDiff){ //This is where I'm gonna handle note hits
        streak++;
        notesHit++;
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
            progressSlider.value = 0;
            actionPoints += 1;
            Debug.Log(actionPoints);
            updateAP();
        }else{
            fillImage.color = new Color(0, 0, 255);
        }
    }
    
    void triggerGauge(){
        progressSlider.value = 0;
        rythmStateController.SendMessage("enterActionState");
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        rActionPointDisplay = GameObject.Find("rythmActionPointCounter").GetComponent<TextMeshProUGUI>();
        bActionPointDisplay = GameObject.Find("battleActionPointCounter").GetComponent<TextMeshProUGUI>();
        streakText = GameObject.FindWithTag("streakCounter").GetComponent<TextMeshProUGUI>();
        progressSlider = GameObject.FindWithTag("progressSlider").GetComponent<Slider>();
        fillImage = GameObject.FindWithTag("sliderFill").GetComponent<Image>();
        rythmStateController = GameObject.FindWithTag("rythmStateController");
    }
    void Start(){
        streak = 0;
        notesHit = 0;
        if(startingAPOverride != 0){
            actionPoints = startingAPOverride;
        }
        updateAP();
    }
    void Update(){
        checkGauge();
        if(Input.GetKeyDown(KeyCode.Space) && actionPoints >= 1){
            triggerGauge();
        }
    }

}