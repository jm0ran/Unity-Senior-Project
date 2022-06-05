using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//This is the beatMap class, used to store the necessary notes

[System.Serializable] //Allows it to be converted to JSON
public class beatMap{
    public List<note> map; //Lists of notes

    public beatMap(){ //Constructor
        map = new List<note>(); //Creates map
    }

    public void addNote(float time, string key){ //Method for adding notes
        map.Add(new note(time, key)); //Add a new note with time and key arguments to map
    }

    public void readBeatMap(string path){ //Method to load beatMap from a json file
        //Reads json file and forms a beatMap object for it, then assigns map value to current object
        this.map = JsonUtility.FromJson<beatMap>(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, path))).map;
    }
}
