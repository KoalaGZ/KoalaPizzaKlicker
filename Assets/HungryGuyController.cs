using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryGuyController : MonoBehaviour
{
    public float doorOffset = 0.5f;
    public bool onDoor = false;
    public int direction = 1;

    public enum Task
    {
        ChoseTask,
        GetInside,
        GetOutside,
    }

    bool indoor = false;

    public Task currentTask = Task.GetInside;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        switch (currentTask)
        {
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
                    //left from door
                    if (transform.position.x < KoalaSpawner.Instance.OutdoorDoor.position.x) { 
                        direction = -1;
                        onDoor = false;
                    }
                //hab ich geqaddet, weil die wenn die rechts vom eingang waren nicht zur tür gelaufen sind, sondern direkt drin waren --> ELSE
                //tight from door
                else if (transform.position.x > KoalaSpawner.Instance.OutdoorDoor.position.x) { 
                        direction = 1;
                        onDoor = false;
                    }
                    if (transform.position.x != KoalaSpawner.Instance.OutdoorDoor.position.x) {
                        transform.Translate(Vector3.left * Time.deltaTime * direction);
                            }
                    else
                    {
                        indoor = true;
                        transform.position = KoalaSpawner.Instance.IndoorDoor.position;
                        currentTask = Task.ChoseTask;
                    }

                }


                break;
        }
    }

}