using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCreation : MonoBehaviour {

    public GameObject Air;
    public GameObject AirGO;

    public List<GameObject> GridPoints;

    private int i;
    private int ii;
	// Use this for initialization
	void Start () {

        List<GameObject> selected = new List<GameObject>();


        i = 0;
        ii = 0;
        for (int X = 0; i < 41; i++)
        {
            
            ii = 0;
            for (int Z = 0; ii < 41; ii++)
            {
                AirGO = (GameObject)Instantiate(Air, new Vector3(X, 0, Z), Quaternion.identity);
                GridPoints.Add(AirGO);
                Z++;
            }
            X++;
        }
	}
	

}
