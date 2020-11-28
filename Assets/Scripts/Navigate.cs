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
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        detectFood();
    }

    //detect food, store the position of the food in foodPosition
    void detectFood()
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
        if (foodPosition != null && Vector3.Distance(foodPosition, transform.position) > 0)
        {
            transform.position = transform.position + dirNormalized * speed * Time.deltaTime;
        }
    }

    //check if a food item is instantiated. If yes, resturn true and stor it in foodItem
    //else return false
    private bool foodExists()
    {
        if (GameObject.Find("FoodItem") != null)
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
            other.gameObject.SetActive(false);
        }
    }

}
