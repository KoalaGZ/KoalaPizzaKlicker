using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapaKoalaController : MonoBehaviour {
    public Sprite bakedTeigboden;
    public Canvas papaKoalaNeeds;

    public Order currentOrder;
    public GameObject TeigbodenPrefab;

    Coroutine orderRoutine;

    public float actionDistance = 0.05f;

    private void Start()
    {
        currentOrder = new Order(2, 0, 0,false);
    }

    private void OnMouseOver()
    {
        papaKoalaNeeds.enabled = true;
    }

    private void OnMouseExit()
    {
        papaKoalaNeeds.enabled = false;
    }

    private void OnMouseDown()
    {
        if (orderRoutine == null && currentOrder != null)
            orderRoutine = StartCoroutine(ProcessOrder(currentOrder));
    }

    IEnumerator ProcessOrder(Order order)
    {
        //Get Flour
        yield return StartCoroutine(GetIngredient(GameController.Instance.flourStapelAtOven));

        //wait production time of teigboden    
        yield return new WaitForSeconds(2);
        //remove flour
        Destroy(GameController.Instance.desk.Find("Flour(Clone)").gameObject);
        //make Teigboden
        Transform teigboden = Instantiate(TeigbodenPrefab, GameController.Instance.desk.position, Quaternion.identity, GameController.Instance.desk).transform;

        //If order contains cheese.. get some!
        if (order.cheese > 0)
            //Get Salami
            StartCoroutine(GetIngredient(GameController.Instance.cheeseStapelAtOven));

        //If order contains salami.. get some! and make ingredients children of teigboden
        if (order.salami > 0)
        {
            //Get Salami
            yield return StartCoroutine(GetIngredient(GameController.Instance.salamiStapelAtOven));

            Transform salami = GameController.Instance.desk.Find("Salami(Clone)");
            salami.parent = teigboden;
            salami.localPosition = Vector3.zero;
        }

        //If order contains tomatoes.. get some!
        if (order.tomatoes > 0)
            //Get Salami
            yield return StartCoroutine(GetIngredient(GameController.Instance.tomatoesStapelAtOven));
        

        //Transform cheese = GameController.Instance.desk.Find("Cheese(Clone)");
        //cheese.parent = teigboden;
        //cheese.localPosition = Vector3.zero;

        //Transform tomatoe = GameController.Instance.desk.Find("Tomatoe(Clone)");
        //tomatoe.parent = teigboden;
        //tomatoe.localPosition = Vector3.zero;

        //bring pizza into oven
        while (Mathf.Abs(transform.position.x - GameController.Instance.ofen.position.x) > actionDistance)
        {
            float direction = GameController.Instance.ofen.position.x - transform.position.x;
            direction = Mathf.Sign(direction);
            transform.Translate(Vector3.right * direction * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        teigboden.parent = GameController.Instance.ofen;
        teigboden.localPosition = Vector3.zero;

        //wait for pizza to bake
        yield return new WaitForSeconds(10);

        //Exchange teigboden
        teigboden.GetComponent<SpriteRenderer>().sprite = bakedTeigboden;

        //Get PizzaKarton
        yield return StartCoroutine(GetIngredient(GameController.Instance.pizzaKartonStapelAtOven));

        //place baked pizza in pizza karton

        Transform pizzaKarton = GameController.Instance.desk.Find("PizzaKarton(Clone)");
        teigboden.parent = pizzaKarton;
        teigboden.localPosition = Vector3.zero;

        //take pizza karton
        pizzaKarton.parent = transform;
        pizzaKarton.localPosition = Vector3.zero;

        if(order.inStore)
        {
            //go with baked pizza to kasse
            while (Mathf.Abs(transform.position.x - GameController.Instance.kasse.position.x) > actionDistance)
            {
                float direction = GameController.Instance.kasse.position.x - transform.position.x;
                direction = Mathf.Sign(direction);
                transform.Translate(Vector3.right * direction * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            //go with baked pizza to window
            while (Mathf.Abs(transform.position.x - GameController.Instance.indoorWindow.position.x) > actionDistance)
            {
                float direction = GameController.Instance.indoorWindow.position.x - transform.position.x;
                direction = Mathf.Sign(direction);
                transform.Translate(Vector3.right * direction * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }
            pizzaKarton.parent = GameController.Instance.Window;
            pizzaKarton.localPosition = Vector3.zero;


        }



        orderRoutine = null;
    }


    IEnumerator GetIngredient(Transform stack)
    {
        //Go To Flour Stack
        while (Mathf.Abs(transform.position.x - stack.position.x) > actionDistance)
        {
            float direction = stack.position.x - transform.position.x;
            direction = Mathf.Sign(direction);
            transform.Translate(Vector3.right * direction * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }
        Debug.Log("hier");
        //Check for flour and wait if unavailable
        while (stack.childCount <= 0)
        {
            yield return null;
        }
        //Take Flour
        stack.GetChild(stack.childCount - 1).parent = transform;

        //Bring Flour To desk
        while (Mathf.Abs(transform.position.x - GameController.Instance.desk.position.x) > actionDistance)
        {
            float direction = GameController.Instance.desk.position.x - transform.position.x;
            direction = Mathf.Sign(direction);
            transform.Translate(Vector3.right * direction * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        //place flour on desk
        transform.GetChild(2).localPosition = Vector3.zero;
        transform.GetChild(2).parent = GameController.Instance.desk;

    }
}
