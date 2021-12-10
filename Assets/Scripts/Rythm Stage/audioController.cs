using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public static float songTime = 0;

    public AudioSource testAudio;
    public GameObject arrowPrefab;
    public beatMap mainMap;
    public float delayStart;
    public float timeToTarget;
    public float noteSpawnX;
    private float noteSpawnY;
    public float noteTargetX;
    public float customStartTime = 0;

//------------------------------------------------------------------------
//User Defined Functions
    void visualizeNote(){        
        if(mainMap.map.Count > 0 && (Time.timeSinceLevelLoad + customStartTime) > (mainMap.map[0].time + delayStart - timeToTarget)){
            if(mainMap.map[0].time < customStartTime){
                mainMap.map.RemoveAt(0);
            }else{
                newArrow(mainMap.map[0].time, mainMap.map[0].button);
            }
        }
    }

    void updateSongTime(){
        songTime = testAudio.time;
    }

    void newArrow(float triggerTime, string button){ //This is my function that is going to instantiate my arrow
        mainMap.map.RemoveAt(0);
        GameObject newArrow = Instantiate(arrowPrefab); //This is what creates an arrow, this is also what I'll be doing with my script
        noteSpawnY = 0f; //Default in case beatmap is wrong
        switch(button){
            case "up":
                noteSpawnY = 1.5f;
                break;
            case "down":
                noteSpawnY = 0.5f;
                break;
            case "right":
                noteSpawnY = -0.5f;
                break;
            case "left":
                noteSpawnY = -1.5f;
                break;
        }
        
        newArrow.transform.position = new Vector3(noteSpawnX,noteSpawnY,0); //Want to change this based on note type
        noteController newArrowNC = newArrow.GetComponent<noteController>(); 
        newArrowNC.triggerTime = triggerTime;
        newArrowNC.button = button;
        newArrowNC.timeToTarget = timeToTarget;
        newArrowNC.trackDistance = noteSpawnX - noteTargetX;
        
    }

    IEnumerator startMusic(){
        yield return new WaitForSeconds(delayStart);
        //Starts audio
        testAudio.time = 0 + customStartTime;
        testAudio.Play();
    }



//------------------------------------------------------------------------
//Unity Defined functions
    // Start is called before the first frame update
    void Start()
    {
        testAudio = gameObject.GetComponent<AudioSource>();
        //Imports the beatMap's json file which holds the information on each note
        mainMap = new beatMap();
        mainMap.readBeatMap("Runaway.json");
        StartCoroutine(startMusic());
       
    }

    // Update is called once per frame
    void Update()
    {
        updateSongTime();
        visualizeNote();
        if(Input.GetKey("p")){
            testAudio.Pause();
        }else if(Input.GetKey("o")){
            testAudio.UnPause();
        }
        if(Input.GetKeyDown("space")){
            Debug.Log(testAudio.time);
        }
    }
}