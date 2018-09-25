using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int BuildCost;
    public int ContinuedCost;
    public GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameMan");
    }

    public void Build()
    {
        gameManager.GetComponent<GameMan>().PurchaseBuilding(BuildCost, ContinuedCost);
        this.GetComponent<BuildingMovement>().ScriptActive = false;
    }

}

