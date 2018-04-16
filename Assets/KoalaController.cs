using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaController : MonoBehaviour {
    bool indoor = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (indoor)
        {
            if (transform.position.x > KoalaSpawner.Instance.IndoorDoor.position.x)
                transform.Translate(Vector3.left * Time.deltaTime);
            else
            {
                indoor = false;
                transform.position = KoalaSpawner.Instance.OutdoorDoor.position;
            }
        }
        else
        {
            if (transform.position.x < KoalaSpawner.Instance.Moped.position.x)
                transform.Translate(Vector3.right * Time.deltaTime);
            else
            {
            }
        }
	}
}
