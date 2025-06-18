using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    public interface IBuildable
    {
        int gas_Required { get; set; }
        int minerals_Required { get; set; }
    }

    public interface IAddon
    {
        Addon myAddon { get; set; }
        void Build(Addon buildAddon);
    }

    //13
    public enum Structure_Name
    {
        Command_Center, Barracks, Supply_Depot, Armory,
        Ghost_Academy, Engineering_Bay, Factory, Starport, Turret, Bunker, Sensor_Tower,
        Fusion_Core, Refinery, Reactor, Tech_Lab, Orbital_Command, Planetary_Fortress
    }
    public enum Units_Name
    {
        Marine, SCV, Marauder, Ghost, Reaper, Hellion, Siege_Tank,
        Thor, Viking, Medivac, Battlecruiser, Raven, Banshee, Mule
    }
    public enum Upgrade_Name
    {
        Infantry_Weapons_Level_1, Infantry_Weapons_Level_2, Infantry_Weapons_Level_3,
        Vehicle_Weapons_Level_1, Vehicle_Weapons_Level_2, Vehicle_Weapons_Level_3,
        Ship_Weapons_Level_1, Ship_Weapons_Level_2, Ship_Weapons_Level_3,
        Infantry_Armor_Level_1, Infantry_Armor_Level_2, Infantry_Armor_Level_3,
        Vehicle_Plating_Level_1, Vehicle_Plating_Level_2, Vehicle_Plating_Level_3,
        Ship_Plating_Level_1, Ship_Plating_Level_2, Ship_Plating_Level_3,
        HiSec_Auto_Tracking, Strike_Cannons, Cloaking_Field, Concussive_Shells, Personal_Cloaking,
        Seeker_Missile, Siege_Tech, Weapon_Refit, Behemoth_Reactor, Corvid_Reactor, Moebius_Reactor,
        Stim_Packs, Combat_Shields, Nitro_Packs, Caduceus_Reactor, Building_Armor, Durable_Materials,
        Infernal_PreIgniter, Neosteel_Frame
    }  

 }
