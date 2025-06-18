using System;


namespace StarcraftDemo4
{
    class Test
    {
        private static Player Jason = new Player();

        private static Player Jason2 = new Player();

        #region testmanualgame
        private static void testBuilding()
        {
            SupplyDepot SD1 = new SupplyDepot();
            Jason.Build(SD1);
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Build(new SupplyDepot());
            Jason.Time_Step(30);
            Jason.Build(new Barracks());
            Jason.Build(new Barracks());
            Jason.Build(new Barracks());
            Jason.Build(new Barracks());
            Jason.Build(new Barracks());
            Jason.Time_Step(60);
            Jason.Build(new Factory());
            Jason.Build(new Factory());
            Jason.Build(new Factory());
            Jason.Build(new Factory());
            Jason.Build(new Factory());
            Jason.Build(new Factory());
            Jason.Time_Step(60);
            Jason.Build(new Starport());
            Jason.Build(new Starport());
            Jason.Build(new Starport());
            Jason.Build(new Starport());
            Jason.Build(new Starport());
            Jason.Time_Step(50);
            Jason.Build(new FusionCore());
            Jason.Build(new FusionCore());
            Jason.Build(new FusionCore());
            Jason.Build(new FusionCore());
            Jason.Build(new GhostAcademy());
            Jason.Build(new GhostAcademy());
            Jason.Build(new GhostAcademy());
            Jason.Build(new GhostAcademy());
            Jason.Build(new Armory());
            Jason.Build(new Armory());
            Jason.Build(new Armory());
            Jason.Build(new Armory());
            Jason.Build(new Bunker());
            Jason.Build(new CommandCenter());
            Jason.Build(new CommandCenter());
            Jason.Time_Step(100);
            Jason.Build(new EngineeringBay());
            Jason.Build(new EngineeringBay());
            Jason.Build(new EngineeringBay());
            Jason.Build(new EngineeringBay());
            Jason.Time_Step(70);
            Jason.Build(new Turret());
            Jason.Build(new Turret());
            Jason.Build(new Turret());
            Jason.Build(new SensorTower());
            Jason.Build(new Refinery());
            Jason.Time_Step(80);


        }

        private static void testAddon()
        {
            Jason.Build(new TechLab(Structure_Name.Barracks));
            Jason.Build(new TechLab(Structure_Name.Barracks));
            Jason.Build(new TechLab(Structure_Name.Barracks));
            Jason.Build(new TechLab(Structure_Name.Factory));
            Jason.Build(new TechLab(Structure_Name.Factory));
            Jason.Build(new TechLab(Structure_Name.Factory));
            Jason.Build(new TechLab(Structure_Name.Factory));
            Jason.Build(new TechLab(Structure_Name.Starport));
            Jason.Build(new TechLab(Structure_Name.Starport));
            Jason.Build(new TechLab(Structure_Name.Starport));
            Jason.Build(new TechLab(Structure_Name.Starport));
            Jason.Build(new Reactor(Structure_Name.Factory));
            Jason.Build(new Reactor(Structure_Name.Starport));
            Jason.Build(new Reactor(Structure_Name.Barracks));
            Jason.Build(new OrbitalCommand(Structure_Name.Command_Center));
            Jason.Build(new PlanetaryFortress(Structure_Name.Command_Center));
            Jason.Time_Step(70);

            //             Jason.LiftoffFromAddOn(Structure_Name.Barracks, Structure_Name.Reactor);
            //             Jason.LiftoffFromAddOn(Structure_Name.Barracks, Structure_Name.Tech_Lab);
            //             Jason.LiftoffFromAddOn(Structure_Name.Barracks, Structure_Name.Tech_Lab);
            //             Jason.LiftoffFromAddOn(Structure_Name.Factory, Structure_Name.Reactor);
            //             Jason.LiftoffFromAddOn(Structure_Name.Factory, Structure_Name.Tech_Lab);
            //             Jason.LiftoffFromAddOn(Structure_Name.Factory, Structure_Name.Tech_Lab);
            //             Jason.LiftoffFromAddOn(Structure_Name.Starport, Structure_Name.Reactor);
            //             Jason.LiftoffFromAddOn(Structure_Name.Starport, Structure_Name.Tech_Lab);
            //             Jason.LiftoffFromAddOn(Structure_Name.Starport, Structure_Name.Tech_Lab);

        }

        private static void testUnits()
        {
            Jason.Build(new Ghost());
            Jason.Build(new Marauder());
            Jason.Build(new Reaper());
            Jason.Build(new Marine());
            Jason.Build(new Marine());
            Jason.Build(new Banshee());
            Jason.Build(new Battlecruiser()); //takes 90!
            Jason.Build(new Raven());
            Jason.Build(new Viking());
            Jason.Build(new Viking());
            Jason.Build(new SiegeTank());
            Jason.Build(new Thor()); //takes 60!
            Jason.Build(new Hellion());
            Jason.Build(new Hellion());
            Jason.Build(new SCV());
            Jason.Build(new SCV());
            Jason.Time_Step(91);

        }

