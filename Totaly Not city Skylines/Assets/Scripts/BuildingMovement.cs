using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMovement : MonoBehaviour {

    Vector3 target;
    Vector3 TruePos;
    public float gridSpacing;
    public bool ScriptActive;

    private void Start()
    {
        ScriptActive = true;
    }
    //While in placement mode the building follows the mouse snapping to the grid.
    private void LateUpdate()
    {
        if (ScriptActive)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                target = hit.point;
                target = target + new Vector3(0.5f, 0, 0.5f);
            }

            TruePos.x = Mathf.FloorToInt(target.x / gridSpacing) * gridSpacing;
            TruePos.z = Mathf.FloorToInt(target.z / gridSpacing) * gridSpacing;
            TruePos.y = 0;

            this.transform.position = TruePos;
        }
    }
}
