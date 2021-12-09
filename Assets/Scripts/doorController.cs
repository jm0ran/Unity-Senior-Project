using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public string destScene; //Destination scene
    public string originScene; //Origin scene
    public string nextCameraPos; //Next CameraPosition
    public float destX; //X destination for player
    public float destY; //Y destination for player

    public GameObject fadeShade;

//------------------------------------------------------------------------
//Main User defined functions
    void nextScene(){
        //Grabs cam size because pixelperfect camera is weird
        sceneController.initPlayerPos[0] = destX;
        sceneController.initPlayerPos[1] = destY;
        sceneController.origin = originScene;
        sceneController.cameraPos = nextCameraPos;
        StartCoroutine(fadeTransition(destScene));
    }

    IEnumerator fadeTransition(string destScene){
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>();
        float alpha = 0.0f;
        while (alpha < 1f){
            alpha += 0.02f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.0025f);
        }
        SceneManager.LoadScene(destScene);

    }



//------------------------------------------------------------------------
//Unity defined functions
    void Start(){
        fadeShade = GameObject.FindWithTag("fadeShade");
    }

    void Update(){ //Want to move this to input controller
        if(Input.GetKeyDown("m")){
            SceneManager.LoadScene(4);
        }
    }
}

