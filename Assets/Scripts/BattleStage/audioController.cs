using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class audioController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public static float songTime = 0;

    public static AudioSource mainSong;
    public GameObject arrowPrefab;
    public beatMap mainMap;
    public float delayStart;
    public float timeToTarget;
    public float noteSpawnX;
    private float noteSpawnY;
    public float noteTargetX;
    public float customStartTime = 0f;
    public float noteTimingOffset = 0f;
    public float infoAtStart = 5f;
    public GameObject lossScreen;

    public bool noteLocked = false;
    public bool started = false;
    public bool playerWon = false;
    public bool playerLost = false;

//------------------------------------------------------------------------
//User Defined Functions

    void noteSpawner(){ //Tracks current progress in song and spawns new notes accordingly      
        songTime = mainSong.time;
        if(mainMap.map.Count > 0 && !noteLocked){
            if(mainMap.map[0].time - timeToTarget - customStartTime <= 0){
                if((Time.timeSinceLevelLoad - infoAtStart + customStartTime) > (mainMap.map[0].time + delayStart - timeToTarget)){
                    newArrow(mainMap.map[0].time, mainMap.map[0].button);
                }
            }
            else if(!(mainMap.map[0].time - timeToTarget - customStartTime <= 0)){
                if((mainMap.map[0].time - timeToTarget) < mainSong.time){
                    newArrow(mainMap.map[0].time, mainMap.map[0].button);
                }
            }
            else{ //If it doesn't meet either conditions above remove the note bc something is prob wrong
                mainMap.map.RemoveAt(0);
            }
        }else if(!playerWon){
            //Check how many notes are left
            GameObject[] remainingArrows;
            remainingArrows = GameObject.FindGameObjectsWithTag("arrow");
            if(remainingArrows.Length == 0 && !playerLost && !playerWon){
                playerLost = true;
                StartCoroutine(lossTransition());
            }
           
        }
    }

    IEnumerator lossTransition(){
        Debug.Log("Player Lost");
        float musicVolume = mainSong.volume;
        while(musicVolume > 0f){
            musicVolume -= 0.005f;
            mainSong.volume = musicVolume;
            yield return new WaitForSeconds(0.10f);
        }

        //This is where I need to fade in the loss screen relatively quickly
        lossScreen.SetActive(true);
        CanvasRenderer lossImage = GameObject.Find("lossImage").GetComponent<CanvasRenderer>();
        float opacity = 0f;

        while(opacity <= 1){
            opacity += 0.05f;
            lossImage.SetAlpha(opacity);
            yield return new WaitForSeconds(0.05f);
        }

        //Now I need an enter gate to hold player until they make their decision
        while(!Input.GetKeyDown(KeyCode.Return)){
            yield return null;
        }
        
        SceneManager.LoadScene("Battle Stage");

        
    }

    void newArrow(float triggerTime, string button){ //This is my function that is going to instantiate my arrow
        mainMap.map.RemoveAt(0);
        GameObject newArrow = Instantiate(arrowPrefab); //This is what creates an arrow, this is also what I'll be doing with my script
        noteSpawnY = 0f; //Default in case beatmap is wrong
        switch(button){
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
        
        newArrow.transform.position = new Vector3(noteSpawnX,noteSpawnY,0); 
        noteController newArrowNC = newArrow.GetComponent<noteController>(); 
        newArrowNC.triggerTime = triggerTime;
        newArrowNC.button = button;
        newArrowNC.timeToTarget = timeToTarget;
        newArrowNC.trackDistance = noteSpawnX - noteTargetX;
        
    }

    IEnumerator startMusic(){
        yield return new WaitForSeconds(delayStart);
        //Starts audio
        mainSong.time = 0 + customStartTime; //Start at a custom start time if necessary
        mainSong.Play();
    }

    void pruneNotesFrom(float prunePoint){
        if(mainMap.map.Count > 0){
            while(mainMap.map[0].time <= prunePoint){
                mainMap.map.RemoveAt(0);
            }
        }
    }

    public void pruneNotesFromCurrent(){
        if(mainMap.map.Count > 0){
            while(mainMap.map[0].time <= mainSong.clip.length){
                mainMap.map.RemoveAt(0);
            }
        }
    }

    void isAction(bool state){ //Function that is going to clear notes on field, lock notespawner and iniate transition to action stage
        noteLocked = state;
    }

    IEnumerator battleInit(){
        if(customStartTime != 0){
            GameObject.Find("howToPlayCanvas").SetActive(false);
        }else{
            yield return new WaitForSeconds(infoAtStart);
            GameObject.Find("howToPlayCanvas").SetActive(false);
        }


        mainSong = gameObject.GetComponent<AudioSource>();
        //Imports the beatMap's json file which holds the information on each note
        mainMap = new beatMap();
        mainMap.readBeatMap("iWonder.json");
        for(var i = 0; i < mainMap.map.Count;i++){
            mainMap.map[i].time = mainMap.map[i].time + noteTimingOffset;
        }
        pruneNotesFrom(customStartTime);
        StartCoroutine(startMusic());
        // StartCoroutine(fadeIn());
        started = true;
        yield return null;

    }

    IEnumerator timingEvents(){
        yield return null;
    }
   

//------------------------------------------------------------------------
//Unity Defined functions
    // Start is called before the first frame update
    void Awake(){
        lossScreen = GameObject.Find("lossScreen");
    }
    
    void Start()
    {
        lossScreen.SetActive(false);
        StartCoroutine(battleInit());
 
    }

    // Update is called once per frame
    void Update()
    {
        if(started){
            noteSpawner();
        }
        
    }
}