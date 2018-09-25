using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {
    public GameObject gameManager;

    public void DestroyAll()
    {
        Destroy(this.gameObject);
    }

    public void DestroyReturnCost()
    {
        gameManager.GetComponent<GameMan>().BuildingDestroyed(this.GetComponent<Building>().ContinuedCost);
        Destroy(this.gameObject);
    }

}
