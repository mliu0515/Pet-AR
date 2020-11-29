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
    Target lookTarget;
    
    void Start() 
    {
        lookTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<Target>();
        StartCoroutine(LookAtPlayer());
    }

    void Update() //manages what scripts to use, if ifelse ifelse if else time !
    {
        if (GameObject.FindGameObjectWithTag("yarn") != null)
        {
            lookTarget.target = GameObject.FindGameObjectWithTag("yarn");
        }
    }
    IEnumerator LookAtPlayer()  //look at player for 3 seconds and then go back to idling 
    {
        lookTarget.target = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(5);
        Debug.Log("I am done looking at player!");
    }
}
