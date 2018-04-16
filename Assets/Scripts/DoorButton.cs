using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorButton : MonoBehaviour
{
    public GameObject IndoorCam;
    public GameObject OutdoorCam;
    private void OnMouseDown()
    {
        Debug.Log("indoor:" + IndoorCam.activeSelf + "<color=red>    outdoor:</color> " + OutdoorCam.activeSelf);
        if(IndoorCam.activeSelf)
        {
            IndoorCam.SetActive(false);
            OutdoorCam.SetActive(true);
        }
        else
        {
            OutdoorCam.SetActive(false);
            IndoorCam.SetActive(true);
        }
    }
}
