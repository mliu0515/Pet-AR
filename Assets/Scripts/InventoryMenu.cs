using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject InvPanel;
    public TapToPlaceObject ObjectController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseFirst() {
        ObjectController.objNumber = 0;
        ObjectController.RemoveCurrentItem();
        Debug.Log("Selected yarn");
        InvPanel.SetActive(false);
    }

    public void ChooseSecond() {
        ObjectController.objNumber = 1;
        ObjectController.RemoveCurrentItem();
        Debug.Log("Selected fish");
        InvPanel.SetActive(false);
    }
}
