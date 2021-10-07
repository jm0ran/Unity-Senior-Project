using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionController : MonoBehaviour
{
    public float interactionRange; //Want to implement later
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void printHi(string message){
        gameObject.SetActive(false);
        Debug.Log(message);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
