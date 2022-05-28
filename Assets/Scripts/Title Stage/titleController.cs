using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleController : MonoBehaviour
{
    public GameObject flashingText;
    private RectTransform groundA;
    private RectTransform groundB;
    public RectTransform leftGround;
    public RectTransform rightGround;

    private RectTransform cloudA;
    private RectTransform cloudB;
    private RectTransform leftCloud;
    private RectTransform rightCloud;

    IEnumerator groundSlide(){ //Works better then it did before
        leftGround = groundA;
        rightGround = groundB;
        RectTransform tempVar;
        while(true){
            if(rightGround.localPosition.x == 0){
                leftGround.localPosition = new Vector3(1987, 0, 0);
                tempVar = leftGround;
                leftGround = rightGround;
                rightGround = tempVar;
            }
            groundA.localPosition = groundA.localPosition + new Vector3(-0.5f,0,0);
            groundB.localPosition = groundB.localPosition + new Vector3(-0.5f,0,0);
            yield return new WaitForSeconds(0.005f);
        }
    }

    IEnumerator cloudSlide(){
        leftCloud = cloudA;
        rightCloud = cloudB;
        RectTransform tempVar;
        while(true){
            if(leftCloud.localPosition.x >= 0){
                rightCloud.localPosition = new Vector3(-1987, 0, 0);
                tempVar = leftCloud;
                leftCloud = rightCloud;
                rightCloud = tempVar;
            }
            cloudA.localPosition = cloudA.localPosition + new Vector3(0.02f,0,0);
            cloudB.localPosition = cloudB.localPosition + new Vector3(0.02f,0,0);
            yield return new WaitForSeconds(0.008f);
        }
    }

    IEnumerator startFlashingText(){
        while(true){
            flashingText.SetActive(false);
            yield return new WaitForSeconds(0.75f);
            flashingText.SetActive(true);
            yield return new WaitForSeconds(0.75f);
        }
    }

    void startGame(){
        //This is where I need to implement logic based on persist object to decide what scene to load into
        //Work backwards in terms of checking to go to the latest scene
        Debug.Log("Stage Transition");
        Debug.Log(saveDataController.globalSave.oneTimes[0]);
        string targetScene = "Cutscene 1"; //Default if save data check returns all false
       
        //Have to reverse this in a second
        if(saveDataController.globalSave.oneTimes[8]){ //If player beat drake fight, spawn them in Route 1 to explore
            targetScene = "Route 1";
        }else if(saveDataController.globalSave.oneTimes[1]){ //If starting dialogue in Junkyard has been triggered
            targetScene = "Junkyard";
        }else if(saveDataController.globalSave.oneTimes[0]){ //If opening dialogue has been triggered
            targetScene = "Junk Cave";
        }
        //There is already a default set which is cutscene 1
        
        SceneManager.LoadScene(targetScene);
    }





    void Start()
    {
        flashingText = GameObject.FindWithTag("flashingText");
        groundA = GameObject.FindWithTag("groundA").GetComponent<RectTransform>();
        groundB = GameObject.FindWithTag("groundB").GetComponent<RectTransform>();
        cloudA = GameObject.FindWithTag("cloudA").GetComponent<RectTransform>();
        cloudB = GameObject.FindWithTag("cloudB").GetComponent<RectTransform>();
        StartCoroutine(startFlashingText());
        StartCoroutine(groundSlide());
        StartCoroutine(cloudSlide());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            startGame();
        }else if(Input.GetKeyDown(KeyCode.M)){
            SceneManager.LoadScene("Battle Stage");
        }
    }
}