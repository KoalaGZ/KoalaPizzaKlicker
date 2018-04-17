using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapel : MonoBehaviour {

    public GameObject pizzaKartonPrefab;
    int kartonOffset;
    int frameCount;
    float duration;
    public float turnSpeed;

    public enum Type
    {
        PizzaKarton,
        Salami,
        Cheese,
        Flour
    }
    public Type type;

    public int amount = 0;

    public void AddElement()
    {
        Transform newElement =  Instantiate(pizzaKartonPrefab, transform.position + kartonOffset * new Vector3(0.1f, 0f, 0f), Quaternion.identity, transform).transform;
        StartCoroutine(kartonRotation(newElement));
    }

    IEnumerator kartonRotation(Transform tKarton)
    {
        for(float t = 0; t < duration; t ++)
        {
            tKarton.Rotate(Vector3.forward * turnSpeed * Time.deltaTime, t);
            yield return new WaitForEndOfFrame();
        }        
            
    }

    public void RemoveElement()
    {
        if (!pizzaKartonPrefab)
        {
            Destroy(transform.GetChild(transform.childCount).gameObject);
        }
    }
    private void Start()
    {
        duration = 270f;
        turnSpeed = 1f;
    }
    private void Update()
    {
        kartonOffset = transform.childCount;
        frameCount++;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            AddElement();
        }
    }
}
