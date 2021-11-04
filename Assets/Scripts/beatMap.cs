using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class beatMap{
    public List<note> map;

    //Constructor
    public beatMap(){
        map = new List<note>();
    }

    public void addNote(float time, string key){
        map.Add(new note(time, key));
    }

    public void readBeatMap(string path){
        //Going to read a beatmap from a file
    }
}
