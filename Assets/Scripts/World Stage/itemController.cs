using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{

//The item controller stores item sprites for rendering

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    //Storage of sprites
    public Sprite pitbullGlasses;
    public Sprite dragonBall;
    public Sprite yeezy;
    public Sprite mbdtf;
    public Sprite oldYeezy;
    //Dictionaries for linking keys to sprites and item descriptions
    public static Dictionary<string, Sprite> itemDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, string> infoDictionary = new Dictionary<string, string>();


    //Sort out the error relating to this at a later point
    void Awake(){
        if(!(itemDictionary.Count > 0)){ //If itemDictionary has not been initialized
            //Set itemDictionary values
            itemDictionary.Add("Black Glasses", pitbullGlasses);
            itemDictionary.Add("Yeezy", yeezy);
            itemDictionary.Add("Dragon Ball", dragonBall);
            itemDictionary.Add("MBDTF", mbdtf);
            itemDictionary.Add("Old Yeezy", oldYeezy);
        }
        if(!(infoDictionary.Count > 0)){ //If infoDictionary has not been initialized
            //Set infoDictionary values
            infoDictionary.Add("Black Glasses", "A pair of simple black glasses, something tells you they've been well traveled");
            infoDictionary.Add("Yeezy", "The matching shoe, will this open the door to a new discovery?");
            infoDictionary.Add("Dragon Ball", "???");
            infoDictionary.Add("MBDTF", "The widely recognized GOAT Kanye West Album");
            infoDictionary.Add("Old Yeezy", "The item that sent you on this journey, for some reason it feels special");
        }
    }
}
