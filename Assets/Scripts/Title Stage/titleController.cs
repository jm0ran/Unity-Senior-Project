using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleController : MonoBehaviour
{
    public GameObject flashingText;
    private RectTransform sliding1;
    private RectTransform sliding2;
    private RectTransform frontBacking;
    private RectTransform backBacking;
    public RectTransform leftMost;
    public RectTransform rightMost;

    IEnumerator slideBacking(){ //Works better then it did before
        rightMost = sliding2;
        leftMost = sliding1;
        RectTransform tempVar;
        while(true){
            if(rightMost.localPosition.x == 0){
                leftMost.localPosition = new Vector3(1987, 0, 0);
                tempVar = leftMost;
                leftMost = rightMost;
                rightMost = tempVar;
            }
            sliding1.localPosition = sliding1.localPosition + new Vector3(-0.5f,0,0);
            sliding2.localPosition = sliding2.localPosition + new Vector3(-0.5f,0,0);
            yield return new WaitForSeconds(0.005f);
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
        sliding1 = GameObject.FindWithTag("sliding1").GetComponent<RectTransform>();
        sliding2 = GameObject.FindWithTag("sliding2").GetComponent<RectTransform>();
        StartCoroutine(startFlashingText());
        StartCoroutine(slideBacking());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            startGame();
        }
    }
}