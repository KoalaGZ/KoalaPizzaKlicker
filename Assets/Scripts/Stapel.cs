using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stapel : MonoBehaviour {

    public GameObject pizzaKartonPrefab;
    int kartonOffset;

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
        Instantiate(pizzaKartonPrefab, transform.position + kartonOffset * Vector3.right, Quaternion.identity, transform);
    }

    public void kartonRotation()
    {
        
            
    }

    public void RemoveElement()
    {
        if (!pizzaKartonPrefab)
        {
            Destroy(transform.GetChild(transform.childCount).gameObject);
        }
    }
    private void Update()
    {
        kartonOffset = transform.childCount;
    }
}
