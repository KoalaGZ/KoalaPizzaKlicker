using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PapaKoalaController : MonoBehaviour {
    public Sprite bakedTeigboden;
    public Canvas papaKoalaNeeds;
    public Text papaKoalaNeedText;

    public string[] papaKoalaQuotes;

    public Order currentOrder;
    public GameObject TeigbodenPrefab;

    Coroutine orderRoutine;

    public float actionDistance = 0.05f;

    private void Start()
    {
        currentOrder = new Order(2, 2,4,false);
    }

    private void OnMouseOver()
    {
        //papaKoalaNeeds.enabled = true;
        papaKoalaNeeds.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        //papaKoalaNeeds.enabled = false;
        papaKoalaNeeds.gameObject.SetActive(false);
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


        //remove flour
        Destroy(GameController.Instance.desk.Find("Flour(Clone)").gameObject);

        //place Teigboden
        Transform teigboden = Instantiate(TeigbodenPrefab, GameController.Instance.desk.position, Quaternion.identity, GameController.Instance.desk).transform;

        //production time of teigboden    
        float duration = 1;

        Vector3 randomScale = new Vector3(Random.Range(0.95f,1.1f),Random.Range(0.95f,1.1f),1);
        float randomOversize = Random.Range(1.2f, 1.4f);

        //scale up with oversize
        for(float t=0; t<duration; t+= Time.deltaTime)
        {
            teigboden.localScale = Vector3.Lerp(Vector3.zero, randomScale* randomOversize, t / duration);

            yield return new WaitForEndOfFrame();
        }

        //scale down to real size
        duration = 0.25f;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            teigboden.localScale = Vector3.Lerp(randomScale * randomOversize, randomScale, t / duration);
            yield return new WaitForEndOfFrame();
        }


        //If order contains cheese.. get some!
        if (order.cheese > 0)
        {
            //Get Cheese
            yield return StartCoroutine(GetIngredient(GameController.Instance.cheeseStapelAtOven));

            Transform cheese = GameController.Instance.desk.Find("Cheese(Clone)");
            cheese.parent = teigboden;
            cheese.localPosition = Vector3.zero;
        }

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
        {
            //Get Tomatoes
            yield return StartCoroutine(GetIngredient(GameController.Instance.tomatoesStapelAtOven));

            Transform tomatoes = GameController.Instance.desk.Find("Tomatoes(Clone)");
            tomatoes.parent = teigboden;
            tomatoes.localPosition = Vector3.zero;
        }

        // wait for a while to visualize the change
        yield return new WaitForSeconds(1);

        //get pizza into oven
        while (Mathf.Abs(transform.position.x - GameController.Instance.ofen.position.x) > actionDistance)
        {
            float direction = GameController.Instance.ofen.position.x - transform.position.x;
            direction = Mathf.Sign(direction);
            transform.Translate(Vector3.right * direction * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        teigboden.parent = GameController.Instance.ofen;
        duration = 0.25f;
        Vector3 fromPos = teigboden.localPosition;
        Vector3 toPos = Vector3.zero;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            teigboden.localPosition = Vector3.Lerp(fromPos, Vector3.zero, t / duration);

            yield return new WaitForEndOfFrame();
        }
        teigboden.localPosition = Vector3.zero;

        //wait for pizza to bake
        yield return new WaitForSeconds(5);

        //Exchange teigboden
        teigboden.GetComponent<SpriteRenderer>().sprite = bakedTeigboden;

        //place pizza on desk

        teigboden.parent = GameController.Instance.desk;
        fromPos = teigboden.localPosition;
        toPos = Vector3.zero;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            teigboden.localPosition = Vector3.Lerp(fromPos, Vector3.zero, t / duration);

            yield return new WaitForEndOfFrame();
        }
        teigboden.localPosition = Vector3.zero;
        teigboden.localPosition = Vector3.zero;

        //wait for better visualization
        yield return new WaitForSeconds(1f);

        //Get PizzaKarton
        yield return StartCoroutine(GetIngredient(GameController.Instance.pizzaKartonStapelAtOven));


        //place baked pizza in pizza karton

        Transform pizzaKarton = GameController.Instance.desk.Find("PizzaKarton(Clone)");
        pizzaKarton.localPosition = Vector3.zero;
        teigboden.parent = pizzaKarton;
        teigboden.localPosition = Vector3.zero;
        
        //wait for better visualization
        yield return new WaitForSeconds(1f);

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
        papaKoalaNeedText.text = "Click me!";
    }


    IEnumerator GetIngredient(Transform stack)
    {
        //Go To ingredient Stack
        while (Mathf.Abs(transform.position.x - stack.position.x) > actionDistance)
        {
            float direction = stack.position.x - transform.position.x;
            direction = Mathf.Sign(direction);
            transform.Translate(Vector3.right * direction * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        //Check for ingredient and wait if unavailable
        while (stack.childCount <= 0)
        {
            papaKoalaNeedText.text = "We need:" + stack.name + "!";
            papaKoalaNeeds.gameObject.SetActive(true);
            yield return null;
        }

        if (papaKoalaQuotes.Length > 0)
            papaKoalaNeedText.text = papaKoalaQuotes[Random.Range(0, papaKoalaQuotes.Length - 1)];
        else
            papaKoalaNeedText.text = "Bella!";

        //Take ingredient
        Transform ingredient = stack.GetChild(stack.childCount - 1);
        ingredient.parent = transform;
        //ingredient.localPosition = Vector3.zero;
        float duration = 0.25f;
        //Vector3 toPos = new Vector3(Random.Range(0.95f, 1.1f), Random.Range(0.95f, 1.1f), 1);
        Vector3 fromPos = ingredient.localPosition;
        
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            ingredient.localPosition = Vector3.Lerp(fromPos,Vector3.zero, t / duration);

            yield return new WaitForEndOfFrame();
        }

        //Bring ingredient To desk
        while (Mathf.Abs(transform.position.x - GameController.Instance.desk.position.x) > actionDistance)
        {
            float direction = GameController.Instance.desk.position.x - transform.position.x;
            direction = Mathf.Sign(direction);
            transform.Translate(Vector3.right * direction * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        //place ingredient on desk
        //transform.GetChild(2).localPosition = Vector3.zero;
        //--- transform.GetChild(2).parent = GameController.Instance.desk;
        ingredient.parent = GameController.Instance.desk;
        fromPos = ingredient.localPosition;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            ingredient.localPosition = Vector3.Lerp(fromPos, Vector3.zero, t / duration);

            yield return new WaitForEndOfFrame();
        }

    }
}
