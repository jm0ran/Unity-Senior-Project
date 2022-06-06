using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class cutsceneController : MonoBehaviour
{
//The Cutscene controller is the singular controller for the cutscene and is very straightforward as it doesn't really interact with any other scripts

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public TextMeshProUGUI sceneText; //Reference to scene text
    public float textDelay; //Delay for text
    public bool locked; //Used to lock checking to move forward while new text is loading
    public GameObject fadeShade; //Reference to fadeShade
    public Image cutsceneImage; //Reference to cutscene image
    public AudioSource inputSFX; //Reference to input SFX

    //Storage for cutscene images
    public Sprite scene0;
    public Sprite scene1;
    public Sprite scene2;
    public Sprite scene3;
    public Sprite scene4;

//------------------------------------------------------------------------
//User Defined Functions

    void renderText(string input){ //This renders the text by riggereding the charByChar coroutine to have it load progressively
        locked = true; //Locks while text is loading
        StartCoroutine(charByChar(input)); //Calls charByChar
    }

    IEnumerator fadeTransition(string dest){ //Fade transition out of scene
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>(); //Reference to canvasGroup for fade
        float alpha = 0.0f; //Default alpha of 0 (Not Visible)
        while (alpha < 1f){ //While alpha (opacity) is less than 1
            alpha += 0.01f; //Increase opacity
            canvasGroup.alpha = alpha; //Update opacity
            yield return new WaitForSeconds(0.025f); //Time out and then repeat
        }
        sceneController.origin = "Cutscene 1"; //Set origin so game knows where player is coming from
        SceneManager.LoadScene(dest); //Load destination scene (Junk Cave)
    }

    IEnumerator charByChar(string input){ //Coroutine to load in each char individually
        string toRender = ""; //Store progressively increasing text
        for(int i = 0; i < input.Length; i++){ //For each character in the passed string
            toRender += input[i]; //Add character to render string
            sceneText.text = toRender; //Update with render string
            yield return new WaitForSeconds(0.05f); //Time out and loop
        }
        locked = false; //Unlock input after completion
    }
       
    IEnumerator mainLoop(){ //This is the main loop that defines the path of the cutscene
        //Every one of these while loops will pause the functions progression to create a pathway for the dialogue to follow
        //I have comments on the first one, then the other ones are self explanatory
        inputSFX.Play(); //Play sound effect
        renderText("July 21, 2022"); //Render text
        yield return new WaitForSeconds(textDelay); //Delay
        while(!Input.GetKeyDown(KeyCode.Return) || locked){ //Return Gate
            yield return null;
        }
        
        inputSFX.Play();
        renderText("Detroit Michigan");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        cutsceneImage.sprite = scene1; //Move to next cutscene image
        renderText("Kanye: What are you doing, aren’t you supposed to be back in Canada?!");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        cutsceneImage.sprite = scene2; //Move to next cutscene image
        renderText("Drake: Plans changed…");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        renderText("Drake: Little old me has a little old job to do Ye");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        renderText("Drake: We no longer have a need for you");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        cutsceneImage.sprite = scene3; //Move to next cutscene image
        renderText("Kanye: We?!");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        cutsceneImage.sprite = scene4; //Move to next cutscene image
        renderText("Taylor: It’s time to end this Bad Blood Kanye");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }

        inputSFX.Play();
        cutsceneImage.sprite = scene0; //Move to next cutscene image
        renderText("On July 21st, 2022, Kanye West dissapeared.");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        
        inputSFX.Play();
        cutsceneImage.sprite = scene0; //Move to next cutscene image
        renderText("Almost a decade later many have forgotten about Ye, but not all have given up hope…");
        yield return new WaitForSeconds(textDelay);
        while(!Input.GetKeyDown(KeyCode.Return) || locked){
            yield return null;
        }
        StartCoroutine(fadeTransition("Junk Cave")); //Start the fade transition to junkCave
    }

    void InitSaveData(){ //Function to initialize save data so it is formatted correctly for the game
        Save targetSave; //instantiates save class
        targetSave = saveDataController.globalSave; //Reference to global save
        targetSave.oneTimes = new List<bool>(){ //Assigns one times
            false, //0 Opening Diaogue in Junk Cave
            false, //1 Opening Dialogue in Junkyard
            false, //2 MBDTF chest
            false, //3 initial time speaking to Ned in junkyard
            false, //4 Yeezy chest
            false, //5 Yeezy Dialogue Complete and ned is gone
            //--------------------------------------------------------------
            //First short section of one times done
            false, //6 Kanye summoned trigger
            false, //7 Initial encounter with drake fight
            false, //8 Completion of drake fight <-- Want to spawn player in 
        };
        targetSave.inventory.items = new List<Item>(){ //Assigns inventory
            new Item("Old Yeezy", 1)
        };
        targetSave.serializeSaveData(); //Serialize changes
    }
    


    //---------------------------------------------------------------------------------
    //Unity Defined Functions
    void Awake()
    {
        //Assign necessary values
        sceneText = GameObject.FindWithTag("textBox").GetComponent<TextMeshProUGUI>();
        fadeShade = GameObject.FindWithTag("fadeShade");
        cutsceneImage = GameObject.Find("cutsceneImage").GetComponent<Image>();
        inputSFX = gameObject.GetComponent<AudioSource>();

        
    }

    void Start(){
        InitSaveData(); //Initialize save data
        StartCoroutine(mainLoop()); //Start main loop
    }

}
