using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour
{
    public Sprite pitbullGlasses;
    public Sprite dragonBall;
    public Sprite yeezy;

    public static Dictionary<string, Sprite> itemDictionary = new Dictionary<string, Sprite>();

    void Awake(){
        itemDictionary.Add("Black Glasses", pitbullGlasses);
        itemDictionary.Add("Yeezy", yeezy);
        itemDictionary.Add("Dragon Ball", dragonBall);
    }
}
