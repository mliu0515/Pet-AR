using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR
using UnityEngine.XR.ARSubsystems;
using System;

public class PlaceModel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject modelToPlace;
    private Pose PlacementPose;
    public GameObject player; 
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid;
    void Start()
    {
        //Basically initiate the animal at the beginning without tapping or anything
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        getPlacementPose();
        placeObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //change the value of PlacementPost so that it can be used to inatantiate the animal
    private void getPlacementPose()
    {
        
        //var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(player.transform.position, hits, TrackableType.PlaneEstimated);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
		{
            PlacementPose = hits[0].pose;
		}
    }

    private void placeObject()
    {
        Instantiate(modelToPlace, PlacementPose.position, PlacementPose.rotation);
    }
}
