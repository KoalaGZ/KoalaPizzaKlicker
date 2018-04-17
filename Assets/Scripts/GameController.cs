using System.Collections;
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

    public Transform Window;

    #endregion

    #region supply stapel
    public Stapel pizzaKartonSupply;
    #endregion

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
}
