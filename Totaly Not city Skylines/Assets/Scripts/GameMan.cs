using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour {
    private int MoneyCap = 20000;
    public int StartingMoney;

    public int Money;


    public int Pop;
    

    private int Power;
    private int PowerCap;
    private int PopCap = 50;
    
    private int Day = 0;
    private int TaxPerDay = 50;
    private int CostPerDay = 0;
    private int GainPerDay;


    public Text DayText;
    public Text MoneyText;
    public Text PopText;
    public Text PowerText;
    public Text CostText;
    public Text GainText;


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
        yield return new WaitForSeconds(5);
        Day++;
        GainPerDay = Pop * TaxPerDay;

        
        Money = Money + (GainPerDay - CostPerDay);

        if (Money > MoneyCap)
        {
            Money = MoneyCap;
        }

        if (Pop > PopCap)
        {
            Pop = PopCap;
        }

        StartCoroutine(DayCycle());
    }

	// Update is called once per frame
	void Update () {
        DayText.text = "Day : " + Day.ToString();
        MoneyText.text = "$ " + Money.ToString() + " / " + MoneyCap.ToString();
        PopText.text = "Population : " + Pop.ToString() + " / " + PopCap.ToString();
        PowerText.text = "Power : " + Power.ToString() + " / " + PowerCap.ToString();
        CostText.text = "- " + CostPerDay.ToString() + " Per day";
        GainText.text = "+ " + GainPerDay.ToString() + " Per day";


    }

    public void PurchaseBuilding(int BuildCost,int ContinuedCost)
    {
        Money = Money - BuildCost;
        CostPerDay = CostPerDay + ContinuedCost;
    }

    public void AddPopulation(int PopIncrease)
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
    public void IncreasePopCap(int PopCapIncrease)
    {
        PopCap = PopCapIncrease;
    }

    public void BuildingDestroyed(int CostReduction)
    {
        CostPerDay = CostPerDay - CostReduction;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
