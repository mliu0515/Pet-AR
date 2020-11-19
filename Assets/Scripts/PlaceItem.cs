using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : MonoBehaviour
{
    //Throw prefab item from the camera origin to playspace.
    public GameObject item;
    public AudioSource throwSound;
    int inventory;
    void Start()
    {
    }
    void Update()
    {
        
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("thrown" + item.name);
                Throw();
            }
        }
        

    }
    void Throw()
    {
        throwSound.Play();
        if (inventory > 1)
        {
            Instantiate(item, transform.position + transform.forward, Quaternion.identity);
        }

    }
}
