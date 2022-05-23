using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kanyeDanceController : MonoBehaviour
{
    public Sprite danceFrame1;
    public Sprite danceFrame2;
    public Sprite danceFrame3;
    public Sprite danceFrame4;
    public Sprite danceFrame5;
    public Sprite danceFrame6;
    public Sprite danceFrame7;
    public Sprite danceFrame8;

    private Sprite[] danceFrames;
    private Image targetImage;

    void Awake(){
        danceFrames = new Sprite[]{
            danceFrame1, danceFrame2, danceFrame3, danceFrame4, danceFrame5, danceFrame6, danceFrame7, danceFrame8
        };
        targetImage = gameObject.GetComponent<Image>();
        targetImage.sprite = danceFrames[0];
    }

    void OnEnable(){
        StartCoroutine(danceAnimation());
    }

    IEnumerator danceAnimation(){
        while(true){
            for(int i = 0; i < 8; i++){
                targetImage.sprite = danceFrames[i];
                yield return new WaitForSeconds(0.25f);
            }
        }
    }

           
}
