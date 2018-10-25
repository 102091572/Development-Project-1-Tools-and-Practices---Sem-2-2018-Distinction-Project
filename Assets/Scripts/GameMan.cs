??using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMan : MonoBehaviour
{

    private GameObject BuildingSystem;

    // Variables for the day/night cycle
	public GameObject camera;
    public GameObject light;
	public Color dawnlight;
	public Color daylight;
	public Color nightlight;

    private int Apartments = 0;
    private int Workplaces = 0;
    private int Banks = 0;
    private int PowerPlants = 0;

    private int ApartmentUpgradeLevel = 1;
    private int WorkplaceUpgradeLevel = 1;
    private int BankUpgradeLevel = 1;
    private int PowerPlantUpgradeLevel = 1;

    public int StartingMoney;
    public int Money;
    private int MoneyCap = 20000;

    public int Pop = 50;
    public int PopCap = 50;

    public int Workers = 10;
    public int WorkerCap = 30;
    public int Unemployment = 0;

    public int Power;
    public int PowerCap;

    public int Day = 1;
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
        StartCoroutine(DayCycle());
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

        SetCaps(); // Calculate the economic caps based off no. of buildings 
        CalculateCosts();  // Calculate how much money the city gains/loses per day
        ValidateVariables(); // Make sure variables are within bounds

        // City gains workers if there are enough people and workplaces 
        if (Workers < WorkerCap && Workers < Pop)
        {
            Workers += 2;
        }
       
        AddPopulation(2);  //City gains population if there are enough population capaticy
        StartCoroutine(DayCycle()); //Must be called last in loop as it starts the loop again
    }

    // Update is called once per frame
    //Updates the ui with the correct values
    void Update()
    {
        DayText.text = "Day : " + Day.ToString();
        MoneyText.text = "$ " + Money.ToString() + " / " + MoneyCap.ToString() + " *" + Banks;
        PopText.text = "Population : " + Pop.ToString() + " / " + PopCap.ToString() + " *" + Apartments;
        PowerText.text = "Power : " + Power.ToString() + " / " + PowerCap.ToString() + " *" + PowerPlants;
        CostText.text = "- " + CostPerDay.ToString() + " Per day";
        GainText.text = "+ " + GainPerDay.ToString() + " Per day";
        JobText.text = "Employment : " + Workers.ToString() + " / " + WorkerCap.ToString() + " *" + Workplaces;  
    }

    // Make sure our stats dont exceed the caps so we dont get weird numbers like Money: 100/50
    public void ValidateVariables()
    {
        if (Money > MoneyCap)
            Money = MoneyCap;
    
        if (Pop > PopCap)
            Pop = PopCap;

        if (Workers > WorkerCap)
            Workers = WorkerCap;

        if (Workers > Pop)
            Workers = Pop;
    }

    // Based off no. of buildings, 
    public void CalculateCosts()
    {
        if (Workers < Pop)
        {
            Unemployment = (Pop - Workers) * 10; // Each unemployed person costs $10 per day 
        }
        //calcs how much money we are makeing/losing and changes the money value
        GainPerDay = Workers * TaxPerDay; // Earn $50 per worker per day
        CostPerDay = (Banks * 20) + (Apartments * 20) + (Workplaces * 50) + (PowerPlants * 100) + Unemployment;

        Money += (GainPerDay - CostPerDay);
    }

    public void SetCaps()
    {
        // Upgrades increase the 
        PopCap = (Apartments * 50 * ApartmentUpgradeLevel) + 30;
        WorkerCap = (Workplaces * 30 * WorkplaceUpgradeLevel) + 20;
        MoneyCap = (Banks * 500 * BankUpgradeLevel) + 1000;
        PowerCap = (PowerPlants * 5 * PowerPlantUpgradeLevel) + 10;
    }

    //called when a building is placed 
    public void PurchaseBuilding(int BuildCost, int PowerCost)
    {
        Money = Money - BuildCost;
        Power = Power + PowerCost;
    }

    public void AddBuilding(int BuildingType)
    {
        switch(BuildingType)
        {
            // Bank
            case 1:
                Banks++;
                break;
            // Apartment
            case 2:
                Apartments++;
                break;
            // Workplace
            case 3:
                Workplaces++;
                break;
            // Powerplant
            case 4:
                PowerPlants++;
                break;
        }
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
    public void BuildingDestroyed(int Type)
    {
        switch (Type)
        {
            case 1:
                Banks--;
                break;
            case 2:
                Apartments--;
                break;
            case 3:
                Workplaces--;
                break;
            case 4:
                PowerPlants--;
                break;
        }
    }

    // User can purchase upgrades that make their buildings more effective 
    public void PurchaseUpgrade(int type)
    {
        if (Money >= 1000)
        {
            switch (type)
            {
                // Upgrade Bank
                case 1:
                    BankUpgradeLevel++;
                    break;
                // Upgrade Apartmnet
                case 2:
                    ApartmentUpgradeLevel++;
                    break;
                // Upgrade Workplace
                case 3:
                    WorkplaceUpgradeLevel++;
                    break;
                // Upgrade Powerplants
                case 4:
                    PowerPlantUpgradeLevel++;
                    break;
            }
            // upgrades cost 1000 dollars each 
            Money -= 1000;
        }
        // Return an error message if the user cannot afford the upgrade
        else
        {
            BuildingSystem.GetComponent<BuildingSystem>().Error("You cannot afford that ");
        }
    }

    //exits the application Called by ui event in game
    public void Exit()
    {
        Application.Quit();
    }
}

