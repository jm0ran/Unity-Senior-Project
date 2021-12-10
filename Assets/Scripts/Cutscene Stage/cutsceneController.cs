using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cutsceneController : MonoBehaviour
{
    public TextMeshProUGUI sceneText;


    void Start()
    {
        sceneText = GameObject.FindWithTag("textBox").GetComponent<TextMeshProUGUI>();
        sceneText.text = "Hi";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
