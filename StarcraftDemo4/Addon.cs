using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    public class Addon : Structure
    {
        public Structure_Name where_Added;

        //public int? production_Time_Left;
        //public Structure_Name name;
        //public int gas_Required;
        //public int minerals_Required;
        //List<Structure_Name> structure_Reqs;
        public Addon(
            Structure_Name _where_Added,
            List<Structure_Name> _structure_Reqs,
            int _minerals_Required,
            int _gas_Required,
            Structure_Name _name,
            int? _production_Time_Left)
            : base(_structure_Reqs, _minerals_Required, _gas_Required, _name, _production_Time_Left)
        {
            where_Added = _where_Added;
        }
        public bool HasReactorDone()
        {
            return ((production_Time_Left == 0) && (this.name == Structure_Name.Reactor));
        }
        public bool HasTechDone()
        {
            return ((production_Time_Left == 0) && (this.name == Structure_Name.Tech_Lab));
        }
    }
    [Serializable]
    public class TechLab : Addon
    {
        public TechLab( 
            Structure_Name _where_Added,
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 50,
            int _gas_Required = 25, 
            Structure_Name _name = Structure_Name.Tech_Lab,
            int? _production_Time_Left = 25):
            base(_where_Added, _structure_Reqs, _minerals_Required, _gas_Required, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            //no default where added,this must be set.
        }

    }
    [Serializable]
    public class Reactor : Addon
    {
        public Reactor(
            Structure_Name _where_Added,
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 50,
            int _gas_Required = 50,
            Structure_Name _name = Structure_Name.Reactor,
            int? _production_Time_Left = 50) :
            base(_where_Added, _structure_Reqs, _minerals_Required, _gas_Required, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            //no default where added,this must be set.
        }

    }
    [Serializable]
    public class OrbitalCommand : Addon
    {
        public int Energy
        {
            get { return energy; }
            set
            {
                if (value > 200)
                    energy = 200;
                else
                    energy = value;
            }
        }
        private int energy;
        public OrbitalCommand(
            Structure_Name _where_Added = Structure_Name.Command_Center,
            List<Structure_Name> _structure_Reqs = null,
            int _minerals_Required = 150,
            int _gas_Required = 0,
            Structure_Name _name = Structure_Name.Orbital_Command,
            int? _production_Time_Left = 35,
            int _Energy = 50) :
            base(_where_Added, _structure_Reqs, _minerals_Required, _gas_Required, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Barracks);
            Energy =50;
            //no default where added,this must be set.
        }
    }
    [Serializable]
    public class PlanetaryFortress : Addon
    {
        public PlanetaryFortress(
            Structure_Name _where_Added = Structure_Name.Command_Center,
            List<Structure_Name> _structure_Reqs = null,
            int _gas_Required = 150,
            int _minerals_Required = 150,
            Structure_Name _name = Structure_Name.Planetary_Fortress,
            int? _production_Time_Left = 50) :
            base(_where_Added, _structure_Reqs, _minerals_Required, _gas_Required, _name, _production_Time_Left)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Engineering_Bay);
            //no default where added,this must be set.
        }
    }
}
