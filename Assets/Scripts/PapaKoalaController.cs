using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapaKoalaController : MonoBehaviour {

    public Canvas papaKoalaNeeds;

    private void OnMouseOver()
    {
        papaKoalaNeeds.enabled = true;
    }

    private void OnMouseExit()
    {
        papaKoalaNeeds.enabled = false;
    }

    
}
