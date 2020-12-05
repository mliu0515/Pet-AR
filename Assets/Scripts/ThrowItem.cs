using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{
    //Throw prefab item from the camera origin to playspace.
    public GameObject item;
    public AudioSource throwSound;
    int inventory = 5;
    public void Throw()
    {
            if (GameObject.FindGameObjectsWithTag("Pet") != null)
            {
                Debug.Log("thrown" + item.name);
                throwSound.Play();
                if (inventory > 1 && GameObject.FindGameObjectWithTag("Pet") != null)
                {
                    Instantiate(item, transform.position + transform.forward, Quaternion.identity);
                }
            }
        
    }
}
