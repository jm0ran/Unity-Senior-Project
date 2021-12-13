using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleController : MonoBehaviour
{
    public GameObject flashingText;
    public Transform backing1;
    public Transform backing2;

    IEnumerator slideBacking(){
        for(int i = 0; i < 200000; i++){
            backing1.position = backing1.position + new Vector3(0.5f,0,0);
            backing2.position = backing2.position + new Vector3(0.5f,0,0);
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
        backing1 = GameObject.FindWithTag("backing1").GetComponent<Transform>();
        backing2 = GameObject.FindWithTag("backing2").GetComponent<Transform>();
        StartCoroutine(startFlashingText());
        StartCoroutine(slideBacking());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            startGame();
        }
    }
}