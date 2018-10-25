using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int Type; // 1 = Bank, 2 = Apartment, 3 = Workplace, 4 = Power
    public int BuildCost;
    public int PowerCost;
    public GameObject gameManager;
   //finds the gameman object
    private void Start()
    {

        gameManager = GameObject.FindGameObjectWithTag("GameMan");
    }
    //Builds the builing stops it following mouse and subtracts the money and adds the daily cost 
    public void Build()
    {
        gameManager.GetComponent<GameMan>().PurchaseBuilding(BuildCost, PowerCost);
    }

}

