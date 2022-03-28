using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    public Sprite pitbullGlasses;
    public Sprite dragonBall;
    public Sprite yeezy;
    public Sprite mbdtf;


    public static Dictionary<string, Sprite> itemDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, string> infoDictionary = new Dictionary<string, string>();


    //Sort out the error relating to this at a later point
    void Awake(){
        if(!(itemDictionary.Count > 0)){
            itemDictionary.Add("Black Glasses", pitbullGlasses);
            itemDictionary.Add("Yeezy", yeezy);
            itemDictionary.Add("Dragon Ball", dragonBall);
            itemDictionary.Add("MBDTF", mbdtf);
        }
        
        if(!(infoDictionary.Count > 0)){
            // infoDictionary.Add();
        }

    }
}
