using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTestController : MonoBehaviour
{
    public AudioSource testAudio;
    public GameObject arrowPrefab;
    public beatMap mainMap;

    //User Defined Functions
    void visualizeNote(){
        if(mainMap.map.Count > 0 && testAudio.time > mainMap.map[0].time){
            newArrow(mainMap.map[0].time, mainMap.map[0].button);
            mainMap.map.RemoveAt(0);
        }
    }

    void newArrow(float triggerTime, string button){ //This is my function that is going to instantiate my arrow
        GameObject newArrow = Instantiate(arrowPrefab); //This is what creates an arrow, this is also what I'll be doing with my script
        newArrow.transform.position = new Vector3(3,2,0); //Want to change this based on note type
        noteController newArrowNC = newArrow.GetComponent<noteController>(); 
        newArrowNC.triggerTime = triggerTime;
        newArrowNC.button = button;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //Creates and starts audio
        testAudio = gameObject.GetComponent<AudioSource>();
        testAudio.Play();

        //Imports the beatMap's json file which holds the information on each note
        mainMap = new beatMap();
        mainMap.readBeatMap("Runaway.json");
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