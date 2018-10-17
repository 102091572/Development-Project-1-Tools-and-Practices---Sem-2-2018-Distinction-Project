using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingSystem : MonoBehaviour {

    //buildingplacement vars
    public GameObject BuildMenu;
    public GameObject BuildMenuOptions;
    public GameObject PausePanel;
    public GameObject ToBuildCostPannel;

    public GameObject BankPreFab;
    public GameObject ApartmentPreFab;
    public GameObject WorkPlacePreFab;
    public GameObject PowerStationPreFab;
    public GameObject ToBeBuilt;

    Vector3 PlaceLocation;


    public Text ErrorText;
    public GameObject ErrorTextParent;

   
    public bool PauseGameBool;

    public Text Costtobuild;
    public Text costperdaytobuild;

    public GameObject gameManager;



    public int CurrentBuildType;

    //Vector3 target;
    //Vector3 TruePos;
    GameObject hitG0;
    List<GameObject> SelectedGo;
    private int _cost;
    private int _daycost;
    private int _costBuild;
    private int _dayCostBuild;


    //public bool ScriptActive;
    public Material deselctmat;
    public Material SelectMat;
    public GameObject gridpointsParent;
    bool buildSelectModeBool;

    private void Start()
    {
        
        SelectedGo = new List<GameObject>();
        gridpointsParent.SetActive(false);
        BuildType(5);
       
    }

    //Changing build states and showing / hiding ui elements.



   

    public void BuildType(int buildingType) {
        
        buildSelectModeBool = true;
        CurrentBuildType = buildingType;
        //ui show
        

        switch (buildingType)
        {
            case 1:
                //bank
                gridpointsParent.SetActive(true);
                ToBeBuilt = BankPreFab;
                _cost = BankPreFab.GetComponent<Building>().BuildCost;
                _daycost = BankPreFab.GetComponent<Building>().ContinuedCost;
                ToBuildCostPannel.gameObject.SetActive(true);
                BuildMenuOptions.gameObject.SetActive(false);
                break;
            case 2:
                //appartmeent
                gridpointsParent.SetActive(true);
                ToBeBuilt = ApartmentPreFab;
                _cost = ApartmentPreFab.GetComponent<Building>().BuildCost;
                _daycost = ApartmentPreFab.GetComponent<Building>().ContinuedCost;
                ToBuildCostPannel.gameObject.SetActive(true);
                BuildMenuOptions.gameObject.SetActive(false);
                break;
            case 3:
                //workplace
                gridpointsParent.SetActive(true);
                ToBeBuilt = WorkPlacePreFab;
                _cost = WorkPlacePreFab.GetComponent<Building>().BuildCost;
                _daycost = WorkPlacePreFab.GetComponent<Building>().ContinuedCost;
                ToBuildCostPannel.gameObject.SetActive(true);
                BuildMenuOptions.gameObject.SetActive(false);
                break;
            case 4:
                //power
                gridpointsParent.SetActive(true);
                ToBeBuilt = PowerStationPreFab;
                _cost = PowerStationPreFab.GetComponent<Building>().BuildCost;
                _daycost = PowerStationPreFab.GetComponent<Building>().ContinuedCost;
                ToBuildCostPannel.gameObject.SetActive(true);
                BuildMenuOptions.gameObject.SetActive(false);
                break;
            case 5:
                //no build object
                
                BuildMenu.gameObject.SetActive(true);
                BuildMenuOptions.gameObject.SetActive(false);
                
                ToBuildCostPannel.gameObject.SetActive(false);
                foreach (GameObject point in SelectedGo)
                {
                    point.tag = "Free";
                    point.GetComponent<MeshRenderer>().material = deselctmat;
                    
                }
                SelectedGo.Clear();
                gridpointsParent.SetActive(false);
                break;
            case 6:
                
                BuildMenu.gameObject.SetActive(false);
                BuildMenuOptions.gameObject.SetActive(true);
                gridpointsParent.SetActive(false);
                ToBuildCostPannel.gameObject.SetActive(false);
                SelectedGo.Clear();

                break;
        }

    }
   
    //While in placement mode the building follows the mouse snapping to the grid.
    private void Update()
    {

        Costtobuild.text = "Cost to build all buildings : - " + _costBuild.ToString();
        costperdaytobuild.text = "Cost Per day if all buildings are built : + " + _dayCostBuild.ToString();
        //old update
        if (Input.GetKeyDown(KeyCode.A))
        {
            Build();
        }
            //The escape key handles backing out of menus and pauseing the game
            if (Input.GetKeyDown(KeyCode.Escape))
        {
            //rework esc key
            if (CurrentBuildType != 5)
            {
                BuildType(5);
            }
            else if (PauseGameBool == false)
                {
                    PauseGame();
                    PauseGameBool = true;
                }
            
        }

        //good update
        if (CurrentBuildType!= 5)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    hitG0 = hit.collider.gameObject;

                }
                if (hitG0.tag == "Free")
                {
                    SelectedGo.Add(hitG0);
                    hitG0.tag = "selected";
                    Debug.Log("Selected");
                }
                else if (hitG0.tag == "selected")
                {
                    SelectedGo.Remove(hitG0);
                    hitG0.tag = "Free";
                    Debug.Log("deSelected");
                    hitG0.GetComponent<MeshRenderer>().material = deselctmat;
                }
                _costBuild = 0;
                _dayCostBuild = 0;

                foreach (GameObject Go in SelectedGo)
                {
                    
                    Go.GetComponent<MeshRenderer>().material = SelectMat;
                    Debug.Log("Selected mat");
                    _costBuild = _costBuild + _cost;
                    _dayCostBuild = _dayCostBuild + _daycost;
                }
            }

        }
    }

   //rework places buildings at all selected location if money is enough when build button pressed
    public void Build()
    {
        
        //ensures you have enough money to build else throws an error
        if (this.GetComponent<GameMan>().Money > _costBuild)
        {
            foreach (GameObject GridPoint in SelectedGo)
            {
                Instantiate(ToBeBuilt, GridPoint.transform.position + new Vector3(0,1.5f,0),ToBeBuilt.transform.rotation);
                this.GetComponent<GameMan>().PurchaseBuilding(_cost, _daycost);

            }
            BuildType(5);
            

        }
        else
        {
            Error("Not Enough Money");
            BuildType(5);
        }
        BuildType(5);
        
        

    }

    //Function for displaying error msgs can pass any error to save on repeating code
    public void Error(string errMsg)
    {
        ErrorText.text = errMsg;
        StartCoroutine(DisplayTextForSeonds(2, ErrorTextParent));
    }
    //Ienumerators handle timedelay code this one is for displaying error msgs
    IEnumerator DisplayTextForSeonds(int Time, GameObject Text)
    {
        Text.SetActive(true);
        yield return new WaitForSeconds(Time);
        Text.SetActive(false);
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
        PauseGameBool = false;
    }

     
}
