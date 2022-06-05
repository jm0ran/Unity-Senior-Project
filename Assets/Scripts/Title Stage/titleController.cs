using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleController : MonoBehaviour
{

//The title controller is used for the title screen and like the cutscene controller is very independent and as a result isn't too complex

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    //Tons of references because I need duplicates for the infinite scrolling effect
    public GameObject flashingText; //Reference to flashing text
    private RectTransform groundA; //Reference to groundA
    private RectTransform groundB; //Reference to groundB
    public RectTransform leftGround; //Reference to leftGround
    public RectTransform rightGround; //Reference to rightGround
    private RectTransform cloudA; //Reference to cloudA
    private RectTransform cloudB; //Reference to cloudB
    private RectTransform leftCloud; //Reference to leftCloud
    private RectTransform rightCloud; //Reference to rightCloud

    private AudioSource inputSFX; //Reference to inputSFX

//------------------------------------------------------------------------
//User Defined Funtions
    IEnumerator groundSlide(){ //This coroutine slides the ground while looping
        leftGround = groundA; //Assigns left ground
        rightGround = groundB; //Assigns right ground
        RectTransform tempVar; //Assigns a temporary storage location
        while(true){ //Infinite loop
            if(rightGround.localPosition.x == 0){ //Once right ground hits x 0
                leftGround.localPosition = new Vector3(1987, 0, 0); //teleport left ground to new position
                //Swap left and right ground
                tempVar = leftGround; 
                leftGround = rightGround;
                rightGround = tempVar;
            }
            //Slide the ground
            groundA.localPosition = groundA.localPosition + new Vector3(-0.5f,0,0);
            groundB.localPosition = groundB.localPosition + new Vector3(-0.5f,0,0);
            yield return new WaitForSeconds(0.005f); //Timeout and repeat
        }
    }

    IEnumerator cloudSlide(){ //Slides clouds in a very similar way to ground
        leftCloud = cloudA; //Assigns left ground
        rightCloud = cloudB; //Assigns a right ground
        RectTransform tempVar; //Temporary storage location
        while(true){ //Infinite loop
            if(leftCloud.localPosition.x >= 0){ //Once left cloud hits x 0
                rightCloud.localPosition = new Vector3(-1987, 0, 0); //teleport right cloud to new position
                //Swap left and right clouds
                tempVar = leftCloud;
                leftCloud = rightCloud;
                rightCloud = tempVar;
            }
            //Slide the clouds
            cloudA.localPosition = cloudA.localPosition + new Vector3(0.02f,0,0);
            cloudB.localPosition = cloudB.localPosition + new Vector3(0.02f,0,0);
            yield return new WaitForSeconds(0.008f); //Timeout and repeat
        }
    }

    IEnumerator startFlashingText(){ //Flashes text
        while(true){ //Infinite loop
            flashingText.SetActive(false); //Disables text
            yield return new WaitForSeconds(0.75f); //Timeout
            flashingText.SetActive(true); //Enables text
            yield return new WaitForSeconds(0.75f); //Timeout and loop
        }
    }

    IEnumerator startGame(){
        //This is where I need to implement logic based on persist object to decide what scene to load into
        //Work backwards in terms of checking to go to the latest scene
        string targetScene = "Cutscene 1"; //Default if save data check returns all false
        if(saveDataController.globalSave.oneTimes[8]){ //If player beat drake fight, spawn them in Route 1 to explore
            targetScene = "Route 1";
        }else if(saveDataController.globalSave.oneTimes[1]){ //If starting dialogue in Junkyard has been triggered
            targetScene = "Junkyard";
        }else if(saveDataController.globalSave.oneTimes[0]){ //If opening dialogue has been triggered
            targetScene = "Junk Cave";
        }
        inputSFX.Play(); //Play input SFX
        yield return new WaitForSeconds(0.5f); //Pause
        SceneManager.LoadScene(targetScene); //Go to target scene
        yield return null; //Default return position
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        //Assigns necessary values
        inputSFX = gameObject.GetComponent<AudioSource>();
        flashingText = GameObject.FindWithTag("flashingText");
        groundA = GameObject.FindWithTag("groundA").GetComponent<RectTransform>();
        groundB = GameObject.FindWithTag("groundB").GetComponent<RectTransform>();
        cloudA = GameObject.FindWithTag("cloudA").GetComponent<RectTransform>();
        cloudB = GameObject.FindWithTag("cloudB").GetComponent<RectTransform>();
    }

    void Start()
    {
        //Starts all three animation loops
        StartCoroutine(startFlashingText());
        StartCoroutine(groundSlide());
        StartCoroutine(cloudSlide());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){ //If return key is pressed
            StartCoroutine(startGame()); //Start game on coroutine so sound effect has time to play
        }
    }
}