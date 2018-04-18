using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorButton : MonoBehaviour
{
    private void OnMouseDown()
    {
       // Debug.Log("indoor:" + GameController.Instance.IndoorCam.activeSelf + "<color=red>    outdoor:</color> " + GameController.Instance.OutdoorCam.activeSelf);
        if(GameController.Instance.IndoorCam.activeSelf)
        {
            GameController.Instance.IndoorCam.SetActive(false);
            GameController.Instance.OutdoorCam.SetActive(true);
        }
        else
        {
            GameController.Instance.OutdoorCam.SetActive(false);
            GameController.Instance.IndoorCam.SetActive(true);
        }
    }
}
