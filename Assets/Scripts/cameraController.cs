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

    // Start is called before the first frame update
    void Start()
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
        if(cam.orthographicSize * cam.aspect + Mathf.Abs(player.position.x) < mapBoundaries[0]){
            newCameraCords[0] = player.position.x;
        }else{
            newCameraCords[0] = transform.position.x;
        }
        if (cam.orthographicSize + Mathf.Abs(player.position.y) < mapBoundaries[1]){
            newCameraCords[1] = player.position.y;
        }else{
            newCameraCords[1] = transform.position.y;
        }

        transform.position = new Vector3(newCameraCords[0], newCameraCords[1], transform.position.z);
        
    }

}
