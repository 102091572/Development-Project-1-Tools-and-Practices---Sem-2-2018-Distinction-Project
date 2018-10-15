﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int BuildCost;
    public int ContinuedCost;
    public GameObject gameManager;
    public GameMan _gameMan;
   //finds the gameman object
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameMan");
        _gameMan = gameManager.GetComponent<GameMan>();
    }

    public void Init() {
        
}
    //Builds the builing stops it following mouse and subtracts the money and adds the daily cost 
    public virtual void Build()
    {
        gameManager.GetComponent<GameMan>().PurchaseBuilding(BuildCost, ContinuedCost);
    }

}

