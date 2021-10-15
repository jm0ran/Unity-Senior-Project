using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{
    public int destScene;
    public string originScene;
    public string nextCameraPos;
    public float destX;
    public float destY;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    void nextScene(){
        //Grabs cam size because pixelperfect camera is weird
        sceneController.initPlayerPos[0] = destX;
        sceneController.initPlayerPos[1] = destY;
        sceneController.origin = originScene;
        sceneController.cameraPos = nextCameraPos;
        SceneManager.LoadScene(destScene);
    }
}
