using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    float Money = 100.0f;
    int Koalas = 1;
    int Öfen = 1;

    int frameCount;
    int frameRange = 30;

    public Text moneyText;
    public Text koalaText;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        frameCount++;

        moneyText.text = "Money: " + Money;
        koalaText.text = "Koalas: " + Koalas;

        if (frameCount >= frameRange)
        {
            Money += 2.50f * Koalas;
           
            frameCount = 0;

        }

        
        
	}

    public void BuyKoala()
    {
        if (Money > 100)
        {
            Koalas += 1;
            Money -= 100;
        }
    }

    public void Buy100Koalas()
    {
        if (Money > 1000)
        {
            Koalas += 100;
            Money -= 1000;
        }
    }
}
