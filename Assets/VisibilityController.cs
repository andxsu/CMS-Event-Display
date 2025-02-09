using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VisibilityController : MonoBehaviour
{
    public GameObject TEC;

    public GameObject PixelEndcap;

    public GameObject PixelBarrel;

    public GameObject CSC;

    
    public void TECBtn()
    { //Get current State
        bool currentState = TEC.activeSelf;

        //Flip it
        currentState = !currentState;

        //Set the current State to the flipped value
        TEC.SetActive(currentState);
        EventSystem.current.SetSelectedGameObject(null); //fix for the weird button remaining selected problem
    }


    public void PixelEndBtn()
    { //Get current State
        bool currentState1 = PixelEndcap.activeSelf;

        //Flip it
        currentState1 = !currentState1;

        //Set the current State to the flipped value
        PixelEndcap.SetActive(currentState1);
        EventSystem.current.SetSelectedGameObject(null); //fix for the weird button remaining selected problem
    }

    public void PixelBarBtn()
    { //Get current State
        bool currentState2 = PixelBarrel.activeSelf;

        //Flip it
        currentState2 = !currentState2;

        //Set the current State to the flipped value
        PixelBarrel.SetActive(currentState2);
        EventSystem.current.SetSelectedGameObject(null); //fix for the weird button remaining selected problem
    }

    public void CSCBtn()
    { //Get current State
        bool currentState3 = CSC.activeSelf;

        //Flip it
        currentState3 = !currentState3;

        //Set the current State to the flipped value
        CSC.SetActive(currentState3);
        EventSystem.current.SetSelectedGameObject(null); //fix for the weird button remaining selected problem
    }


}
