using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
   public string enemy = "Drake";

   void Start(){
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
   }

   
}
