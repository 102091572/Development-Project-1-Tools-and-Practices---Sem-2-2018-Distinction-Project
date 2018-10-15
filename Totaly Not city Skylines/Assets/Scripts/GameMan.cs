using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour {
    private int MoneyCap = 20000;
    public int StartingMoney;

    public int Money; // Current amount of money player has
    public int Pop; // Current Population

    private int Employed; // Total no. of employed people
    private int JobCap; // total no. of available jobs

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
    public Text EmploymentText;

    //sets defults
    // Use this for initialization
    void Start () {
        Money = StartingMoney;
        PopCap = 50;
        Pop = 50;
        PowerCap = 5;
        JobCap = 20;
        StartCoroutine(DayCycle());
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

        if (Employed > JobCap)
        {
            Employed = JobCap;
        }

        // If jobs & people are available the no. of employed grows by 5 daily
        if ((Employed < JobCap) && (Employed < Pop))
        {
            Employed += 5;
        }

        //calcs how much money we are makeing/losing and changes the money value
        GainPerDay = Employed * TaxPerDay;
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
        EmploymentText.text = "Employed: " + Employed.ToString() + " / " + JobCap.ToString();
        PowerText.text = "Power : " + Power.ToString() + " / " + PowerCap.ToString();
        CostText.text = "- " + CostPerDay.ToString() + " Per day";
        GainText.text = "+ " + GainPerDay.ToString() + " Per day";
    }

    //alled when a building is placed 
    public void PurchaseBuilding(int BuildCost,int ContinuedCost)
    {
        Money = Money - BuildCost;
        CostPerDay = CostPerDay + ContinuedCost;
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

    //called when building is destroyed 
    public void BuildingDestroyed(int CostReduction)
    {
        CostPerDay = CostPerDay - CostReduction;
    }


    /* Called when Workplace building is created */
    // When a workplace is created the max no. of employees rises
    public void IncreaseJobCap(int JobGrowth)
    {
        JobCap = JobCap + JobGrowth;
    }

    //exits the application 

    public void Exit()
    {
        Application.Quit();
    }
}
