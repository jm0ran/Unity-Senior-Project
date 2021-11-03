using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTestController : MonoBehaviour
{
    public AudioSource testAudio;
    // Start is called before the first frame update
    void Start()
    {
        testAudio = gameObject.GetComponent<AudioSource>();
        testAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("p")){
            testAudio.Pause();
        }else if(Input.GetKey("o")){
            testAudio.UnPause();
        }
    }
    void FixedUpdate(){
        Debug.Log(testAudio.time);
    }