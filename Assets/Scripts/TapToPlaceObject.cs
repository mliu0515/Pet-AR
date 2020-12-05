using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR
using UnityEngine.XR.ARSubsystems;
using System;

//So yea this script is used for placing item onto the scene.
//So like food and stuff that is going to be thrown to the pet. 
[RequireComponent(typeof(ARRaycastManager))]
public class TapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject Animal;
    public GameObject placementIndicator;
    private Pose PlacementPose;
    private Vector2 touchPosition;
    private ARRaycastManager aRRaycastManager;
    private GameObject spawnObject;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool placementPoseIsValid = false;
    private bool animalExists = false;
    // Start is called before the first frame update
    void Awake()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdatePlacementPose(out Vector2 touchPosition))
        {
            return;
        }
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
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
	}

    private void PlaceObject()
    {
        if (animalExists == false)
        {
            Instantiate(Animal, PlacementPose.position, PlacementPose.rotation);
            Debug.Log("Here's your animal!");
            animalExists = true;
        }
        else if (spawnObject == null)
        {
            spawnObject = Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
        }
        else {
            Debug.Log("You already threw an item");
        }
    }
}
