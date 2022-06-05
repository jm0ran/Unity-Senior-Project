using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class audioController : MonoBehaviour
{
//Audio Controller object that controls note spawning and some general game logic
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public static float songTime = 0; //Variable to store song time
    public static AudioSource mainSong; //Reference to mainSong AudioSource holding audio Clip
    public GameObject arrowPrefab; //Refence to arrow prefab
    public beatMap mainMap; //Reference to mainMap of beatMap class
    public float delayStart; //Amount to delay start of song
    public float timeToTarget; //Time notes will take to reach target
    public float noteSpawnX; //Where notes should spawn X
    private float noteSpawnY; //Where notes should spawn Y
    public float noteTargetX; //Where notes should target X
    public float customStartTime = 0f; //Custom Start time for song, used for Debugging
    public float noteTimingOffset = 0f; //Note timing offset, used because notes from FNF Converter are timed a couple seconds off based on tempo
    public float infoAtStart = 5f; //How long info screen at start will show
    public GameObject lossScreen; //Reference to scene shown upon loss
    public bool noteLocked = false; //Bool to lock the spawning of botes
    public bool started = false; //Bool to determine if started
    public bool playerWon = false; //Bool to determine victory
    public bool playerLost = false; //Bool to determine loss
    //There is both a bool for victory and loss because there is the unsure state when the player is stil gaming

//------------------------------------------------------------------------
//User Defined Functions
    void noteSpawner(){ //Tracks current progress in song and spawns new notes accordingly      
        songTime = mainSong.time; //Grabs the time of the song
        if(mainMap.map.Count > 0 && !noteLocked){ //If there are still notes remaining and spawner is not locked
            if(mainMap.map[0].time - timeToTarget - customStartTime <= 0){ //If the time has passed the note's target spawn time
                if((Time.timeSinceLevelLoad - infoAtStart + customStartTime) > (mainMap.map[0].time + delayStart - timeToTarget)){ //Verification of note spawning to prevent mistiming
                    newArrow(mainMap.map[0].time, mainMap.map[0].button); //Spawn the new note with the newArrow function
                }
            }
            else if(!(mainMap.map[0].time - timeToTarget - customStartTime <= 0)){ //Another layer of checking if top conditional falls through
                if((mainMap.map[0].time - timeToTarget) < mainSong.time){ //Second layer of verification checking that note will be able to reach target, only really triggers in case of custom start time
                    newArrow(mainMap.map[0].time, mainMap.map[0].button); //Spawns the new note with the newArrow function
                }
            }
            else{ //If it doesn't meet either conditions above remove the note bc something is prob wrong
                mainMap.map.RemoveAt(0); //Removes the note because something is wrong
            }
        }else if(!playerWon){ //If there are no more notes and player has not yet won
            GameObject[] remainingArrows; //Storage for remaining arrows
            remainingArrows = GameObject.FindGameObjectsWithTag("arrow"); //Grabs remaining arrows
            if(remainingArrows.Length == 0 && !playerLost && !playerWon){ //If there are no more remaining arrows and layer has not lost or won yet
                playerLost = true; //Player lost condition set to true
                StartCoroutine(lossTransition()); //Starts the loss transition
            }
        }
    }

    IEnumerator lossTransition(){ //Loss transition coroutine
        float musicVolume = mainSong.volume; //Grabs music volume reference
        //Below is for fading audio
        while(musicVolume > 0f){ //If music volume is greater than zero, audible
            musicVolume -= 0.005f; //Reduce volume
            mainSong.volume = musicVolume; //Assign new volume
            yield return new WaitForSeconds(0.10f); //hold code for short amount of time and then repeat
        }

        //Below is for fading screen
        lossScreen.SetActive(true); //Enables the loss screen
        CanvasRenderer lossImage = GameObject.Find("lossImage").GetComponent<CanvasRenderer>(); //Grabs Canvas rederer to modify opacity
        float opacity = 0f; //Set starting opacity of 0f
        while(opacity <= 1){ //Run until opacity is 1 (fully visible)
            opacity += 0.05f; //Adds small amount of opacity
            lossImage.SetAlpha(opacity); //Assigns new opacity value
            yield return new WaitForSeconds(0.05f); //Holds code for 0.05 seconds and repeats
        }

        //Below is an enter gate to hold code execution until the player hits enter
        while(!Input.GetKeyDown(KeyCode.Return)){ //While enter is not held
            yield return null; //Repeat
        }
        
        SceneManager.LoadScene("Battle Stage"); //Once code breaks out of enter gate reload the battle stage so player can retry

        
    }

    void newArrow(float triggerTime, string button){ //This is my function that is going to instantiate my arrow prefab for each note
        mainMap.map.RemoveAt(0); //Removes the latest note, the one you are spawning
        GameObject newArrow = Instantiate(arrowPrefab); //This is what creates an arrow from the prefab
        noteSpawnY = 0f; //Default in case beatmap is wrong
        switch(button){ //Switch based off value of button, sets the Y value the note will spawn at so that it collides with the right trigger
            case "up":
                noteSpawnY = 1.1f;
                break;
            case "down":
                noteSpawnY = -1.9f;
                break;
            case "right":
                noteSpawnY = -0.9f;
                break;
            case "left":
                noteSpawnY = 0.1f;
                break;
        }
        
        newArrow.transform.position = new Vector3(noteSpawnX,noteSpawnY,0); //Sets the position for the new note
        noteController newArrowNC = newArrow.GetComponent<noteController>();  //Gets reference to the noteController component 
        
        //Below passes various values to noteControler
        newArrowNC.triggerTime = triggerTime; 
        newArrowNC.button = button; 
        newArrowNC.timeToTarget = timeToTarget; 
        newArrowNC.trackDistance = noteSpawnX - noteTargetX;
        
    }

    IEnumerator startMusic(){ //Initiates the song
        yield return new WaitForSeconds(delayStart); //Waits for delayStart amount
        mainSong.time = 0 + customStartTime; //Start at a custom start time if necessary
        mainSong.Play(); //Start song
    }

    void pruneNotesFrom(float prunePoint){ //Prune notes from is used to prune notes that are outside of the time range in case of custom start/end time
        if(mainMap.map.Count > 0){ //If main map has notes
            while(mainMap.map[0].time <= prunePoint){ //While the latest note is less than or equal to prune point
                mainMap.map.RemoveAt(0); //Remove the note
            }
        }
    }

    public void pruneNotesFromCurrent(){ //Prune notes from current time to end of song
        if(mainMap.map.Count > 0){ //If map map has notes
            while(mainMap.map[0].time <= mainSong.clip.length){ //If latest note is less than or equal to clip length
                mainMap.map.RemoveAt(0); //Remove note
            }
        }
    }

    void isAction(bool state){ //Just changes note state, really shouldn't be it's own function but I had initially planned to do a bit more to it
        noteLocked = state; //Sets note state to the passed value
    }

    IEnumerator battleInit(){ //Battle initialization Coroutine
        if(customStartTime != 0){ //If custom start time is not set to 0 (For Debugging)
            GameObject.Find("howToPlayCanvas").SetActive(false); //Disable the how to play canvas
        }else{ //If not (custom start time is 0)
            yield return new WaitForSeconds(infoAtStart); //Wait for the infoAtStart float
            GameObject.Find("howToPlayCanvas").SetActive(false); //Disable the how to play canvas
        }

        //Imports the beatMap's json file which holds the information on each note
        mainMap = new beatMap(); //Instantiates mainMap from beatMap class
        mainMap.readBeatMap("iWonder.json"); //Reads beat map from streaming assets
        for(var i = 0; i < mainMap.map.Count; i++){ //For each on of the notes in MainMap
            mainMap.map[i].time = mainMap.map[i].time + noteTimingOffset; //Add the timing offset
        }
        pruneNotesFrom(customStartTime); //In the case of a customStartTimr, prune notes
        StartCoroutine(startMusic()); //Start the music
        // StartCoroutine(fadeIn());
        started = true; //Set started state to true
        yield return null; //Default return case

    }
   

//------------------------------------------------------------------------
//Unity Defined functions
    // Start is called before the first frame update
    void Awake(){ //On Awake
        lossScreen = GameObject.Find("lossScreen"); //Assigns lossScreen
        mainSong = gameObject.GetComponent<AudioSource>(); //Assigns mainSong
    }
    
    void Start() //On Start
    {
        lossScreen.SetActive(false); //Disables loss screen
        StartCoroutine(battleInit()); //trigger battle init
 
    }

    void Update() //Called once per frame
    {
        if(started){ //if game has been started
            noteSpawner(); //Constantly update note spawner
        }

        //Below is an auto win button for testing abd debugging purposes, boils down to "Press V to Win"
        if(Input.GetKeyDown(KeyCode.V) & (Time.timeSinceLevelLoad > delayStart + 0.05f + infoAtStart)){ 
            enemyController drake = GameObject.Find("enemyObject").GetComponent<enemyController>();
            drake.enemyHealth = 0;
            drake.updateSlider();
        }
        
    }
}