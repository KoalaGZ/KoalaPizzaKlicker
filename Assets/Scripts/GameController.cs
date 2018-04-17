﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController Instance;
        

    float money = 10000.0f;
    public int koalas = 0;
    //int oefen = 1; ??
    public int mopeds = 0;
    public int koalaMopeds = 0;
    public int pizzaKartons = 0;

    int frameCount;
    int frameRange = 30;

    public Text MoneyText;
    public Text KoalaText;


    public int koalaPrice = 100;
    
    #region moped
    public int mopedPrice = 100;
    public int pizzakartonPrice = 10;

    public int salamiPrice = 10;
    public int cheesePrice = 10;
    public int tomatoesPrice = 10;
    public int flourPrice = 10;

    //Price that player defines
    public int salamiSellPrice = 20;
    public int cheeseSellPrice = 20;
    public int tomatoesSellPrice = 20;
    public int flourSellPrice = 20;
    public int pizzaKartonSellPrice = 20;

    public GameObject deliveryVanPrefab;
    public Transform Window;

    #endregion

    #region supply stapel

    public Stapel flourSupply;
    public Transform flourStapelAtOven;

    public Stapel pizzaKartonSupply;
    public Transform pizzaKartonStapelAtOven;

    public Stapel salamiSupply;
    public Transform salamiStapelAtOven;


    public Stapel cheeseSupply;
    public Transform cheeseStapelAtOven;

    public Stapel tomatoesSupply;
    public Transform tomatoesStapelAtOven;


    public GameObject pizzaKartonPrefab;
    public GameObject salamiPrefab;
    public GameObject cheesePrefab;
    public GameObject tomatoesPrefab;
    public GameObject flourPrefab;

    #endregion

    public Transform desk;

    public Transform kasse;

    public Transform ofen;

    public Transform indoorWindow;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        if (MoneyText != null && KoalaText != null)
        {
            MoneyText.text = "Money: " + money;
            KoalaText.text = "Koalas: " + koalas;
        }
    }
	
	// Update is called once per frame
	void Update () {
        frameCount++;

        if(MoneyText != null && KoalaText != null)
        {
            MoneyText.text = "Money: " + money;
            KoalaText.text = "Koalas: " + koalas;
        }

        if (frameCount >= frameRange)
        {
            money += 2.50f * koalas;
           
            frameCount = 0;

        }

        
        
	}

    public void BuyKoala()
    {
        if (money >= koalaPrice)
        {
            koalas += 1;
            money -= koalaPrice;
            KoalaSpawner.Instance.SpawnKoala();
        }
    }

    public void Buy100Koalas()
    {
        if (money >= koalaPrice * 100)
        {
            koalas += 100;
            money -= koalaPrice * 100;
            for(int i = 0; i<100; i++)
                KoalaSpawner.Instance.SpawnKoala();
        }
    }

    public void BuyMoped()
    {
        if(money >= mopedPrice)
        {
            mopeds++;
            money -= mopedPrice;
            MopedSpawner.Instance.SpawnMoped();
        }
    }

    public void BuyPizzaKarton()
    {
        if(money >= pizzakartonPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = pizzaKartonSupply;
            money -= pizzakartonPrice;
        }
    }

    public void BuySalami()
    {
        if (money >= salamiPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = salamiSupply;
            money -= salamiPrice;
        }
    }
    public void BuyCheese()
    {
        if (money >= cheesePrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = cheeseSupply;
            money -= cheesePrice;
        }
    }

    public void BuyFlour()
    {
        if (money >= flourPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = flourSupply;
            money -= flourPrice;
        }
    }

    public void BuyTomatoes()
    {
        if (money >= tomatoesPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = tomatoesSupply;
            money -= tomatoesPrice;
        }
    }
}
