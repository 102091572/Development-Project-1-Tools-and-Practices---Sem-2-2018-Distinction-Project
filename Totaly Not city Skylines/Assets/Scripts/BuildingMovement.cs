using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour {

    Vector3 target;
    Vector3 TruePos;
    GameObject hitG0;
    List<GameObject> SelectedGo;
   
    public bool ScriptActive;
    public Material deselctmat;
    public Material SelectMat;

    private void Start()
    {
        
        SelectedGo = new List<GameObject>();
    }
    //While in placement mode the building follows the mouse snapping to the grid.
    private void Update()
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
            foreach (GameObject Go in SelectedGo)
            {
                Go.GetComponent<MeshRenderer>().material = SelectMat;
                Debug.Log("Selected mat");
            }
        }
            
        }
    
}
