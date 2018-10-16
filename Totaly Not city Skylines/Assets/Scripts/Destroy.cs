using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public GameObject gameManager;

    //Destroys a game obhect in the game word that has this scrip on it 
    public void DestroyAll()
    {
        Destroy(this.gameObject);
    }
    //Destroys a game obhect in the game word that has this scrip on it and also removes the cost per day of the building from rhe daily cost
    public void DestroyReturnCost()
    {
        gameManager.GetComponent<GameMan>().BuildingDestroyed(this.GetComponent<Building>().ContinuedCost);
        Destroy(this.gameObject);
    }

}
