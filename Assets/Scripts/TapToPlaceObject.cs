using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR
using UnityEngine.XR.ARSubsystems;
using UnityEngine.AI;
using System;

//So yea this script is used for placing item onto the scene.
//So like food and stuff that is going to be thrown to the pet. 
[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject Animal;
    public GameObject Toilet;
    public GameObject Rock;
    public GameObject placementIndicator;
    public GameObject Yarn;
    public GameObject Fish;


    private Pose PlacementPose;
    private Vector2 touchPosition;
    private ARRaycastManager aRRaycastManager;
    private GameObject spawnObject;
    private GameObject spawnAnimal;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool placementPoseIsValid = false;
    private bool animalExists = false;
    private bool objExists = false;


    public int objNumber = 3;
    public int modelNumber;
    // Start is called before the first frame update
    void Awake()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdatePlacementPose(out Vector2 touchPosition))
        
            return;
        
        if (aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPost = hits[0].pose;
            PlacementPose = hitPost;
            placementPoseIsValid = hits.Count > 0;
            UpdatePlacementIndicator();
            PlaceObject();
        }
        // UpdatePlacementIndicator();

    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
		{
            placementIndicator.SetActive(false);
		}
	}

    private bool UpdatePlacementPose(out Vector2 touchPosition)
	{
        //var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        // List<ARRaycastHit> hits = new List<ARRaycastHit>();
        // aRRaycastManager.Raycast(player.transform.position, hits, TrackableType.PlaneEstimated);

        // placementPoseIsValid = hits.Count > 0;
        // if (placementPoseIsValid)
		// {
        //     PlacementPose = hits[0].pose;
		// }
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
	}

    private void PlaceObject()
    {
        Debug.Log("Model number is " + modelNumber);
        Debug.Log("animalExists is " + animalExists);
        if (spawnObject == null && animalExists == true && objExists == false)
        {
            if (objNumber == 0) {
                spawnObject = Instantiate(Yarn, PlacementPose.position, PlacementPose.rotation);
                objExists = true;
            } else if (objNumber == 1) {
                spawnObject = Instantiate(Fish, PlacementPose.position, PlacementPose.rotation);
                objExists = true;
            } else {
                objExists = false;
            }
        }
        else
        {
            Debug.Log("You already threw an item");
        }
        
        if (animalExists == false)
        {
            //sneak edit to make it work with nav agent
            //Vector3 newPos = new Vector3(PlacementPose.position.x, .5f, PlacementPose.position.z);
            if (modelNumber == 0) {
                Debug.Log("Placing cat!");
               spawnAnimal = Instantiate(Animal, PlacementPose.position, PlacementPose.rotation);
            } else if (modelNumber == 1) {
                Debug.Log("placing toilet");
                spawnAnimal = Instantiate(Toilet, PlacementPose.position, PlacementPose.rotation);
            } else {
                Debug.Log("placing rock");
                spawnAnimal = Instantiate(Rock, PlacementPose.position, PlacementPose.rotation);
            }

            Debug.Log("Here's your animal!");
            animalExists = true;
        }
        else
        {
            Debug.Log("You already have a pet");
        }
        
    }

    public void RemoveCurrentPet() {
        Debug.Log("Removing current pet");
        Destroy(spawnAnimal);
        Destroy(spawnObject);
        animalExists = false;
    }

    public void RemoveCurrentItem() {
        Destroy(spawnObject);
        objExists = false;

    }
}
