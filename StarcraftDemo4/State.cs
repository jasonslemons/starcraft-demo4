using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StarcraftDemo4
{

    
        //all static classes can only contain static members(fields, methods, etc) see pg 189 of troelsen
    public class State
    {

        #region properties and fields
        public int minerals { get; set; }
        public int gas { get; set; }
        public int unit_Count { get; set; }
        public int unit_Cap { get; set; }
        public int totalTime { get; set; }
        public static string str;
        public List<object> Mostbuild = new List<object> {
          new Barracks(), //structures
//          new Factory(), 
//          new Starport(),
//          new EngineeringBay(),
//          new CommandCenter(), 
//          new Armory(),
//          new GhostAcademy(), 
//          new FusionCore(), 
          new SupplyDepot(), 
//          new Turret(), 
//          new Bunker(), 
          //new SensorTower(), 
          new Refinery(),
          new Marine(), //units
          new SCV(), 
          new Marauder(), 
//          new Ghost(), 
//          new Reaper(), 
//          new Hellion(), 
          //new SiegeTank(), 
          //new Thor(), 
          //new Viking(),
          //new Medivac(), 
          //new Battlecruiser(), 
          //new Raven(), 
          //new Banshee(), 
          new Mule(),
          //new InfantryWeaponsLvl1(), //upgrades
//          new CombatShield(), 
//          new StimPacks(), 
          //new ConcussiveShells(), 
          //new InfernalPreIgniter(),
          //new BuildingArmor(),
//          new TechLab(Structure_Name.Barracks),
          new Reactor(Structure_Name.Barracks),
          //new TechLab(Structure_Name.Factory),
          //new Reactor(Structure_Name.Factory),
          new OrbitalCommand()
    };

        public static bool verbose = false;

        public List<Unit> currentUnits { get; set; }
        public List<Units_Name> namesCurrentUnits
        {
            get
            {
                var NamesCurrentUnits =
                 from ups in currentUnits
                 select ups.name;
                List<Units_Name> temp = new List<Units_Name>();
                //wierd, cant convert with () casting.
                foreach (Units_Name name in NamesCurrentUnits)
                {
                    temp.Add(name);
                }
                return temp;
            }
        }

        public List<Upgrade> currentUpgrades { get; set; }
        public List<Upgrade_Name> namesCurrentUpgrades
        {
            get
            {
                var NamesCurrentUpgrades =
                 from ups in currentUpgrades
                 select ups.name;
                List<Upgrade_Name> temp = new List<Upgrade_Name>();
                //wierd, cant convert with () casting.
                foreach (Upgrade_Name name in NamesCurrentUpgrades)
                {
                    temp.Add(name);
                }
                return temp;

            }
        }

        public List<Structure> currentStructures { get; set; }
        public List<Structure_Name> namesCurrentStructures
        {
            get
            {
                var NamesCurrentStructures =
                 from structures in currentStructures
                 select structures.name;
                List<Structure_Name> temp = new List<Structure_Name>();
                //wierd, cant convert with () casting. 
                foreach (Structure_Name name in NamesCurrentStructures)
                {
                    temp.Add(name);
                }
                return temp;

            }
        }

        public List<string> Moves { get; set; }

        public static Structure_Name[] XPS_Name = { Structure_Name.Factory, Structure_Name.Starport, Structure_Name.Barracks };

        #endregion

        public State()
        {
            minerals = 50;
            gas = 0;
            unit_Count = 6;
            unit_Cap = 12;
            currentStructures = new List<Structure>();
            currentUpgrades = new List<Upgrade>();
            Moves = new List<string>();
            currentUnits = new List<Unit>();

            //build CC
            CommandCenter CC = new CommandCenter();
            currentStructures.Add(CC);
            CC.production_Time_Left = 0;

            //build SCVs
            currentUnits.AddRange(new List<Unit> {new SCV(), new SCV(), new SCV(), new SCV(), new SCV(), new SCV()});
            foreach(Unit u in currentUnits)
            {
                u.production_Time_Left = 0;
            }
        }


        #region public bools
        public bool meetReqs(List<Structure_Name> Reqs)
        {
            var nameOfStructsDone =
                from structures in currentStructures
                where ((structures.production_Time_Left == 0) || (structures.production_Time_Left == null))
                select structures.name;

            if (!Reqs.Any())
                return true;

            if (nameOfStructsDone.Any())
            {
                foreach (Structure_Name arg in Reqs)
                {
                    if (nameOfStructsDone.Contains(arg))
                        continue;
                    else
                    {
                        SendString(" cant build; dont have a done " + arg);
                        return false;
                    }
                }
            }
            else
            {
                SendString(" cant build; dont have any done structures");
                return false;
            }

            return true;
        }
        public bool PmeetReqs(List<Structure_Name> Reqs)
        {
            if (!Reqs.Any())
                return true;

            foreach (Structure_Name arg in Reqs)
            {
                if (namesCurrentStructures.Contains(arg))
                    continue;
                else
                {
                    //SendString(" cant build; dont have a done/building "+ arg);
                    return false;
                }
            }
            return true;
        }
        public bool meetReqs(List<Upgrade_Name> Reqs)
        {
            if (Reqs.Any())
            {
                foreach (Upgrade_Name arg in Reqs)
                {
                    if (namesCurrentUpgrades.Contains(arg))
                        continue;
                    else
                    {
                        SendString(" doesnt have the required upgrade: " + arg);
                        return false;
                    }
                }
            }
            return true;
        }
        public bool canIAfford(Upgrade myUpgrade)
        {
            return (gas >= myUpgrade.gas_Required &&
                minerals >= myUpgrade.minerals_Required);
        }
        public bool canIAfford(Unit myUnit)
        {
            return (gas >= myUnit.gas_Required &&
                minerals >= myUnit.minerals_Required);
        }
        public bool canIAfford(Structure myStructure)
        {
            return (gas >= myStructure.gas_Required &&
                minerals >= myStructure.minerals_Required);
        }
        public bool canIAfford(Addon myAddon)
        {
            return (gas >= myAddon.gas_Required && minerals >= myAddon.minerals_Required);
        }
        public bool canTheyEat(Unit myUnit)
        {
            return (myUnit.population + unit_Count <= unit_Cap);
        }
        public bool isGettingGasAnIssue<T>(T myUnit) where T : IBuildable
        { return ((SCVTotalGas() > 0 && myUnit.gas_Required > 0) || (myUnit.gas_Required == 0)); } 
        #endregion

        #region unit totals
        public int Total<T>()
        { return currentUnits.Count(me => (me is T)) + currentStructures.Count(me => (me is T) && me.production_Time_Left == 0); }
        public int SCVTotalGas()
        { return currentUnits.Count(me => ((me is SCV) && (((SCV)me).onGas) && !(((SCV)me).building))); }
        public int SCVTotal()
        { return currentUnits.Count(me => ((me is SCV) && !(((SCV)me).onGas) && !(((SCV)me).building))); }
        public int MuleTotal()
        { return currentUnits.Count(me => me is Mule); } 
        #endregion

        public void Sendhome()
        {
            var Workin =
                from units in currentUnits
                where units is SCV
                where ((SCV)units).building
                select units;
            Unit jimmy = Workin.First();
            if (jimmy is SCV)
                ((SCV)jimmy).building = false;
        }

        #region output methods
        public void SendString(string str)
        {
            if (State.verbose)
                Console.WriteLine(str);
        }
        public void SendStringl(string str)
        {
            if (State.verbose)
                Console.Write(str);
        } 
        #endregion

        public void Time_Step(int seconds)
        {
            totalTime += seconds;
            //min update
            int CCTotal = namesCurrentStructures.Count(me => (me == Structure_Name.Command_Center));

            if (CCTotal > 0)
            {
                decimal Margin = Math.Ceiling((decimal)SCVTotal() / (decimal)CCTotal);

                if (Margin > 16)
                {
                    minerals = minerals + (int)(CCTotal * 16 * .65 * seconds) + 3 * MuleTotal(); //.72222
                }
                else
                {
                    if ((Margin < 16) && (Margin < 4))
                    {
                        minerals = minerals + (int)(SCVTotal() * .72 * seconds) + 3 * MuleTotal();//.7222
                    }
                    else//between 16 and 4 scvs per CC
                    {
                        minerals = minerals + (int)(SCVTotal() * .65 * seconds) + 3 * MuleTotal();//.658841
                    }
                }
            }
            else
            {
                str = String.Format("cant harvest minerals; no CC!");
                SendString(str);
            }

            //gas update
            int RefTotal = namesCurrentStructures.Count(me => (me == Structure_Name.Refinery));

            if (RefTotal > 0)
            {
                decimal Marging = Math.Ceiling((decimal)SCVTotalGas() / (decimal)RefTotal);
                if (Marging > 3)
                {
                    gas += (int)(RefTotal * 3 * .73333 * seconds);
                }
                else
                {
                    gas += (int)(SCVTotalGas() * .73333 * seconds);
                }
            }
            else
            {
                //SendString("cant harvest gas; no Refinery!");
            }


        }
        public void Display_Stats()
        {
            str=String.Format("");
            SendString(str);
            str=String.Format("we have {0} minerals and {1} gas", minerals, gas);
            SendString(str);
            str=String.Format("Our unit count is {0} out of {1}", unit_Count, unit_Cap);
            SendString(str);
            str = String.Format("we have {0} scv on min and {1} on gas", SCVTotal(), SCVTotalGas()); 
            SendString(str);
            str=String.Format("{0} is how many are building", currentUnits.Count(me => (me is SCV) && ((SCV)me).building));
            SendString(str);
        }
    }

    class CantBuildException : Exception
    {
        public CantBuildException()
            : base("we cant build that")
        {
        }
        public CantBuildException(string message)
            : base(message)
        {//nothing else
        }
    }

}
