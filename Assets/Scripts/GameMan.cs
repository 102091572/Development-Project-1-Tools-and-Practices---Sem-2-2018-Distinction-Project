using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour {
    private int MoneyCap = 20000;
    public int StartingMoney;

    public int Money;


    public int Pop;
    

    public int Power;
    public int PowerCap;
    public int PopCap = 50;
    
    public int Day = 0;
    public int TaxPerDay = 50;
    public int CostPerDay = 0;
    public int GainPerDay;


    public Text DayText;
    public Text MoneyText;
    public Text PopText;
    public Text PowerText;
    public Text CostText;
    public Text GainText;

    //sets defults
    // Use this for initialization
    void Start () {
        Money = StartingMoney;
        PopCap = 51;
        Pop = 55;
        StartCoroutine(DayCycle());
        PowerCap = 5;
	}

    //GameLoop
    IEnumerator DayCycle()
    {
        //each day loop / game loops are 5 seconds
        yield return new WaitForSeconds(5);
        Day++;
      //validation to ensure correct values 
        if (Money > MoneyCap)
        {
            Money = MoneyCap;
        }

        if (Pop > PopCap)
        {
            Pop = PopCap;
        }
        //calcs how much money we are makeing/losing and changes the money value
        GainPerDay = Pop * TaxPerDay;
        Money = Money + (GainPerDay - CostPerDay);
        
        //Must be called last in loop as it starts the loop again
        StartCoroutine(DayCycle());
    }

	// Update is called once per frame
    //Updates the ui with the correct values
	void Update () {
        DayText.text = "Day : " + Day.ToString();
        MoneyText.text = "$ " + Money.ToString() + " / " + MoneyCap.ToString();
        PopText.text = "Population : " + Pop.ToString() + " / " + PopCap.ToString();
        PowerText.text = "Power : " + Power.ToString() + " / " + PowerCap.ToString();
        CostText.text = "- " + CostPerDay.ToString() + " Per day";
        GainText.text = "+ " + GainPerDay.ToString() + " Per day";


    }
    //called when a building is placed 
    public void PurchaseBuilding(int BuildCost,int ContinuedCost, int PowerCost)
    {
        Money = Money - BuildCost;
        CostPerDay = CostPerDay + ContinuedCost;
        Power = Power + PowerCost;
    }

    public void AddPopulation(int PopIncrease)
        //validation to ensure population is not over cap
    {
        if (Pop + PopIncrease > PopCap)
        {
            Pop = PopCap;
        }
        else
        {
            Pop = Pop + PopIncrease;
        }
    }
    //called when apartment building is created
    public void IncreasePopCap(int PopCapIncrease)
    {
        PopCap = PopCapIncrease;
    }

    //called when a building reqiuires power
    public void IncreasePowerCap(int PowerCapIncrease)
    {
        PowerCap = PowerCapIncrease;
    }

    //called when building is destroyed 
    public void BuildingDestroyed(int CostReduction)
    {
        CostPerDay = CostPerDay - CostReduction;
    }
    //exits the application Called by ui event in game
    public void Exit()
    {
        Application.Quit();
    }
}
