using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstItem : MonoBehaviour
{
    // Instantiated item
    Rigidbody rb;
    public float speed = 600f;
    GameObject player;
    float maxFallDistance = -1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        //transform.rotation = player.transform.rotation * Quaternion.Euler(90, 0, 0);
        rb.AddForce(player.transform.forward * speed);
        Debug.Log("placed");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (transform.position.y <= maxFallDistance)
        {
            Debug.Log("Item Out of bounds!");
            Destroy(gameObject);
        }
        */
    }
}
