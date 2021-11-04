using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistController : MonoBehaviour
{
    public static List<bool> gameProg = 
    new List<bool>() { //Indexes will be assigned to one time objects and set to false if they havent been triggered
        false, //1: Initial Dialougue in the JunkCave
        false,
        false
    };
}
