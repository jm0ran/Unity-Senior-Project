using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{
    public int destScene;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    }

    void nextScene(){
        sceneController.origin = "junkCave";
        SceneManager.LoadScene(destScene);
    }
}
