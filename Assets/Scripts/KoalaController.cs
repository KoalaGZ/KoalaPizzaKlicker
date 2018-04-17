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

                        //currentTask = Task.BringSalamiToFridge;
                    }
                    else
                    {
                        currentTask = Task.GetOutside;
                    }
                }

                if (!indoor)
                {
                    //logic for... take supplies (pizza kartons, salami etc) and bring it inside 
                    if(GameController.Instance.pizzaKartonSupply.amount > 0)
                    {
                        currentTask = Task.BringPizzaKartonToOven;
                    }
                    //else
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
                    }
                }
                break;

            case Task.DriveMopedToWindow:
                if (transform.parent.position.x > GameController.Instance.Window.position.x)
                    transform.parent.position += Vector3.left * Time.deltaTime;
                else
                {

                }
                break;

            case Task.BringPizzaKartonToOven:
                if (!indoor)
                {
                    if (transform.childCount == 0)
                    {
                        if (GameController.Instance.pizzaKartonSupply.amount > 0)
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
        }
        
        
	}


}
