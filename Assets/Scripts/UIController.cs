using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false); //Disables object
    }

    void FixedUpdate()
    {
        //I will need to check for input values relating to clicking next on dialogue
    }

    //I need to retrieve the game object for the textbox so I can assign text to it
}
