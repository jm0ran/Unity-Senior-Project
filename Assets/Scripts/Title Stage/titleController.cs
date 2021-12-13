using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleController : MonoBehaviour
{
    public GameObject flashingText;
    private RectTransform backing1;
    private RectTransform backing2;
    private RectTransform backing3;
    public RectTransform backBacking;
    public RectTransform midBacking;
    public RectTransform frontBacking;
    public RectTransform backOfTheLine;


    IEnumerator slideBacking(){ //Idk how I got this to work lol
        backBacking = backing1;
        midBacking = backing2;
        frontBacking = backing3;
        backOfTheLine = backing1;
        while(true){
            if(backBacking.position.x == 0){
                frontBacking.position = new Vector3(-1987, frontBacking.position.y, frontBacking.position.z);
                backBacking = frontBacking;
                frontBacking = midBacking;
                midBacking = backOfTheLine;
                backOfTheLine = backBacking;
            }
            backing1.position = backing1.position + new Vector3(0.5f,0,0);
            backing2.position = backing2.position + new Vector3(0.5f,0,0);
            backing3.position = backing3.position + new Vector3(0.5f,0,0);
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
        backing1 = GameObject.FindWithTag("backing1").GetComponent<RectTransform>();
        backing2 = GameObject.FindWithTag("backing2").GetComponent<RectTransform>();
        backing3 = GameObject.FindWithTag("backing3").GetComponent<RectTransform>();
        StartCoroutine(startFlashingText());
        StartCoroutine(slideBacking());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            startGame();
        }
    }
}