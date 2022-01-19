using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterManagementController : MonoBehaviour
{
    private List<Move> moves;
    private Character character;
    //Temporary code to get my character library started
    void Start(){
        moves = new List<Move>();
        moves.Add(new Move("Tackle", 20, 30));
        moves.Add(null);
        moves.Add(null);
        moves.Add(null);
        character = new Character("Kanye", 200, moves);
        character.serialize();
    }
}
