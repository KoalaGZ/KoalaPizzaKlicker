using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaController : MonoBehaviour {
    bool indoor = true;
    public Vector3 destination;
    public enum Task
    {
        ChoseTask,
        GetInside,

        AskGuestForOrder,
        TellPapaKoalaOrder,
        ServePizzaToGuest,

        GetOutside,

        TakePizzaKarton,
        BringPizzaKartonToOven,

        TakeSalami,
        BringSalamiToFridge,
        
        TakeCheese,
        BringCheeseToFridge,

        TakeTomatoes,
        BringTomatoesToFridge,

        TakeFlour,
        BringFlourToDesk,

        TakeWine,
        BringWineToShelf,

        GetOnMoped,
        DriveMopedToWindow,
        DeliverPizza,


    }

    public Task currentTask = Task.ChoseTask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        switch (currentTask)
        {
            case Task.ChoseTask:

                if (indoor)
                {
                    //logic for... serve guests 
                    if (transform.childCount != 0)
                    {
                        if(transform.GetChild(0).name == "PizzaKarton(Clone)") 
                            currentTask = Task.BringPizzaKartonToOven;
                        if (transform.GetChild(0).name == "Salami(Clone)")
                            currentTask = Task.BringSalamiToFridge;
                        if (transform.GetChild(0).name == "Cheese(Clone)")
                            currentTask = Task.BringCheeseToFridge;
                        if (transform.GetChild(0).name == "Tomatoes(Clone)")
                            currentTask = Task.BringTomatoesToFridge;
                        if (transform.GetChild(0).name == "Flour(Clone)")
                            currentTask = Task.BringFlourToDesk;
                    }
                    else
                    {
                        currentTask = Task.GetOutside;
                    }
                }

                if (!indoor)
                {
                    //logic for... take supplies (pizza kartons, salami etc) and bring it inside 
                    if(GameController.Instance.pizzaKartonSupply.transform.childCount > 0)
                    {
                        currentTask = Task.BringPizzaKartonToOven;
                    }
                    if (GameController.Instance.salamiSupply.transform.childCount > 0)
                    {
                        currentTask = Task.BringSalamiToFridge;
                    }
                    if (GameController.Instance.cheeseSupply.transform.childCount > 0)
                    {
                        currentTask = Task.BringCheeseToFridge;
                    }
                    if (GameController.Instance.tomatoesSupply.transform.childCount > 0)
                    {
                        currentTask = Task.BringTomatoesToFridge;
                    }
                    if (GameController.Instance.flourSupply.transform.childCount > 0)
                    {
                        currentTask = Task.BringFlourToDesk;
                    }



                    if (GameController.Instance.mopeds > 0) // moped available?
                        currentTask = Task.GetOnMoped;
                }

                break;

            case Task.GetOutside:
                if (indoor)
                {
                    if (transform.position.x > KoalaSpawner.Instance.IndoorDoor.position.x)
                        transform.Translate(Vector3.left * Time.deltaTime);
                    else
                    {
                        indoor = false;
                        transform.position = KoalaSpawner.Instance.OutdoorDoor.position;
                        currentTask = Task.ChoseTask;
                    }
                }
                break;

            case Task.GetInside:
                if (!indoor)
                {
                    if (transform.position.x < KoalaSpawner.Instance.OutdoorDoor.position.x)
                        transform.Translate(Vector3.right * Time.deltaTime);
                    else
                    {
                        indoor = true;
                        transform.position = KoalaSpawner.Instance.IndoorDoor.position;
                        currentTask = Task.ChoseTask;
                    }

                }


                    break;

            case Task.GetOnMoped:
                if(!indoor && GameController.Instance.mopeds > 0)
                {
                    //go to moped spawn
                    if (transform.position.x < MopedSpawner.Instance.transform.position.x + MopedSpawner.Instance.spawnOffset.x)
                        transform.Translate(Vector3.right * Time.deltaTime);
                    else
                    {
                        Transform child = MopedSpawner.Instance.transform.GetChild(MopedSpawner.Instance.transform.childCount-1);
                        if(child != null)
                        {
                            GameController.Instance.mopeds--;
                            transform.parent = child;

                            transform.parent.parent = MopedSpawner.Instance.koalaMopedsContainer;
                            transform.localPosition = transform.parent.GetChild(0).localPosition;
                            transform.localRotation = transform.parent.GetChild(0).localRotation;
                            transform.parent.Rotate(Vector3.up, 180);
                            currentTask = Task.DriveMopedToWindow;
                        }
                        else
                        {
                            currentTask = Task.ChoseTask;
                        }
                    }
                }
                break;

            case Task.DriveMopedToWindow:
                if (transform.parent.position.x > GameController.Instance.Window.position.x)
                    transform.parent.position += Vector3.left * Time.deltaTime;
                else
                {
                    if (GameController.Instance.Window.childCount > 0)
                    {
                        Transform deliveryPizza = GameController.Instance.Window.GetChild(0);
                        deliveryPizza.parent = transform;
                        deliveryPizza.localPosition = Vector3.zero;
                        transform.parent.Rotate(Vector3.up, -180);
                        currentTask = Task.DeliverPizza;
                    }
                }
                break;

            case Task.DeliverPizza:
                if (transform.position.x < 12)
                    transform.parent.Translate(Vector3.right * Time.deltaTime);
                else
                {
                    transform.parent.Rotate(Vector3.up, 180);
                    currentTask = Task.DriveMopedToWindow;
                }
                break;

            case Task.BringPizzaKartonToOven:
                if (!indoor)
                {
                    if (transform.childCount == 0)
                    {
                        if (GameController.Instance.pizzaKartonSupply.transform.childCount > 0)
                        {
                            if (transform.position.x > GameController.Instance.pizzaKartonSupply.transform.position.x)
                            {
                                transform.position += Vector3.left * Time.deltaTime;
                            }
                            else if(GameController.Instance.pizzaKartonSupply.transform.childCount > 0)
                            {
                                GameController.Instance.pizzaKartonSupply.transform.GetChild(GameController.Instance.pizzaKartonSupply.transform.childCount - 1).parent = transform;
                            }
                        }
                    }
                    else
                    {
                        currentTask = Task.GetInside;
                    }
                }
                if (indoor)
                {
                    if(transform.position.x < GameController.Instance.pizzaKartonStapelAtOven.position.x)
                    {
                        transform.Translate(Vector3.right * Time.deltaTime);
                    }
                    else
                    {
                        transform.GetChild(0).localScale = Vector3.one;
                        Transform karton = transform.GetChild(0);
                        karton.rotation = Quaternion.identity;
                        transform.GetChild(0).parent = GameController.Instance.pizzaKartonStapelAtOven;
                        karton.localPosition = Vector3.up * GameController.Instance.pizzaKartonStapelAtOven.childCount * 0.25f;
                        currentTask = Task.ChoseTask;
                    }
                }

                break;

            case Task.BringSalamiToFridge:
                if (!indoor)
                {
                    if (transform.childCount == 0)
                    {
                        if (GameController.Instance.salamiSupply.transform.childCount > 0)
                        {
                            if (transform.position.x > GameController.Instance.salamiSupply.transform.position.x)
                            {
                                transform.position += Vector3.left * Time.deltaTime;
                            }
                            else if (GameController.Instance.salamiSupply.transform.childCount > 0)
                            {
                                GameController.Instance.salamiSupply.transform.GetChild(GameController.Instance.salamiSupply.transform.childCount - 1).parent = transform;
                            }
                        }
                    }
                    else
                    {
                        currentTask = Task.GetInside;
                    }
                }
                if (indoor)
                {
                    if (transform.position.x < GameController.Instance.salamiStapelAtOven.position.x)
                    {
                        transform.Translate(Vector3.right * Time.deltaTime);
                    }
                    else
                    {
                        transform.GetChild(0).localScale = Vector3.one*0.5f;
                        Transform salami = transform.GetChild(0);
                        salami.rotation = Quaternion.identity;
                        transform.GetChild(0).parent = GameController.Instance.salamiStapelAtOven;
                        salami.localPosition = Vector3.up * GameController.Instance.salamiStapelAtOven.childCount * 0.25f;
                        currentTask = Task.ChoseTask;
                    }
                }

                break;

            case Task.BringCheeseToFridge:
                if (!indoor)
                {
                    if (transform.childCount == 0)
                    {
                        if (GameController.Instance.cheeseSupply.transform.childCount > 0)
                        {
                            if (transform.position.x > GameController.Instance.cheeseSupply.transform.position.x)
                            {
                                transform.position += Vector3.left * Time.deltaTime;
                            }
                            else if (GameController.Instance.cheeseSupply.transform.childCount > 0)
                            {
                                GameController.Instance.cheeseSupply.transform.GetChild(GameController.Instance.cheeseSupply.transform.childCount - 1).parent = transform;
                            }
                        }
                    }
                    else
                    {
                        currentTask = Task.GetInside;
                    }
                }
                if (indoor)
                {
                    if (transform.position.x < GameController.Instance.cheeseStapelAtOven.position.x)
                    {
                        transform.Translate(Vector3.right * Time.deltaTime);
                    }
                    else
                    {
                        transform.GetChild(0).localScale = Vector3.one * 0.5f;
                        Transform cheese = transform.GetChild(0);
                        cheese.rotation = Quaternion.identity;
                        transform.GetChild(0).parent = GameController.Instance.cheeseStapelAtOven;
                        cheese.localPosition = Vector3.up * GameController.Instance.cheeseStapelAtOven.childCount * 0.25f;
                        currentTask = Task.ChoseTask;
                    }
                }

                break;

            case Task.BringFlourToDesk:
                if (!indoor)
                {
                    if (transform.childCount == 0)
                    {
                        if (GameController.Instance.flourSupply.transform.childCount > 0)
                        {
                            if (transform.position.x > GameController.Instance.flourSupply.transform.position.x)
                            {
                                transform.position += Vector3.left * Time.deltaTime;
                            }
                            else if (GameController.Instance.flourSupply.transform.childCount > 0)
                            {
                                GameController.Instance.flourSupply.transform.GetChild(GameController.Instance.flourSupply.transform.childCount - 1).parent = transform;
                            }
                        }
                    }
                    else
                    {
                        currentTask = Task.GetInside;
                    }
                }
                if (indoor)
                {
                    if (transform.position.x < GameController.Instance.flourStapelAtOven.position.x)
                    {
                        transform.Translate(Vector3.right * Time.deltaTime);
                    }
                    else
                    {
                        transform.GetChild(0).localScale = Vector3.one * 0.5f;
                        Transform flour = transform.GetChild(0);
                        flour.rotation = Quaternion.identity;
                        transform.GetChild(0).parent = GameController.Instance.flourStapelAtOven;
                        flour.localPosition = Vector3.up * GameController.Instance.flourStapelAtOven.childCount * 0.25f;
                        currentTask = Task.ChoseTask;
                    }
                }

                break;
        }
        
        
	}


}
