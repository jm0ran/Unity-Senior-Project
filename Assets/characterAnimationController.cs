using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterAnimationController : MonoBehaviour
{
    //This is where I want to animate both Kanye and Drake's headnods. I don't like the Unity Built in editor

    //Kanye vars
    private SpriteRenderer kanyeTarget;
    public Sprite kanyeNod1;
    public Sprite kanyeNod2;
    private Sprite[] kanyeNods;

    //Drake vars
    private SpriteRenderer drakeTarget;
    public Sprite drakeNod1;
    public Sprite drakeNod2;
    private Sprite[] drakeNods;

    void Awake(){
        kanyeTarget = GameObject.Find("playerObject").GetComponent<SpriteRenderer>();
        drakeTarget = GameObject.Find("enemyObject").GetComponent<SpriteRenderer>();

        kanyeNods = new Sprite[]{kanyeNod1, kanyeNod2};
        drakeNods = new Sprite[]{drakeNod1, drakeNod2};
    }

    IEnumerator startAnimations(){
        while(true){
            for(int i = 0; i < 2; i++){
                kanyeTarget.sprite = kanyeNods[i];
                drakeTarget.sprite = drakeNods[i];
                yield return new WaitForSeconds(0.30f);
            }
        }
    }

    void Start(){
        StartCoroutine(startAnimations());
    }
}

