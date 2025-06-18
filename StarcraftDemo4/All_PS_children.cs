using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    public class EngineeringBay : ProducingStructure
    {
        public EngineeringBay(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 125,
            int _gas_Required = 0,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Unit _myProducingUnit = null,
            Upgrade _myProducingUpgrade = null,
            Structure_Name _name = Structure_Name.Engineering_Bay,
            int? _production_Time_Left = 35) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull,
             _myProducingUnit, _myProducingUpgrade, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Command_Center);
        }
    }
    [Serializable]
    public class GhostAcademy : ProducingStructure
    {
        int? time_On_Nuke;
        public GhostAcademy(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 50,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Unit _myProducingUnit = null,
            Upgrade _myProducingUpgrade = null,
            Structure_Name _name = Structure_Name.Ghost_Academy,
            int? _production_Time_Left = 40,
            int? _time_On_Nuke = null) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull,
             _myProducingUnit, _myProducingUpgrade, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Barracks);
        }
        public void Build_Nuke()
        {
            time_On_Nuke = 60;
        }
    }
    [Serializable]
    public class Armory : ProducingStructure
    {
        public Armory(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 100,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Unit _myProducingUnit = null,
            Upgrade _myProducingUpgrade = null,
            Structure_Name _name = Structure_Name.Armory,
            int? _production_Time_Left = 65) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull,
             _myProducingUnit, _myProducingUpgrade, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Factory);
        }
    }
    [Serializable]
    public class FusionCore : ProducingStructure
    {
        public FusionCore(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Unit _myProducingUnit = null,
            Upgrade _myProducingUpgrade = null,
            Structure_Name _name = Structure_Name.Fusion_Core,
            int? _production_Time_Left = 65) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull,
             _myProducingUnit, _myProducingUpgrade, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Starport);
        }
    }
    [Serializable]
    public class CommandCenter : ProducingStructure, IAddon
    {
        public Addon myAddon { get; set; }
        public CommandCenter(
            Addon _myAddon = null,
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 400,
            int _gas_Required = 0,
            bool _unitQueFull = false,
            bool _upgradeQueFull = false,
            Unit _myProducingUnit = null,
            Upgrade _myProducingUpgrade = null,
            Structure_Name _name = Structure_Name.Command_Center,
            int? _production_Time_Left = 100) :
            base(_structure_Reqs, _minerals_Required, _gas_Required, _unitQueFull, _upgradeQueFull,
             _myProducingUnit, _myProducingUpgrade, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
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
        public override void Display_Stats()
        {
            if (myAddon != null)
            {
                if (myAddon.production_Time_Left == 0)
                {
                    str=String.Format("\thas an attached {0}", myAddon.name);
                    SendString(str);
                    if (myAddon.name == Structure_Name.Orbital_Command)
                    { 
                        str = String.Format("{0} energy", ((OrbitalCommand)myAddon).Energy);
                        SendString(str);
                    }
                }
                else
                {
                    str=String.Format("\tbuilding a {0} with {1} seconds left",
                        myAddon.name, myAddon.production_Time_Left);
                    SendString(str);
                }
            }
            base.Display_Stats();
        }
        public bool hasOrbital()
        {
            if (myAddon != null)
                return (production_Time_Left == 0 &&
               myAddon.production_Time_Left == 0 &&
               myAddon is OrbitalCommand);
            else
                return false;
        }
        override public void Time_Step(int seconds, State myState)
        {
            //deal with addon; means CC is done.
            if (myAddon != null)
            {
                //im done
                if (myAddon.production_Time_Left == 0)
                {
                    if (myAddon is OrbitalCommand)
                    {
                        ((OrbitalCommand)myAddon).Energy = seconds + ((OrbitalCommand)myAddon).Energy;
                        //you may have a mule!
                        List<Mule> removeList = new List<Mule>();
                        var AllMules =
                            from mule in myState.currentUnits
                            where mule is Mule
                            select mule;
                        if (AllMules.Any())
                        {
                            foreach (Mule mule in AllMules)
                            {
                                mule.lifeTime = Math.Max(mule.lifeTime - seconds, 0);
                                if (mule.lifeTime == 0)
                                    removeList.Add(mule);
                            }
                        }
                        foreach (Mule mule in removeList)
                        { myState.currentUnits.Remove(mule); }
                    }

                }//im  about to be.
                else if (seconds >= myAddon.production_Time_Left)
                {
                    if (myAddon is OrbitalCommand)
                        ((OrbitalCommand)myAddon).Energy = ((OrbitalCommand)myAddon).Energy + seconds - (myAddon.production_Time_Left ?? 0);

                    SendString("my " + myAddon.name + " is finishing");
                    myAddon.production_Time_Left = 0;
                    unitQueFull = false;
                    upgradeQueFull = false;
                }
                else //not about to be done.
                    myAddon.production_Time_Left -= seconds;
            }

            if ((production_Time_Left - seconds <= 0) && (production_Time_Left != 0))//is going to finnish
            {
                myState.unit_Cap += 12;
                str=String.Format("cc is about to finish");
                SendString(str);
            }
            base.Time_Step(seconds, myState);
        }
        public void Cast(EnergyUnit myEUnit)
        {
            ((OrbitalCommand)myAddon).Energy -= myEUnit.energyRequired;

        }
    }
}
