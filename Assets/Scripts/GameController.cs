using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController Instance;
    
    float Money = 10000.0f;
    public int Koalas = 0;
    int Oefen = 1;

    int frameCount;
    int frameRange = 30;

    public Text moneyText;
    public Text koalaText;

    // Use this for initialization
    void Awake () {
        DontDestroyOnLoad(this);
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        if (moneyText != null && koalaText != null)
        {
            moneyText.text = "Money: " + Money;
            koalaText.text = "Koalas: " + Koalas;
        }
    }
	
	// Update is called once per frame
	void Update () {
        frameCount++;

        if(moneyText != null && koalaText != null)
        {
            moneyText.text = "Money: " + Money;
            koalaText.text = "Koalas: " + Koalas;
        }

        if (frameCount >= frameRange)
        {
            Money += 2.50f * Koalas;
           
            frameCount = 0;

        }

        
        
	}

    public void BuyKoala()
    {
        if (Money >= 100)
        {
            Koalas += 1;
            Money -= 100;
            KoalaSpawner.Instance.SpawnKoala();
        }
    }

    public void Buy100Koalas()
    {
        if (Money >= 1000)
        {
            Koalas += 100;
            Money -= 1000;
            for(int i = 0; i<100; i++)
                KoalaSpawner.Instance.SpawnKoala();
        }
    }
}
