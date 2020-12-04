using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR
using UnityEngine.XR.ARSubsystems;
using System;

public class Navigate : MonoBehaviour
{
    // Start is called before the first frame update
    //Plane detection so that object can navigate 
    //Also Interact with the food item
    //It will do a raycast to see if a food item is nearby
    //If there is a food item in the vicinity, it will walk over to the food on the plane detected
    //Using a collider, once a collision with food/item is detected, deactivate the food item.
    private Vector3 foodPosition;
    private GameObject foodItem;
    public float speed;
    private Vector3 start;
    private Vector3 dirNormalized;
    private bool dirCalculated = false;
    private int fullness = 0;
    public int maxFoodIntake;
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        walkToFood();
    }

    //detect food, store the position of the food in foodPosition
    void walkToFood()
    {
        if (foodExists()) 
        {
            interactWithItem();
        }

    }

    //walk towards the food
    //collide
    //deactivate the food
    void interactWithItem()
    {
        if (dirCalculated == true && foodPosition != null && Vector3.Distance(foodPosition, transform.position) > 0)
        {
            //I might neet to fix (Vector3.Distance(foodPosition, transform.position) > 0) part tho. 
            transform.position = transform.position + dirNormalized * speed * Time.deltaTime;
        }
    }

    //check if a food item is instantiated. If yes, resturn true and store it in foodItem
    //else return false
    private bool foodExists()
    {
        if (GameObject.FindWithTag("Food") != null)
        {
            foodItem = GameObject.FindWithTag("Food");
            foodPosition = foodItem.transform.position;
            if (dirCalculated == false)
            {
                dirNormalized = (start - foodPosition).normalized;
                dirCalculated = true;
            }
            
            return true;
        } 
        dirCalculated = false;
        return false;
    }

    //The collider thing
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food")) 
        {
            if (fullness == maxFoodIntake)
            {
                Debug.Log("The pet it full you can't feed it anymore");
            }
            else {
                fullness += 1;
                Debug.Log("The pet just ate the food");
            }
            //other.gameObject.SetActive(false);
            dirCalculated = false;
            Destroy(other.gameObject);
        }
    }

}
