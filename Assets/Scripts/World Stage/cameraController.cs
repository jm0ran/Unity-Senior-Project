using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

//Camera controller focuses on initial camera position and the camera following the player

//------------------------------------------------------------------------
//Main variables used in Script
    public Transform player; //Reference to player transform
    public Vector3 offset; //Offset 
    public Camera cam; //Reference to camera
    public GameObject map; //Reference to map object
    public SpriteRenderer mapSprite; //Reference to mapSprite renderer
    public float[] mapBoundaries; //Storage for mapBoundaries
    public float[] newCameraCords; //Storage for newCameraCords

//------------------------------------------------------------------------
//User Defined Functions
    void setCamera(string cameraPos){ //Used to move the camera to starting positions stores in the Scene Controller
        switch(cameraPos){ //Switch statement based on intended camera position
            //Each case moves the camera based on the passed cameraPos for the start of different scenes
            case "topLeft":
                newCameraCords[0] = -(mapBoundaries[0]) + sceneController.camSize * sceneController.camAspect;
                newCameraCords [1] = (mapBoundaries[1]) - sceneController.camSize;
                transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z);
                break;
            case "bottom":
                newCameraCords[0] = 0f;
                newCameraCords[1] = (-mapBoundaries[1]) + sceneController.camSize;
                transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z);
                break;
            case "top":
                newCameraCords[0] = 0f;
                newCameraCords[1] = mapBoundaries[1] - sceneController.camSize;
                transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z);
                break;
        }
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        //Grabs the necessary components
        cam = gameObject.GetComponent<Camera>();
        map = GameObject.FindWithTag("Map");
        mapSprite = map.GetComponent<SpriteRenderer>();
        //Creates Map Boundaries and CameraCord arrays, index 0 is horizontal while 1 is vertical
        mapBoundaries = new float[2];
        newCameraCords = new float[2];
        mapBoundaries[0] = mapSprite.bounds.extents.x;
        mapBoundaries[1] = mapSprite.bounds.extents.y;
        //Sets the players initial position
        player.position = new Vector3(sceneController.initPlayerPos[0], sceneController.initPlayerPos[1], player.position.z);
    }

    void Update(){ //Update function focuses on camera following player within boundaries
        //If Camera's x position is at the maximum distance for X stop updating in relation to player
        if(cam.orthographicSize * cam.aspect + Mathf.Abs(player.position.x) < mapBoundaries[0]){
            newCameraCords[0] = player.position.x;
        }else{
            newCameraCords[0] = transform.position.x;
        }
        //Same as before but for y
        if (cam.orthographicSize + Mathf.Abs(player.position.y) < mapBoundaries[1]){
            newCameraCords[1] = player.position.y;
        }else{
            newCameraCords[1] = transform.position.y;
        }
        transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z); //Finalize the position for the camera
        
    }
}
