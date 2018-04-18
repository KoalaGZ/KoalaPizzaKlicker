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

    public GameObject IndoorCam;
    public GameObject OutdoorCam;
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

        if(MoneyText != null && KoalaText != null)
        {
            MoneyText.text = "Money: " + money;
            KoalaText.text = "Koalas: " + koalas;
        }

        //frameCount++;
        //if (frameCount >= frameRange)
        //{
        //    money += 2.50f * koalas;
           
        //    frameCount = 0;

        //}

        
        
	}

    public void BuyKoala()
    {
        if (money >= koalaPrice)
        {
            koalas += 1;
            money -= koalaPrice;
            SpendMoney(koalaPrice);
            KoalaSpawner.Instance.SpawnKoala();
        }
    }

    public void Buy100Koalas()
    {
        if (money >= koalaPrice * 100)
        {
            koalas += 100;
            money -= koalaPrice * 100;
            SpendMoney(koalaPrice * 100);
            for (int i = 0; i<100; i++)
                KoalaSpawner.Instance.SpawnKoala();
        }
    }

    public void BuyMoped()
    {
        if(money >= mopedPrice)
        {
            mopeds++;
            money -= mopedPrice;
            SpendMoney(mopedPrice);
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
            SpendMoney(pizzakartonPrice);
        }
    }

    public void BuySalami()
    {
        if (money >= salamiPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = salamiSupply;
            money -= salamiPrice;
            SpendMoney(salamiPrice);
        }
    }
    public void BuyCheese()
    {
        if (money >= cheesePrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = cheeseSupply;
            money -= cheesePrice;
            SpendMoney(cheesePrice);
        }
    }

    public void BuyFlour()
    {
        if (money >= flourPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = flourSupply;
            money -= flourPrice;
            SpendMoney(flourPrice);
        }
    }

    public void BuyTomatoes()
    {
        if (money >= tomatoesPrice)
        {
            DeliveryVan van = Instantiate(deliveryVanPrefab, new Vector3(12, 0, 0), Quaternion.identity).GetComponent<DeliveryVan>();
            van.stapel = tomatoesSupply;
            money -= tomatoesPrice;
            SpendMoney(tomatoesPrice);
        }
    }
    [Header("--UI--")]
    public GameObject[] ingredientPopupButtons;
    public GameObject[] materialsPopupButtons;

    public void UiIngredientsPopup()
    {
        //activate/deactive the sub buttons
        foreach(GameObject sb in ingredientPopupButtons)
        {
            sb.SetActive(!sb.activeSelf);
        }
    }
    public void UiMaterialsPopup()
    {
        //activate/deactive the sub buttons
        foreach (GameObject sb in materialsPopupButtons)
        {
            sb.SetActive(!sb.activeSelf);
        }
    }

    void SpendMoney(int amount)
    {
        for(int i=0; i<amount; i++)
        {
            StartCoroutine(SpendKoalaDollarRoutine());
        }
    }

    public GameObject koalaDollarPrefab;
    IEnumerator SpendKoalaDollarRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 0.3f));
        Vector3 moneyPosition = new Vector3();
        if (OutdoorCam.activeSelf)
             moneyPosition = new Vector3(Random.Range(7f,7.1f), 2.3f, 0); //Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        if(IndoorCam.activeSelf)
            moneyPosition = new Vector3(Random.Range(0.8f, 0.9f), 1.08f, 0); //Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));

        Transform koalaDollar = Instantiate(koalaDollarPrefab, moneyPosition, Quaternion.identity, transform).transform;
        float velocityX = Random.Range(0, 2)-1;
        float velocityY = Random.Range(0, 1);
        float speed = Random.Range(1f,1.6f);
        while (koalaDollar.position.y > -1.1f)
        {
            koalaDollar.position +=(Vector3.down * velocityY * speed + Vector3.right * velocityX*speed*2f + Vector3.down*4f)* Time.deltaTime;
            
            yield return new WaitForEndOfFrame();
        }

        Destroy(koalaDollar.gameObject);
    }
}