        private static void testUpgrade()
        {
            Jason.Build(new InfantryWeaponsLvl1());
            Jason.Build(new VehicleWeaponsLvl1());
            Jason.Build(new ShipWeaponsLvl1());
            Jason.Build(new InfantryArmorLvl1());
            Jason.Build(new VehiclePlatingLvl1());
            Jason.Build(new ShipPlatingLvl1());
            Jason.Time_Step(220);
            Jason.Build(new InfantryWeaponsLvl2());
            Jason.Build(new VehicleWeaponsLvl2());
            Jason.Build(new ShipWeaponsLvl2());
            Jason.Build(new InfantryArmorLvl2());
            Jason.Build(new VehiclePlatingLvl2());
            Jason.Build(new ShipPlatingLvl2());
            Jason.Time_Step(220);
            Jason.Build(new InfantryWeaponsLvl3());
            Jason.Build(new VehicleWeaponsLvl3());
            Jason.Build(new ShipWeaponsLvl3());
            Jason.Build(new InfantryArmorLvl3());
            Jason.Build(new VehiclePlatingLvl3());
            Jason.Build(new ShipPlatingLvl3());
            Jason.Time_Step(220);

            Jason.Build(new NitroPacks());
            Jason.Build(new HiSecAutoTracking());
            Jason.Build(new StrikeCannons());
            Jason.Build(new CloakingField());
            Jason.Time_Step(220);
            Jason.Build(new ConcussiveShells());
            Jason.Build(new PersonalCloaking());
            Jason.Build(new SeekerMissile());
            Jason.Build(new SiegeTech());
            Jason.Time_Step(220);
            Jason.Build(new StimPacks());
            Jason.Build(new WeaponRefit());
            Jason.Build(new BehemothReactor());
            Jason.Time_Step(220);
            Jason.Build(new CaduceusReactor());
            Jason.Build(new CorvidReactor());
            Jason.Build(new BuildingArmor());
            Jason.Time_Step(220);
            Jason.Build(new CombatShield());
            Jason.Build(new DurableMaterials());
            Jason.Build(new InfernalPreIgniter());
            Jason.Build(new NeosteelFrame());
            Jason.Build(new InfantryArmorLvl1());
        }

        private static void testSwap()
        {
            Jason2.Build(new SupplyDepot());
            Jason2.Time_Step(50);
            Jason2.Build(new Barracks());
            Jason2.Time_Step(79);
            Jason2.Build(new Factory());
            Jason2.Time_Step(79);
            Jason2.Build(new TechLab(Structure_Name.Factory));
            Jason2.Build(new Reactor(Structure_Name.Barracks));
            Jason2.Time_Step(60);
            //hot swap
            Jason2.Hot_Swap(Structure_Name.Barracks, Structure_Name.Reactor,
                Structure_Name.Factory, Structure_Name.Tech_Lab);
            Jason2.Hot_Swap(Structure_Name.Barracks, Structure_Name.Tech_Lab,
                Structure_Name.Factory, Structure_Name.Reactor);

            Jason2.LiftoffFromAddOn(Structure_Name.Barracks, Structure_Name.Reactor);
            Jason2.LiftoffFromAddOn(Structure_Name.Factory, Structure_Name.Tech_Lab);
            Jason2.LandonAddOn(Structure_Name.Factory, Structure_Name.Reactor);
            Jason2.LandonAddOn(Structure_Name.Barracks, Structure_Name.Tech_Lab);
        }

        private static void testmingasgrow()
        {
            Jason2.Build(new SupplyDepot());
            Jason2.Build(new SupplyDepot());
            Jason2.Build(new SupplyDepot());
            Jason2.Build(new CommandCenter());
            Jason2.Build(new Refinery());
            Jason2.Build(new Refinery());
            Jason2.Time_Step(100);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.Build(new SCV());
            Jason2.Time_Step(20);
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.PutOnGas();
            Jason2.Time_Step(20);
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
            Jason2.PutOnMinerals();
        } 
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("StarCraft Demo 4 - Build Order Simulator");
            Console.WriteLine("Running 3 simulation games...");
            
            GameOptimizer.PlayBaseGames(3);
            
            Console.WriteLine("Simulation complete! Press any key to exit...");
            Console.ReadKey();
        }
    }
}


//SoapFormatter soapFormat = new SoapFormatter();
//using (Stream fStream2 = new FileStream("firstmove2.dat", FileMode.Create, FileAccess.Write, FileShare.None))
//{
//    soapFormat.Serialize(fStream2, myFirstMove);
//}
