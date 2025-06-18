using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    public abstract class ProducingStructure : Structure
    {
        //these next two are corrected for building units on a working Addon. will add this later when i get timestep working
        public bool unitQueFull;   //also indicates if structure CAN build units or is just for upgrades.
        public bool upgradeQueFull;  //also indicates if structure can upgrade or if it just builds units.
        public Unit myProducingUnit;
        public Upgrade myProducingUpgrade;
        protected ProducingStructure(
            List<Structure_Name> _structure_Reqs,
            int _minerals_Required,
            int _gas_Required,
            bool _unitQueFull,
            bool _upgradeQueFull,
            Unit _myProducingUnit,
            Upgrade _myProducingUpgrade,
            Structure_Name _name,
            int? _production_Time_Left) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _name, _production_Time_Left)
        {
            unitQueFull = _unitQueFull;
            upgradeQueFull = _upgradeQueFull;
            myProducingUnit = _myProducingUnit;
            myProducingUpgrade = _myProducingUpgrade;
        }
        public virtual void Display_Stats()
        {
            str = String.Format("\tUnitQuefull:{0}\tUpgradeQueFull:{1}", unitQueFull, upgradeQueFull);
            SendString(str);
            if (myProducingUnit == null)
            { }// String.Format("\tTraining: Nothing");
            else
            {
                str = String.Format("\tTraining: {0} has {1} seconds left ",
                    myProducingUnit.name, myProducingUnit.production_Time_Left);
                SendString(str);
            }
            if (myProducingUpgrade == null)
            { }//  String.Format("\tUpgrading to: Nothing");
            else
            {
                str = String.Format("\tUpgrading to: {0} has {1} seconds left",
                    myProducingUpgrade.name, myProducingUpgrade.production_Time_Left);
                SendString(str);
            }
        }

        public virtual void Build(Unit myUnit)
        {
            myProducingUnit = myUnit;
            unitQueFull = true;
        }

        public void Build(Upgrade myUpgrade)
        {
            myProducingUpgrade = myUpgrade;
            upgradeQueFull = true;

            //if this upgrade goes to me and im an XPS without a Tech them theres a problem.
            //just a random check.
            if ((this is ExtendableProducingStructure) &&
            (((ExtendableProducingStructure)this).myAddon != null) &&
            !(((ExtendableProducingStructure)this).myAddon.HasTechDone()))
            {
                str = String.Format("something wrong; i dont have tech but im an XPS with an upgrade");
                SendString(str);
            }
        }

        public override void Time_Step(int seconds, State myState)
        {
            if (production_Time_Left != 0)
            {
                base.Time_Step(seconds, myState);
                if (production_Time_Left == 0)
                {
                    unitQueFull = false;
                    upgradeQueFull = false;
                }
            }

            if (myProducingUnit != null)
            {
                Update(seconds, myState, ref myProducingUnit);
            }

            if (myProducingUpgrade != null)
            {
                myProducingUpgrade.production_Time_Left = Math.Max(((int)myProducingUpgrade.production_Time_Left - seconds), 0);
                if (myProducingUpgrade.production_Time_Left == 0)
                {
                    upgradeQueFull = false;
                    myState.currentUpgrades.Add(myProducingUpgrade);
                    myProducingUpgrade = null;
                }
            }
        }

        //ref so it can nullify the unit if need be.
        protected void Update(int seconds, State myState, ref Unit myProducingUnit)
        {
            myProducingUnit.production_Time_Left = Math.Max(((int)myProducingUnit.production_Time_Left - seconds), 0);

            if (myProducingUnit.production_Time_Left == 0)
            {
                unitQueFull = false;
                myState.currentUnits.Add(myProducingUnit);
                //myState.unit_Count += myProducingUnit.population;
                myProducingUnit = null;
            }
        }
    }
}
