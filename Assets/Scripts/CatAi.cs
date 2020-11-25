using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAi : MonoBehaviour
{

    /*
     * Place cat
     * Ability to walk around
     * Cat can look at the player 
     * Cat can interact with yarn 
     * AI must be bound by timer
    */
    public GameObject player;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");

    }

}
