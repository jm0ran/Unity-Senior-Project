using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class battleInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rythmStateController.currentState == "action"){ //Allowable input during action state
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                Debug.Log("Menu Up");
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                Debug.Log("Menu Down");
            }
        }
    }
}
