using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titleController : MonoBehaviour
{
    public GameObject flashingText;

    // Start is called before the first frame update
    void Start()
    {
        flashingText = GameObject.FindWithTag("flashingText");
        StartCoroutine(startFlashingText());
    }


    IEnumerator startFlashingText(){
        flashingText.SetActive(false);
        yield return new WaitForSeconds(0.75f);
        flashingText.SetActive(true);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(startFlashingText());
    }
}
