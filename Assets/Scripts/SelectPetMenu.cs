using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPetMenu : MonoBehaviour
{
    public GameObject SPP;
    public TapToPlaceObject ObjectController;

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseCat() {
        ObjectController.modelNumber = 0;
        ObjectController.RemoveCurrentPet();
        Debug.Log("Selected Cat");
        SPP.SetActive(false);
    }

    public void ChooseToilet() {
        ObjectController.modelNumber = 1;
        ObjectController.RemoveCurrentPet();
        Debug.Log("Selected Toilet");
        SPP.SetActive(false);
    }

    public void ChooseRock() {
        ObjectController.modelNumber = 2;
        ObjectController.RemoveCurrentPet();
        Debug.Log("Selected Rock");
        SPP.SetActive(false);
    }


}
