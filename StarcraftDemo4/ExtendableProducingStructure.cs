using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    class ExtendableProducingStructure : ProducingStructure, IAddon
    {
        protected Unit myProducingUnit2;
        public Addon myAddon { get; set; }

        public ExtendableProducingStructure(
            List<Structure_Name> _structure_Reqs,
            int _minerals_Required,
            int _gas_Required,
            bool _unitQueFull,
            bool _upgradeQueFull,
            Structure_Name _name,
            int? _production_Time_Left,
            Upgrade _myProducingUpgrade,
            Unit _myProducingUnit,
            Unit _myProducingUnit2,
            Addon _myAddon) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull, _myProducingUnit,
            _myProducingUpgrade, _name, _production_Time_Left)
        {
            myProducingUnit2 = _myProducingUnit2;
            myAddon = _myAddon;
        }

        public void Build(Addon buildAddon)
        {
            if (myAddon == null)
            {
                myAddon = buildAddon;
                unitQueFull = true;
                upgradeQueFull = true;
            }
            else
                throw new CantBuildException("should have been noticed; i already have an addon!");
        }
        public override void Build(Unit myUnit)
        {

            if ((myAddon != null) && (myAddon.HasReactorDone()) && (myProducingUnit != null))
            {
                myProducingUnit2 = myUnit;
                unitQueFull = true;
                return;
            }
            if ((myAddon != null) && (myAddon.HasReactorDone()) && (myProducingUnit == null))
            {
                myProducingUnit = myUnit;
                if (myProducingUnit2 == null)
                    unitQueFull = false;
                else
                    unitQueFull = true;

                return;
            }
            if (myProducingUnit != null)
            {
                //should never get thrown. should already be checked in Player
                str=String.Format("i dont have a reactor, i am already producing, but my que isnt full! error!");
                SendString(str);
                throw new CantBuildException("trying to train a " + myUnit.name + " but im already producing" + myProducingUnit.name);
            }
            myProducingUnit = myUnit;
            unitQueFull = true;
        }
        public override void Display_Stats()
        {
           
            if ((myAddon != null) && (myAddon.production_Time_Left == 0))
                SendString("\thas an attached"+ myAddon.name);
            else if (myAddon != null)
            {
                str = String.Format("\tbuilding a {0} with {1} seconds left", 
                    myAddon.name, myAddon.production_Time_Left);
                SendString(str);
            }
            
            if ((myAddon != null) && (myAddon.HasReactorDone()))
            {
                str=String.Format("\tUnitQuefull:{0}\tUpgradeQueFull:{1}", unitQueFull, upgradeQueFull);
                SendString(str);
                if (myProducingUnit == null)
                {}//  String.Format("\tTraining in slot 1: Nothing");
                else
                {
                    str=String.Format("\tTraining in slot 1: {0}", myProducingUnit.name);
                    SendString(str);
                }
                if (myProducingUnit2 == null)
                { }//   String.Format("\tTraining in slot 2: Nothing");
                else
                {
                    str = String.Format("\tTraining in slot 2: {0}", myProducingUnit2.name);
                    SendString(str);
                }
            }
            else
                base.Display_Stats();
        }
        public override void Time_Step(int seconds, State myState)
        {
            //deal with addon;
            if (myAddon != null)
            {   //myAddon is about to finish
                if (seconds >= myAddon.production_Time_Left && myAddon.production_Time_Left !=0)
                {
                    SendString("my xps addon" + myAddon.name + " is finishing has only :"+ myAddon.production_Time_Left);
                    myAddon.production_Time_Left = 0;
                    unitQueFull = false;
                    upgradeQueFull = false;
                }
                else
                    myAddon.production_Time_Left = Math.Max(0, (int)myAddon.production_Time_Left - seconds);
            }
            //if i have a reactor, deal with it, otherwise im like any other PS
            if ((myAddon != null) && myAddon.HasReactorDone())
            {
                if (myProducingUnit != null)
                {
                    Update(seconds, myState,  ref myProducingUnit);
                }
                if (myProducingUnit2 != null)
                {
                    Update(seconds, myState, ref myProducingUnit2);                
                }
            }
            else
            {
                base.Time_Step(seconds, myState);
            }
        }
    }
}
    