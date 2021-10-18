using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    //Want to use UI Controller to control what is being shown in the UI stuff like menus and Dialogue for when I have more than just textboxs
    //------------------------------------------------------------------------
    //Main variables used in Script

    //------------------------------------------------------------------------
    //User defined functions

    //------------------------------------------------------------------------
    //Unity Defined Functions
    void Start()
    {
        gameObject.SetActive(false); //Disables object initially so that the UI is not visable on the start of a Scene
    }    
}
