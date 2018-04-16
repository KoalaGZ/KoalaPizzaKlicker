using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaSpawner : MonoBehaviour {
    public static KoalaSpawner Instance;
    int oldKoalas;
    public GameObject koalaPrefab;
    public Transform IndoorDoor;
    public Transform OutdoorDoor;
    public Transform Moped;
    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        oldKoalas = GameController.Instance.Koalas;
    }
    private void Update()
    {
        //if(GameController.Instance.Koalas > oldKoalas)
        //{
        //    int newKoalaAmount = GameController.Instance.Koalas - oldKoalas;
        //    for (int i = 0; i<newKoalaAmount; i++)
        //    {
        //        SpawnKoala();
        //    }
        //    oldKoalas = GameController.Instance.Koalas;
        //}
    }

    public void SpawnKoala()
    {
        Instantiate(koalaPrefab, transform.position+ Vector3.right * Random.Range(0.75f,3f), Quaternion.identity, transform);
    }
}
