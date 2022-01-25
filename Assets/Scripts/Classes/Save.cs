using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Save{
    //Really all I'm looking to store in here right now are acquired characters and team comp
    public List<Character> acquiredCharacters;
    public string[] currentTeam;
    //MAKE SURE TO ADD NEW VALUES DOWN IN READ FUNCTION

    public Save(){
        acquiredCharacters = new List<Character>();
        currentTeam = new string[3] {null, null, null};
    }

    public void serializeSaveData(){
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "bigBoi.json"), jsonDataW);
    }

    public void loadSavaData(){
        string jsonDataR = "";
        try{
            jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "bigBoi.json"));
            if(jsonDataR != ""){
                //Load the properties individually because object itself is read only, I could put this into a larger object but that sounds annoying, but is it more annoying then typing this long ass comment
                Save acquiredData = JsonUtility.FromJson<Save>(System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "bigBoi.json")));
                this.acquiredCharacters = acquiredData.acquiredCharacters;
                this.currentTeam = acquiredData.currentTeam;
            }else{
                Debug.Log("Save data json file is empty");
            }
        }catch{
            Debug.Log("Could not find json file at " +  Path.Combine(Application.streamingAssetsPath, "bigBoi.json"));
        }
    }

    public void instantiateCharacter(string charName){
        bool willContinue = true;
        for(int i = 0; i <acquiredCharacters.Count; i++){
            if(acquiredCharacters[i].name == charName){
                willContinue = false;
            }else{
                willContinue = true;
            }
        }
        if(willContinue){
            try{
                string jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "characters", charName + ".json"));
                Character returnChar = JsonUtility.FromJson<Character>(jsonDataR);
                acquiredCharacters.Add(returnChar);
            }catch{
                willContinue = false;
                Debug.Log("No character found with name: " + charName);
            }
        }else{
            Debug.Log("Character Already Exists");
        }
        serializeSaveData();
    }
    public void assignToTeam(string charName, int charIndex){
        bool charExists = false;
        bool charAlreadyInTeam = false;
        for(int i = 0; i < this.acquiredCharacters.Count; i++){
            if(acquiredCharacters[i].name == charName){
                charExists = true;
            }
        }

        for(int i = 0; i < this.currentTeam.Length; i++){
            if(currentTeam[i] == charName){
                charAlreadyInTeam = true;
            }
        }

        if(charExists && !charAlreadyInTeam && charIndex <= 3 && charIndex >= 0){
            this.currentTeam[charIndex] = charName;
        }
        serializeSaveData();
    }

    public void pruneTeam(){ 
        for(int i = 0; i < this.currentTeam.Length; i++){
            if(this.currentTeam[i] == ""){
                for(int j = i + 1; j < this.currentTeam.Length; j++){
                    currentTeam[j - 1] = currentTeam[j];
                    currentTeam[j] = "";
                }
            }
        }
        serializeSaveData();
    }

}
