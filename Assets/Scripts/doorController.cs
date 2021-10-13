using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{
    public int destScene;
    public string originScene;
    public string nextCameraPos;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    void nextScene(){
        //Grabs cam size because pixelperfect camera is weird
        sceneController.origin = originScene;
        sceneController.cameraPos = nextCameraPos;
        SceneManager.LoadScene(destScene);
    }
}
