using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapel : MonoBehaviour {
    GameObject stapelObjectPrefab;
    int objectOffset;
    int frameCount;
    float duration;
    public float turnSpeed;

    public enum Type
    {
        PizzaKarton,
        Salami,
        Cheese,
        Flour,
        Tomatoes
    }
    public Type type;

    public int amount = 0;

    public void AddElement()
    {
        Transform newElement =  Instantiate(stapelObjectPrefab, transform.position + objectOffset * new Vector3(0.1f, 0f, 0f), Quaternion.identity, transform).transform;
        StartCoroutine(ObjectRotation(newElement));
    }

    IEnumerator ObjectRotation(Transform tKarton)
    {
        for(float t = 0; t < duration; t ++)
        {
            tKarton.Rotate(Vector3.forward * turnSpeed * Time.deltaTime, t);
            yield return new WaitForEndOfFrame();
        }        
            
    }

    private void Start()
    {
        duration = 270f;
        turnSpeed = 1f;
        switch (type)
        {
            case Type.PizzaKarton:
                stapelObjectPrefab = GameController.Instance.pizzaKartonPrefab;
                break;
            case Type.Salami:
                stapelObjectPrefab = GameController.Instance.salamiPrefab;
                break;

            case Type.Cheese:
                stapelObjectPrefab = GameController.Instance.cheesePrefab;
                break;

            case Type.Flour:
                stapelObjectPrefab = GameController.Instance.flourPrefab;
                break;

            case Type.Tomatoes:
                stapelObjectPrefab = GameController.Instance.tomatoesPrefab;
                break;
        }
    }
    private void Update()
    {
        objectOffset = transform.childCount;
        frameCount++;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddElement();
        }
    }
}
