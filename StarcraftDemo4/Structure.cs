using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    public abstract class Structure : IBuildable 
    {
        public static string str;
        public int minerals_Required { get; set; }
        public int gas_Required { get; set; }
        public int? production_Time_Left;
        public Structure_Name name;
        public List<Structure_Name> structure_Reqs;
        public Structure(
            List<Structure_Name> _structure_Reqs,
            int _minerals_Required,
            int _gas_Required,
            Structure_Name _name,
            int? _production_Time_Left)
        {
            structure_Reqs = _structure_Reqs;
            minerals_Required = _minerals_Required;
            gas_Required = _gas_Required;
            name = _name;
            production_Time_Left = _production_Time_Left;
        }

        public virtual void Time_Step(int seconds, State myState)
        {
            if (production_Time_Left != 0 && !(this is Addon))
            {
                production_Time_Left = Math.Max(((int)production_Time_Left - seconds), 0);
                if(production_Time_Left==0)
                {
                    str = "a " + this.name + " is finished";
                    SendString(str);
                    myState.Sendhome();
                }
            }

        }
        //marking a method  as abstract is only a template. the method still has to be defined. not like with non abstract methods in a derived type.
        public void SendString(string str)
        {
            if (State.verbose)
                Console.WriteLine(str);
        }
    }
    [Serializable]
    public class SupplyDepot :Structure
    {
        public SupplyDepot(
            List<Structure_Name> _structure_Reqs =null,
            int _minerals_Required = 100, 
            int _gas_Required = 0, 
            Structure_Name _name = Structure_Name.Supply_Depot,
            int? _production_Time_Left = 30) :
            base(_structure_Reqs, 
            _minerals_Required, 
            _gas_Required, 
            _name, 
            _production_Time_Left)
            {
                structure_Reqs = new List<Structure_Name>();
            }

        override public void Time_Step(int seconds, State myState)
        {

            if ((production_Time_Left - seconds <= 0) && (production_Time_Left != 0))//is going to finnish
            {
                myState.unit_Cap += 8;
                str = String.Format("supply depot is about to finish");
                SendString(str);
            }
            base.Time_Step(seconds, myState);

        }
    }
    [Serializable]
    public class Bunker : Structure
    {
        public Bunker(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 100,
            int _gas_Required = 0,
            Structure_Name _name = Structure_Name.Bunker,
            int? _production_Time_Left = 40) :
            base(_structure_Reqs,
            _minerals_Required,
            _gas_Required,
            _name,
            _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Barracks);
        }
    }
    [Serializable]
    public class Turret : Structure
    {
        public Turret(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 100,
            int _gas_Required = 0,
            Structure_Name _name = Structure_Name.Turret,
            int? _production_Time_Left = 25) :
            base(_structure_Reqs,
            _minerals_Required,
            _gas_Required,
            _name,
            _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Engineering_Bay);
        }
    }
    [Serializable]
    public class SensorTower : Structure
    {
        public SensorTower(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 125,
            int _gas_Required = 100,
            Structure_Name _name = Structure_Name.Sensor_Tower,
            int? _production_Time_Left = 25) :
            base(_structure_Reqs,
            _minerals_Required,
            _gas_Required,
            _name,
            _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Engineering_Bay);
        }
    }
    [Serializable]
    public class Refinery : Structure
    {
        public Refinery(
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 75,
            int _gas_Required = 0,
            Structure_Name _name = Structure_Name.Refinery,
            int? _production_Time_Left = 30) :
            base(_structure_Reqs,
            _minerals_Required,
            _gas_Required,
            _name,
            _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
        }
    }

}
