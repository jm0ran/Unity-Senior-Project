using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class cutsceneController : MonoBehaviour
{
    public TextMeshProUGUI sceneText;
    public float textDelay;
    public bool locked; //Used to lock checking to move forward while new text is loading

    //I want this this first cutscene controller to almost serve as a template as while I only plan to have this opening cutscene plans could definitely change, going to hardcode the information in slightly


    void renderText(string input){ //I want to keep render text in it's own function as I intend to do some kind of timed animation as the characters come in, I also want to containerize this segmenet as I may use it later
        locked = true;
        StartCoroutine(charByChar(input));
    }

    IEnumerator charByChar(string input){
        string toRender = "";
        for(int i = 0; i < input.Length; i++){
            toRender += input[i];
            sceneText.text = toRender;
            yield return new WaitForSeconds(0.05f);
        }
        locked = false;
    }
       
    IEnumerator mainLoop(){ //This is the main loop which just kind of defines the flow of everything, is going to be very hard coded but thats kind of what I'm going for right now, ordered like this so I can trigger other functions within the progression which I would be unable to do in a for loop
        //Every one of these while loops will pause the functions progression to create a pathway for the dialogue to follow
        renderText("Detroit Michigan");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Kanye: What are you doing, aren’t you supposed to be back in Canada?!");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Unknown Male: Plans changed…");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Unknown Male: Little old me has a little old job to do Ye");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Unknown Male: We no longer have a need for you");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Kanye: We?!");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Unknown Female: It’s time to end this Bad Blood Kanye");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("On July 21st, 2022, Kanye West dissapeared.");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        renderText("Almost a decade later many have forgotten about Ye, but not all have given up hope…");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        SceneManager.LoadScene("Junk Cave");
    }
    


    //---------------------------------------------------------------------------------
    //Unity Defined Functions
    void Start()
    {
        sceneText = GameObject.FindWithTag("textBox").GetComponent<TextMeshProUGUI>();
        StartCoroutine(mainLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
