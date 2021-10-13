using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public Camera cam; 
    public GameObject map;
    public SpriteRenderer mapSprite;
    public float[] mapBoundaries;
    public float[] newCameraCords;

    //I have to be careful about the order I calls stuff like "Awake, Start, OnEnable, etc"
    void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
        map = GameObject.FindWithTag("Map");
        mapSprite = map.GetComponent<SpriteRenderer>();

        //Create arrays
        mapBoundaries = new float[2];
        newCameraCords = new float[2];

        //Assign the Map Boundaries, 0 is horizontal reach, 1 is vertical
        mapBoundaries[0] = mapSprite.bounds.extents.x;
        mapBoundaries[1] = mapSprite.bounds.extents.y;

        
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z);
        
    }

    void setCamera(string cameraPos){
        switch(cameraPos){
            case "topLeft":
                Debug.Log(sceneController.camSize);
                newCameraCords[0] = -(mapBoundaries[0]) + sceneController.camSize * sceneController.camAspect;
                newCameraCords [1] = (mapBoundaries[1]) - sceneController.camSize;
                transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z);
                break;
        }
    }

}
