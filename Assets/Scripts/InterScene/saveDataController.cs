using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataController : MonoBehaviour
{
    public static Save globalSave;

    void Start(){
        globalSave = new Save();
        globalSave.loadSavaData();
        globalSave.serializeSaveData();
        globalSave.instantiateCharacter("Kanye");
    }
}
