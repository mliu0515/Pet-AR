using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatAi : MonoBehaviour
{
    Animator anim;
    Target lookTarget;
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public ParticleSystem particles;
    float timerForNewPath = 1f;
    public GameObject lookForward;
    bool doneLooking = false;
    bool validPath;
    Vector3 moveTarget;
    GameObject foundItem;

    public enum State
    {
        IDLING, WALKING, ITEM
    }
    public State catState;
    void Start() 
    {
        catState = State.IDLING;
        particles = particles.GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        lookTarget = GameObject.FindGameObjectWithTag("Target").GetComponent<Target>();
        StartCoroutine(LookAtPlayer());
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    void Update() //manages what scripts to use, if ifelse ifelse if else time , horrible coding practiced proceed!
    {
        if (doneLooking)
        {
            if (GameObject.FindGameObjectWithTag("Food") != null && catState != State.ITEM)
            {
                foundItem = GameObject.FindGameObjectWithTag("Food");

                Debug.Log("I found fish!");
                StartCoroutine(FindItem());

            }
            else if (GameObject.FindGameObjectWithTag("yarn") != null && catState != State.ITEM)
            { 
                foundItem = GameObject.FindGameObjectWithTag("yarn");
                
                Debug.Log("I found yarn!");
                StartCoroutine(FindItem()); 
                
            }
            if (catState == State.IDLING)
            {
                StartCoroutine(DoSomething());
            }
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                Idle();
            }
            else
            {
                Walk();
            }
        }
        
    }
    IEnumerator LookAtPlayer()  //look at player for 3 seconds and then go back to idling 
    {
        lookTarget.target = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(5);
        doneLooking = true;
        Debug.Log("I am done looking at player!");
        
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-1, 1);
        float z = Random.Range(-1, 1);
        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }
    IEnumerator DoSomething()
    {
        catState = State.WALKING;
        timerForNewPath = Random.Range(5, 10);
        Debug.Log("I will idle move in " + timerForNewPath + " seconds!");
        yield return new WaitForSeconds(timerForNewPath);
        GetNewPath();

        validPath = navMeshAgent.CalculatePath(moveTarget, path);
        if (!validPath)
        {
            Debug.Log("I can't move to this location!");
        }
        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(moveTarget, path);
        }
        catState = State.IDLING;
    }
    void GetNewPath()
    {
        moveTarget = getNewRandomPosition();
        navMeshAgent.SetDestination(moveTarget);
    }
    void Walk()
    {
        lookTarget.target = lookForward;
        anim.SetBool("WalkBool", true);
    }
    void Idle()
    {
        anim.SetTrigger("Idle");
        anim.SetBool("WalkBool", false);
    }
    IEnumerator FindItem()
    {
        catState = State.ITEM;
        moveTarget = foundItem.transform.position;
        lookTarget.target = foundItem;
        navMeshAgent.SetDestination(moveTarget);
        if (!particles.isPlaying) particles.Play();
        while (foundItem != null)
        {
            yield return new WaitForSeconds(10f);
            Destroy(foundItem);
            foundItem = null;

        }
        Debug.Log("I am done using the Item!");
        if (particles.isPlaying) particles.Pause();
        catState = State.IDLING;

    }
}
