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
        SceneManager.LoadScene("Cutscene 1");
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
        }
    }
}