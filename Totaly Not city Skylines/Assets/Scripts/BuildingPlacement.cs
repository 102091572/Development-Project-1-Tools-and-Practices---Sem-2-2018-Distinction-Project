using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPlacement : MonoBehaviour {


    public GameObject BuildMenu;
    public GameObject BuildMenuOptions;
    public GameObject PausePanel;

    public GameObject BankPreFab;
    public GameObject ApartmentPreFab;
    public GameObject WorkPlacePreFab;
    public GameObject PowerStationPreFab;

    Vector3 PlaceLocation;


    public Text ErrorText;
    public GameObject ErrorTextParent;

    private bool _BuildModeBool;
    public bool PauseGameBool;

    public bool CurrentlyBuilding;
    public int CurrentlyBeingBuilt;
    GameObject CurrentlyBeingBuiltGO;

    public GameObject gameManager;

    



    // Use this for initialization
    void Start () {
        BuildMode(false);
        CurrentlyBuilding = false;

    }


    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0)) && (CurrentlyBuilding == true))
        {
            Build();
            CurrentlyBuilding = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentlyBuilding == true)
            {
                CurrentlyBeingBuiltGO.GetComponent<Destroy>().DestroyAll();
                CurrentlyBuilding = false;
            }

            if (_BuildModeBool == true)
            {
                BuildMode(false);
            }
            else
            {
                if (PauseGameBool == false)
                {
                    PauseGame();
                }
            }
        }
    }
    public void Building(int building)
    {
        CurrentlyBuilding = true;
        CurrentlyBeingBuilt = building;
        BuildMode(false);

        if (building == 1)
        {
            CurrentlyBeingBuiltGO = (GameObject)Instantiate(BankPreFab, PlaceLocation, Quaternion.identity);
        }
        if (building == 2)
        {
            CurrentlyBeingBuiltGO = (GameObject)Instantiate(ApartmentPreFab, PlaceLocation, Quaternion.identity);
        }
        if (building == 3)
        {
            CurrentlyBeingBuiltGO = (GameObject)Instantiate(WorkPlacePreFab, PlaceLocation, Quaternion.identity);
        }
        if (building == 4)
        {
            CurrentlyBeingBuiltGO = (GameObject)Instantiate(PowerStationPreFab, PlaceLocation, Quaternion.identity);
        }
    }
    

    public void Build()
    {
        
        if (this.GetComponent<GameMan>().Money > CurrentlyBeingBuiltGO.GetComponent<Building>().BuildCost)
        {
            CurrentlyBeingBuiltGO.GetComponent<Building>().Build();
        }
        else
        {
            Error("Not Enough Money");
            CurrentlyBeingBuiltGO.GetComponent<Destroy>().DestroyAll();
            CurrentlyBuilding = false;
        }
        
    }

    public void Error(string errMsg)
    {
        ErrorText.text = errMsg;
        StartCoroutine(DisplayTextForSeonds(2, ErrorTextParent));
    }

    IEnumerator DisplayTextForSeonds(int Time, GameObject Text)
    {
        Text.SetActive(true);
        yield return new WaitForSeconds(Time);
        Text.SetActive(false);
    }



    public void BuildMode (bool BuildModebool) {
        _BuildModeBool = BuildModebool;
        if (BuildModebool == false)
        {
            BuildMenu.gameObject.SetActive(true);
            BuildMenuOptions.gameObject.SetActive(false);
        }

        if (BuildModebool == true)
        {
            BuildMenu.gameObject.SetActive(false);
            BuildMenuOptions.gameObject.SetActive(true);
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
    }
}
