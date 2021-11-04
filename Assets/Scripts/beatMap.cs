using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
        this.map = JsonUtility.FromJson<beatMap>(File.ReadAllText(Path.Combine(Application.streamingAssetsPath, path))).map;
    }
}
