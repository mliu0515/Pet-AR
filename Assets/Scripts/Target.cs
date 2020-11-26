using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position;
        if (GameObject.FindGameObjectWithTag("yarn") != null)
        {
            target = GameObject.FindGameObjectWithTag("yarn");
        }

    }
}
