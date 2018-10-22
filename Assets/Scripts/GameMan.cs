
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{
	public GameObject camera;
    public GameObject light;
	public Color dawnlight;
	public Color daylight;
	public Color nightlight;
    private int MoneyCap = 20000;
    public int StartingMoney;

    public int Money;


    public int Pop = 50;
    public int PopCap = 50;

    public int Workers = 10;
    public int WorkerCap = 30;

    public int Power;
    public int PowerCap;


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
    public Text JobText;

    //sets defults
    // Use this for initialization
    void Start()
    {
        Money = StartingMoney;
        PopCap = 51;
        Pop = 47;
        StartCoroutine(DayCycle());
        PowerCap = 5;
    }

    //GameLoop
    IEnumerator DayCycle()
    {
		light.GetComponent<Light> ().intensity = 0.5f;
		light.GetComponent<Light> ().color = dawnlight;
		camera.GetComponent<Camera> ().backgroundColor = dawnlight;
        //each day loop / game loops are 5 seconds
        yield return new WaitForSeconds(1);
		light.GetComponent<Light> ().intensity = 1;
		light.GetComponent<Light> ().color = daylight;
		camera.GetComponent<Camera> ().backgroundColor = daylight;
		yield return new WaitForSeconds(1);
		light.GetComponent<Light> ().intensity = 0.5f;
		light.GetComponent<Light> ().color = dawnlight;
		camera.GetComponent<Camera> ().backgroundColor = dawnlight;
		yield return new WaitForSeconds(1);
		camera.GetComponent<Camera> ().backgroundColor = nightlight;
		light.GetComponent<Light> ().color = nightlight;
		light.GetComponent<Light> ().intensity = 0.1f;
		yield return new WaitForSeconds(2);
        Day++;

         if (Money > MoneyCap)
        {
            Money = MoneyCap;
        }

        if (Pop > PopCap)
        {
            Pop = PopCap;
        }

        if (Workers > WorkerCap)
        {
            Workers = WorkerCap;
        }

        if (Workers > Pop)
        {
            Workers = Pop;
        }
        

        // City gains workers if there are enough people and workplaces 
        if (Workers < WorkerCap && Workers < Pop)
        {
            Workers += 2;
        }

        //City gains population if there are enough population capaticy
        AddPopulation(2);

        //calcs how much money we are makeing/losing and changes the money value
        GainPerDay = Workers * TaxPerDay; // Earn $50 per worker per day

        Money += GainPerDay - CostPerDay;

        //validation to ensure correct values 
        if (Money > MoneyCap)
        {
            Money = MoneyCap;
        }

        if (Pop > PopCap)
        {
            Pop = PopCap;
        }

        if (Workers > WorkerCap)
        {
            Workers = WorkerCap;
        }

        if (Workers > Pop)
        {
            Workers = Pop;
        }

        //Must be called last in loop as it starts the loop again
        StartCoroutine(DayCycle());
    }

    // Update is called once per frame
    //Updates the ui with the correct values
    void Update()
    {
        DayText.text = "Day : " + Day.ToString();
        MoneyText.text = "$ " + Money.ToString() + " / " + MoneyCap.ToString();
        PopText.text = "Population : " + Pop.ToString() + " / " + PopCap.ToString();
        PowerText.text = "Power : " + Power.ToString() + " / " + PowerCap.ToString();
        CostText.text = "- " + CostPerDay.ToString() + " Per day";
        GainText.text = "+ " + GainPerDay.ToString() + " Per day";
        JobText.text = "Employment : " + Workers.ToString() + " / " + WorkerCap.ToString();




    }
    //called when a building is placed 
    public void PurchaseBuilding(int BuildCost, int ContinuedCost, int PowerCost)
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
        PopCap += PopCapIncrease;
    }


    // Called when workplace is created
    public void IncreaseWorkerCap(int WorkerIncrease)
    {
        WorkerCap += WorkerIncrease;
    }

    //called when a building reqiuires power
    public void IncreasePowerCap(int PowerCapIncrease)
    {
        PowerCap += PowerCapIncrease;
    }

    // Called when Bank is created
    public void IncreaseMoneyCap(int MoneyCapIncrease)
    {
        MoneyCap += MoneyCapIncrease;
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

