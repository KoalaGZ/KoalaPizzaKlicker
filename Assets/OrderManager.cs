using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour {
    public Order[] orders;

}
public class Order
{
    //every order contains 1 pizza karton & 1 flour + individual combinations of the following fields:
    public int salami;
    public int cheese;
    public int tomatoes;

    public bool inStore;

    public Order(int salamiAmount, int cheeseAmount, int tomatoesAmount, bool orderInStore)
    {
        //(Pizzakarton + Flour) + Salami + Cheese  + Tomatoes 
        salami = salamiAmount;
        cheese = cheeseAmount;
        tomatoes = tomatoesAmount;
        inStore = orderInStore;
    }

    //calculates the total price as a sum of all ingredients
    public float GetProductionPrice()
    {
        return
            GameController.Instance.flourPrice
            + GameController.Instance.pizzakartonPrice
            + salami * GameController.Instance.salamiPrice
            + cheese * GameController.Instance.cheesePrice
            + tomatoes * GameController.Instance.tomatoesPrice;
    }

    //calculate the total sell price
    public float GetSellPrice()
    {
        return
            GameController.Instance.flourSellPrice
            + GameController.Instance.pizzaKartonSellPrice
            + salami * GameController.Instance.salamiSellPrice
            + cheese * GameController.Instance.cheeseSellPrice
            + tomatoes * GameController.Instance.tomatoesSellPrice;
    }

    //calculate production time
    public float GetProductionTime()
    {
        return 0;
    }
}
