using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    [Serializable]
    public abstract class Upgrade : IBuildable 
    {
        public Upgrade_Name name;
        public int minerals_Required { get; set; }
        public int gas_Required { get; set; }
        public int? production_Time_Left;
        public Structure_Name whereResearched;
        public List<Structure_Name> structure_Reqs;
        public List<Upgrade_Name> upgrade_Reqs;
        public Upgrade(Upgrade_Name _name, int _minerals_Required, int _gas_Required, int _poduction_Time_Left, Structure_Name _whereResearched,
            List<Structure_Name> _structure_Reqs, List<Upgrade_Name> _upgrade_Reqs)
        {
            name = _name;
            minerals_Required = _minerals_Required;
            gas_Required = _gas_Required;
            production_Time_Left = _poduction_Time_Left;
            whereResearched = _whereResearched;
            structure_Reqs = _structure_Reqs;
            upgrade_Reqs = _upgrade_Reqs;
        }
    }
    //Weapons upgrades
    #region Weapons
    [Serializable]
    public class InfantryWeaponsLvl1 : Upgrade
    {
        public InfantryWeaponsLvl1(Upgrade_Name _name = Upgrade_Name.Infantry_Weapons_Level_1,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 160,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class InfantryWeaponsLvl2 : Upgrade
    {
        public InfantryWeaponsLvl2(Upgrade_Name _name = Upgrade_Name.Infantry_Weapons_Level_2,
            int _minerals_Required = 175,
            int _gas_Required = 175,
            int _poduction_Time_Left = 190,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Infantry_Weapons_Level_1);
            structure_Reqs.Add(Structure_Name.Armory);
        }
    }
    [Serializable]
    public class InfantryWeaponsLvl3 : Upgrade
    {
        public InfantryWeaponsLvl3(Upgrade_Name _name = Upgrade_Name.Infantry_Weapons_Level_3,
            int _minerals_Required = 250,
            int _gas_Required = 250,
            int _poduction_Time_Left = 220,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Infantry_Weapons_Level_2);
            structure_Reqs.Add(Structure_Name.Armory);
        }
    }
    [Serializable]
    public class VehicleWeaponsLvl1 : Upgrade
    {
        public VehicleWeaponsLvl1(Upgrade_Name _name = Upgrade_Name.Vehicle_Weapons_Level_1,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 160,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class VehicleWeaponsLvl2 : Upgrade
    {
        public VehicleWeaponsLvl2(Upgrade_Name _name = Upgrade_Name.Vehicle_Weapons_Level_2,
            int _minerals_Required = 175,
            int _gas_Required = 175,
            int _poduction_Time_Left = 190,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Vehicle_Weapons_Level_1);
        }
    }
    [Serializable]
    public class VehicleWeaponsLvl3 : Upgrade
    {
        public VehicleWeaponsLvl3(Upgrade_Name _name = Upgrade_Name.Vehicle_Weapons_Level_3,
            int _minerals_Required = 250,
            int _gas_Required = 250,
            int _poduction_Time_Left = 220,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Vehicle_Weapons_Level_2);
        }
    }
    [Serializable]
    public class ShipWeaponsLvl1 : Upgrade
    {
        public ShipWeaponsLvl1(Upgrade_Name _name = Upgrade_Name.Ship_Weapons_Level_1,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 160,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class ShipWeaponsLvl2 : Upgrade
    {
        public ShipWeaponsLvl2(Upgrade_Name _name = Upgrade_Name.Ship_Weapons_Level_2,
            int _minerals_Required = 175,
            int _gas_Required = 175,
            int _poduction_Time_Left = 190,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Ship_Weapons_Level_1);
        }
    }
    [Serializable]
    public class ShipWeaponsLvl3 : Upgrade
    {
        public ShipWeaponsLvl3(Upgrade_Name _name = Upgrade_Name.Ship_Weapons_Level_3,
            int _minerals_Required = 250,
            int _gas_Required = 250,
            int _poduction_Time_Left = 220,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Ship_Weapons_Level_2);
        }
    } 
    #endregion
    //Armor upgrades
    #region Armor
    [Serializable]
    public class InfantryArmorLvl1 : Upgrade
    {
        public InfantryArmorLvl1(Upgrade_Name _name = Upgrade_Name.Infantry_Armor_Level_1,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 160,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class InfantryArmorLvl2 : Upgrade
    {
        public InfantryArmorLvl2(Upgrade_Name _name = Upgrade_Name.Infantry_Armor_Level_2,
            int _minerals_Required = 175,
            int _gas_Required = 175,
            int _poduction_Time_Left = 190,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            structure_Reqs.Add(Structure_Name.Armory);
            upgrade_Reqs.Add(Upgrade_Name.Infantry_Armor_Level_1);
        }
    }
    [Serializable]
    public class InfantryArmorLvl3 : Upgrade
    {
        public InfantryArmorLvl3(Upgrade_Name _name = Upgrade_Name.Infantry_Armor_Level_3,
            int _minerals_Required = 250,
            int _gas_Required = 250,
            int _poduction_Time_Left = 220,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            structure_Reqs.Add(Structure_Name.Armory);
            upgrade_Reqs.Add(Upgrade_Name.Infantry_Armor_Level_2);
        }
    }
    [Serializable]
    public class VehiclePlatingLvl1 : Upgrade
    {
        public VehiclePlatingLvl1(Upgrade_Name _name = Upgrade_Name.Vehicle_Plating_Level_1,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 160,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class VehiclePlatingLvl2 : Upgrade
    {
        public VehiclePlatingLvl2(Upgrade_Name _name = Upgrade_Name.Vehicle_Plating_Level_2,
            int _minerals_Required = 175,
            int _gas_Required = 175,
            int _poduction_Time_Left = 190,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Vehicle_Plating_Level_1);
        }
    }
    [Serializable]
    public class VehiclePlatingLvl3 : Upgrade
    {
        public VehiclePlatingLvl3(Upgrade_Name _name = Upgrade_Name.Vehicle_Plating_Level_3,
            int _minerals_Required = 250,
            int _gas_Required = 250,
            int _poduction_Time_Left = 220,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Vehicle_Plating_Level_2);
        }
    }
    [Serializable]
    public class ShipPlatingLvl1 : Upgrade
    {
        public ShipPlatingLvl1(Upgrade_Name _name = Upgrade_Name.Ship_Plating_Level_1,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 160,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class ShipPlatingLvl2 : Upgrade
    {
        public ShipPlatingLvl2(Upgrade_Name _name = Upgrade_Name.Ship_Plating_Level_2,
            int _minerals_Required = 225,
            int _gas_Required = 225,
            int _poduction_Time_Left = 190,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Ship_Plating_Level_1);
        }
    }
    [Serializable]
    public class ShipPlatingLvl3 : Upgrade
    {
        public ShipPlatingLvl3(Upgrade_Name _name = Upgrade_Name.Ship_Plating_Level_3,
            int _minerals_Required = 300,
            int _gas_Required = 300,
            int _poduction_Time_Left = 220,
            Structure_Name _whereResearched = Structure_Name.Armory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
            upgrade_Reqs.Add(Upgrade_Name.Ship_Plating_Level_2);
        }
    } 
    #endregion
    //Speed Upgrade
    [Serializable]
        public class NitroPacks : Upgrade
        {
            public NitroPacks(Upgrade_Name _name = Upgrade_Name.Nitro_Packs,
                int _minerals_Required = 50,
                int _gas_Required = 50,
                int _poduction_Time_Left = 100,
                Structure_Name _whereResearched = Structure_Name.Barracks,
                List<Structure_Name> _structure_Reqs = null,
                List<Upgrade_Name> _upgrade_Reqs = null) :
                base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
                  _structure_Reqs, _upgrade_Reqs)
            {
                structure_Reqs = new List<Structure_Name>();
                upgrade_Reqs = new List<Upgrade_Name>();
                structure_Reqs.Add(Structure_Name.Factory);
            }
        }
    //Range Upgrade
    [Serializable]
        public class HiSecAutoTracking : Upgrade
        {
            public HiSecAutoTracking(Upgrade_Name _name = Upgrade_Name.HiSec_Auto_Tracking,
                int _minerals_Required = 100,
                int _gas_Required = 100,
                int _poduction_Time_Left = 80,
                Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
                List<Structure_Name> _structure_Reqs = null,
                List<Upgrade_Name> _upgrade_Reqs = null) :
                base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
                  _structure_Reqs, _upgrade_Reqs)
            {
                structure_Reqs = new List<Structure_Name>();
                upgrade_Reqs = new List<Upgrade_Name>();
            }
        }
    //Caster Ability Upgrades
    #region Caster
    [Serializable]
    public class StrikeCannons : Upgrade
    {
        public StrikeCannons(Upgrade_Name _name = Upgrade_Name.Strike_Cannons,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Factory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class CloakingField : Upgrade
    {
        public CloakingField(Upgrade_Name _name = Upgrade_Name.Cloaking_Field,
            int _minerals_Required = 200,
            int _gas_Required = 200,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class ConcussiveShells : Upgrade
    {
        public ConcussiveShells(Upgrade_Name _name = Upgrade_Name.Concussive_Shells,
            int _minerals_Required = 50,
            int _gas_Required = 50,
            int _poduction_Time_Left = 60,
            Structure_Name _whereResearched = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class PersonalCloaking : Upgrade
    {
        public PersonalCloaking(Upgrade_Name _name = Upgrade_Name.Personal_Cloaking,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 120,
            Structure_Name _whereResearched = Structure_Name.Ghost_Academy,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class SeekerMissile : Upgrade
    {
        public SeekerMissile(Upgrade_Name _name = Upgrade_Name.Seeker_Missile,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class SiegeTech : Upgrade
    {
        public SiegeTech(Upgrade_Name _name = Upgrade_Name.Siege_Tech,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 80,
            Structure_Name _whereResearched = Structure_Name.Factory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class StimPacks : Upgrade
    {
        public StimPacks(Upgrade_Name _name = Upgrade_Name.Stim_Packs,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 170,
            Structure_Name _whereResearched = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class WeaponRefit : Upgrade
    {
        public WeaponRefit(Upgrade_Name _name = Upgrade_Name.Weapon_Refit,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 60,
            Structure_Name _whereResearched = Structure_Name.Fusion_Core,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    } 
    #endregion
    //Reactors for more energy
    #region Reactors
    [Serializable]
    public class BehemothReactor : Upgrade
    {
        public BehemothReactor(Upgrade_Name _name = Upgrade_Name.Behemoth_Reactor,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 80,
            Structure_Name _whereResearched = Structure_Name.Fusion_Core,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class CaduceusReactor : Upgrade
    {
        public CaduceusReactor(Upgrade_Name _name = Upgrade_Name.Caduceus_Reactor,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 80,
            Structure_Name _whereResearched = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class CorvidReactor : Upgrade
    {
        public CorvidReactor(Upgrade_Name _name = Upgrade_Name.Corvid_Reactor,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class MoebiusReactor : Upgrade
    {
        public MoebiusReactor(Upgrade_Name _name = Upgrade_Name.Moebius_Reactor,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 80,
            Structure_Name _whereResearched = Structure_Name.Ghost_Academy,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    } 
    #endregion
    //Other
    #region Other
    [Serializable]
    public class BuildingArmor : Upgrade
    {
        public BuildingArmor(Upgrade_Name _name = Upgrade_Name.Building_Armor,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 140,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class CombatShield : Upgrade
    {
        public CombatShield(Upgrade_Name _name = Upgrade_Name.Combat_Shields,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Barracks,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class DurableMaterials : Upgrade
    {
        public DurableMaterials(Upgrade_Name _name = Upgrade_Name.Durable_Materials,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Starport,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class InfernalPreIgniter : Upgrade
    {
        public InfernalPreIgniter(Upgrade_Name _name = Upgrade_Name.Infernal_PreIgniter,
            int _minerals_Required = 150,
            int _gas_Required = 150,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Factory,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    }
    [Serializable]
    public class NeosteelFrame : Upgrade
    {
        public NeosteelFrame(Upgrade_Name _name = Upgrade_Name.Neosteel_Frame,
            int _minerals_Required = 100,
            int _gas_Required = 100,
            int _poduction_Time_Left = 110,
            Structure_Name _whereResearched = Structure_Name.Engineering_Bay,
            List<Structure_Name> _structure_Reqs = null,
            List<Upgrade_Name> _upgrade_Reqs = null) :
            base(_name, _minerals_Required, _gas_Required, _poduction_Time_Left, _whereResearched,
              _structure_Reqs, _upgrade_Reqs)
        {
            structure_Reqs = new List<Structure_Name>();
            upgrade_Reqs = new List<Upgrade_Name>();
        }
    } 
    #endregion

}

