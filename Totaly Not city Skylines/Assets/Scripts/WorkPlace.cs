using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class WorkPlace : Building
    {
        public int MaxWorkers = 60;
    
        public override void Build()
        {
            gameManager.GetComponent<GameMan>().PurchaseBuilding(BuildCost, ContinuedCost);
            gameManager.GetComponent<GameMan>().IncreaseJobCap(MaxWorkers);
            this.GetComponent<BuildingMovement>().ScriptActive = false;
        }
    }
}
