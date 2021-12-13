using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleController : MonoBehaviour
{
    public GameObject flashingText;

    // Start is called before the first frame update
    void Start()
    {
        flashingText = GameObject.FindWithTag("flashingText");
        StartCoroutine(startFlashingText());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            startGame();
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
}
