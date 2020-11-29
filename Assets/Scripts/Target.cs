using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;   //to be called from the CatAI
    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position;

    }
}
