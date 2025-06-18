using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    public abstract class Unit :IBuildable
    {
        public Units_Name name;
        public int population;
        public int minerals_Required { get; set; }
        public int gas_Required { get; set; }
        public int? production_Time_Left;
        public Structure_Name whereTrained;
        public List<Structure_Name> structure_Reqs;
        public List<Upgrade_Name> upgrade_Reqs;
        public bool needTech;
        public bool canEat;
        public Unit(
            Units_Name _name, 
            int _population,
            int _minerals_Required,
            int  _gas_Required, 
            int? _production_Time_Left, 
            Structure_Name _whereTrained,
            List<Structure_Name> _structure_Reqs, 
            List<Upgrade_Name> _upgrade_Reqs,
            bool _needTech)
        {
            name = _name;
            population = _population;
            minerals_Required = _minerals_Required;
            gas_Required = _gas_Required;
            production_Time_Left = _production_Time_Left;
            whereTrained = _whereTrained;
            structure_Reqs = _structure_Reqs;
            upgrade_Reqs = _upgrade_Reqs;
            needTech = _needTech;
            canEat = true;
        }
    }
    [Serializable]
    public class EnergyUnit : Unit
    {
        public int energyRequired;
        public int lifeTime;
        public EnergyUnit(
            int _energyRequired,
            int _lifeTime,
            Units_Name _name,
            int _population = 0,
            int _minerals_Required = 0,
            int _gas_Required = 0,
            int? _production_Time_Left = 0,
            Structure_Name _whereTrained = Structure_Name.Command_Center,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false,
            bool _canEat = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {

            name = _name;
            energyRequired = _energyRequired;
            lifeTime = _lifeTime;
        }
    }
    [Serializable]
    public class Mule : EnergyUnit
    {
        public Mule(
            int _energyRequired = 50,
            int _lifeTime = 90,
            Units_Name _name = Units_Name.Mule,
            int _population = 0,
            int _minerals_Required = 0,
            int _gas_Required = 0,
            int? _production_Time_Left = 0,
            Structure_Name _whereTrained = Structure_Name.Command_Center,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false,
            bool _canEat = true) :
            base(_energyRequired, _lifeTime, _name, _population, _minerals_Required, 
            _gas_Required, _production_Time_Left, _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            structure_Reqs.Add(Structure_Name.Command_Center);
        }  
    }
    [Serializable]
    public class SCV : Unit
    {
        public bool onGas;
        public bool building = false;
        public SCV(
            int _population = 1,
            Units_Name _name = Units_Name.SCV,
            int _minerals_Required = 50,
            int _gas_Required = 0,
            int? _production_Time_Left = 17,
            Structure_Name _whereTrained = Structure_Name.Command_Center,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false,
            bool _onGas = false,
            bool _building = false) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Marine : Unit
    {
        public Marine(
            Units_Name _name = Units_Name.Marine,
            int _population = 1,
            int _minerals_Required = 50,
            int _gas_Required = 0,
            int? _production_Time_Left = 25,
            Structure_Name _whereTrained = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Marauder : Unit
    {
        public Marauder(
            Units_Name _name = Units_Name.Marauder,
            int _population = 2,
            int _minerals_Required = 100,
            int _gas_Required = 25,
            int? _production_Time_Left = 30,
            Structure_Name _whereTrained = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Reaper : Unit
    {
        public Reaper(
            Units_Name _name = Units_Name.Reaper,
            int _population = 1,
            int _minerals_Required = 50,
            int _gas_Required = 50,
            int? _production_Time_Left = 45,
            Structure_Name _whereTrained = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Ghost : Unit
    {
        public Ghost(
            Units_Name _name = Units_Name.Ghost,
            int _population = 2,
            int _minerals_Required = 200,
            int _gas_Required = 100,
            int? _production_Time_Left = 40,
            Structure_Name _whereTrained = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Ghost_Academy);
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Hellion : Unit
    {
        public Hellion(
            Units_Name _name = Units_Name.Hellion,
            int _population = 2,
            int _minerals_Required = 100,
            int _gas_Required = 0,
            int? _production_Time_Left = 30,
            Structure_Name _whereTrained = Structure_Name.Factory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class SiegeTank : Unit
    {
        public SiegeTank(
            Units_Name _name = Units_Name.Siege_Tank,
            int _population = 3,
            int _minerals_Required = 150,
            int _gas_Required = 125,
            int? _production_Time_Left = 50,
            Structure_Name _whereTrained = Structure_Name.Factory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Thor : Unit
    {
        public Thor(
            Units_Name _name = Units_Name.Thor,
            int _population = 6,
            int _minerals_Required = 300,
            int _gas_Required = 200,
            int? _production_Time_Left = 60,
            Structure_Name _whereTrained = Structure_Name.Factory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Armory);
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Viking : Unit
    {
        public Viking(
            Units_Name _name = Units_Name.Viking,
            int _population = 2,
            int _minerals_Required = 150,
            int _gas_Required = 75,
            int? _production_Time_Left = 42,
            Structure_Name _whereTrained = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Medivac : Unit
    {
        public Medivac(
            Units_Name _name = Units_Name.Medivac,
            int _population = 2,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int? _production_Time_Left = 42,
            Structure_Name _whereTrained = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = false) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Raven : Unit
    {
        public Raven(
            Units_Name _name = Units_Name.Raven,
            int _population = 2,
            int _minerals_Required = 100,
            int _gas_Required = 200,
            int? _production_Time_Left = 60,
            Structure_Name _whereTrained = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Banshee : Unit
    {
        public Banshee(
            Units_Name _name = Units_Name.Banshee,
            int _population = 3,
            int _minerals_Required = 150,
            int _gas_Required = 100,
            int? _production_Time_Left = 60,
            Structure_Name _whereTrained = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class Battlecruiser : Unit
    {
        public Battlecruiser(
            Units_Name _name = Units_Name.Battlecruiser,
            int _population = 6,
            int _minerals_Required = 400,
            int _gas_Required = 300,
            int? _production_Time_Left = 90,
            Structure_Name _whereTrained = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null,
            bool _needTech = true) :
            base(_name, _population, _minerals_Required, _gas_Required, _production_Time_Left,
            _whereTrained, _structure_Reqs, _upgrade_Reqs, _needTech)
        {
            structure_Reqs = new List<Structure_Name>();
            structure_Reqs.Add(Structure_Name.Fusion_Core);
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }

}
