using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTestController : MonoBehaviour
{
    public AudioSource testAudio;
    public GameObject arrowPrefab;
    public beatMap mainMap;
    public float delayStart;
    public float targetX;
    public float noteSpawnPos;
    private float timeToTarget;

    //User Defined Functions
    void visualizeNote(){
        Debug.Log((mainMap.map[0].time + delayStart - timeToTarget) - Time.timeSinceLevelLoad);
        
        if(mainMap.map.Count > 0 && (Time.timeSinceLevelLoad) > (mainMap.map[0].time + delayStart - timeToTarget)){
            newArrow(mainMap.map[0].time, mainMap.map[0].button);
        }
    }

    void newArrow(float triggerTime, string button){ //This is my function that is going to instantiate my arrow
        mainMap.map.RemoveAt(0);
        GameObject newArrow = Instantiate(arrowPrefab); //This is what creates an arrow, this is also what I'll be doing with my script
        newArrow.transform.position = new Vector3(noteSpawnPos,2,0); //Want to change this based on note type
        noteController newArrowNC = newArrow.GetComponent<noteController>(); 
        newArrowNC.triggerTime = triggerTime;
        newArrowNC.button = button;
        
    }

    IEnumerator startMusic(){
        yield return new WaitForSeconds(delayStart);
        //Starts audio
        testAudio.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        timeToTarget = (noteSpawnPos - targetX) / (0.1f * 60f);
        testAudio = gameObject.GetComponent<AudioSource>();
        //Imports the beatMap's json file which holds the information on each note
        mainMap = new beatMap();
        mainMap.readBeatMap("Runaway.json");
        StartCoroutine(startMusic());
       
    }

    // Update is called once per frame
    void Update()
    {
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
    void FixedUpdate(){
        //Debug.Log(testAudio.time);
    }
}