using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour {
    float speed = 4f;
    float rightcornerX=8;
    float oldX;
	// Use this for initialization
	void Start () {
        oldX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.right * speed * Time.deltaTime;
        if (transform.position.x > rightcornerX)
            transform.position = new Vector3(oldX,transform.position.y, transform.position.z);
	}
}
