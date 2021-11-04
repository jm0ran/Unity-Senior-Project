using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTestController : MonoBehaviour
{
    public AudioSource testAudio;
    public GameObject testObj;
    public beatMap mainMap;

    // Start is called before the first frame update
    void Start()
    {
        testAudio = gameObject.GetComponent<AudioSource>();
        testAudio.Play();

        //Messing around with beatmaps and checking them out 
        mainMap = new beatMap();
        mainMap.addNote(1.2f, "up");
        //I have to be careful because unity serializes floats in weird and wacky ways 
        Debug.Log(JsonUtility.ToJson(mainMap));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("p")){
            testAudio.Pause();
        }else if(Input.GetKey("o")){
            testAudio.UnPause();
        }
        if(Input.GetKeyDown("space")){
            testObj.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(ExecuteAfterTime());
        }
    }
    void FixedUpdate(){
        //Debug.Log(testAudio.time);
    }

    IEnumerator ExecuteAfterTime(){
        Debug.Log("Started");
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Reenabled");
        testObj.GetComponent<SpriteRenderer>().enabled = true;
    }
}