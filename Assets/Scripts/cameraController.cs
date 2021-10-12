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

    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        map = GameObject.FindWithTag("Map");
        mapSprite = map.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }

    void FixedUpdate(){
        //Going to try to use this to bound the camera from going off the map
        //Debug.Log(cam.orthographicSize); 
        Debug.Log(mapSprite.bounds.extents.x);
    }
}
