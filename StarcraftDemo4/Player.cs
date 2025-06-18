using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StarcraftDemo4
{
    public class Player
    {
        public static string str;
        public State myState = new State();
        /// <summary>
        /// Moves
        /// </summary>
        /// <param name="myStructure"></param>

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

        #region real moves
        public void Build(Upgrade myUpgrade)
        {
            if (myState.meetReqs(myUpgrade.structure_Reqs) && myState.canIAfford(myUpgrade) && myState.meetReqs(myUpgrade.upgrade_Reqs))
            {
                bool UpgradeX = DoINeedTech(myUpgrade);
                if (UpgradeX)
                {
                    var PotentialProducingStructures =
                    from ppsn in myState.currentStructures
                    where ppsn is ExtendableProducingStructure
                    where ((ExtendableProducingStructure)ppsn).myAddon != null
                    where ((ExtendableProducingStructure)ppsn).myAddon.HasTechDone()
                    where ((ppsn.production_Time_Left == 0) || (ppsn.production_Time_Left == null))
                    where ppsn.name == myUpgrade.whereResearched
                    where !((ProducingStructure)ppsn).upgradeQueFull
                    select ppsn;
                    //there is a struct available            
                    if (PotentialProducingStructures.Any())
                    {
                        if (PotentialProducingStructures.First() is ExtendableProducingStructure)
                        {
                            ((ProducingStructure)PotentialProducingStructures.First()).Build(myUpgrade);
                            str = String.Format("upgrading {0} at {1}", myUpgrade.name, myUpgrade.whereResearched);
                            SendString(str);
                            myState.minerals -= myUpgrade.minerals_Required;
                            myState.gas -= myUpgrade.gas_Required;
                            myState.Moves.Add("upgrading " + myUpgrade.name +
    " at " + myUpgrade.whereResearched + " with " + myState.minerals + " min and " +
    myState.gas + " at time " + myState.totalTime);
                        }

                    }
                    else
                    { throw new CantBuildException("cant upgrade " + myUpgrade.name + " since there are no open " + myUpgrade.whereResearched); }

                }
                else
                {
                    var PotentialProducingStructures =
                    from ppsn in myState.currentStructures
                    where ppsn is ProducingStructure
                    where ((ppsn.production_Time_Left == 0) || (ppsn.production_Time_Left == null))
                    where ppsn.name == myUpgrade.whereResearched
                    where !((ProducingStructure)ppsn).upgradeQueFull
                    select ppsn;
                    //there is a struct available

                    if (PotentialProducingStructures.Any())
                    {
                        ((ProducingStructure)PotentialProducingStructures.First()).Build(myUpgrade);
                        str = String.Format("upgrading {0} at {1}", myUpgrade.name, myUpgrade.name);
                        SendString(str);
                        myState.minerals -= myUpgrade.minerals_Required;
                        myState.gas -= myUpgrade.gas_Required;
                        myState.Moves.Add("upgrading " + myUpgrade.name +
" at " + myUpgrade.whereResearched + " with " + myState.minerals + " min and " +
myState.gas + " at time " + myState.totalTime);
                    }
                    else
                    { throw new CantBuildException("cant upgrade " + myUpgrade.name + " since there are no open " + myUpgrade.whereResearched); }
                }
            }
            else
            {
                //doesnt meet structure reqs
                throw new CantBuildException("cant upgrade " + myUpgrade.name + ", failed in structure/upgrade/mineral reqs check");
            }
        }

        public void Build(Structure myStructure)
        {
            if (myState.canIAfford(myStructure))
            {
                if (myState.meetReqs(myStructure.structure_Reqs))
                {
                    if (myState.namesCurrentUnits.Contains(Units_Name.SCV) && (myState.SCVTotal() > 0))
                    {
                        myState.currentStructures.Add(myStructure);
                        myState.gas -= myStructure.gas_Required;
                        myState.minerals -= myStructure.minerals_Required;
                        str = "Starting a " + myStructure.name + " with " + myState.minerals + " min and " +
                            myState.gas + " at time " + myState.totalTime;
                        myState.Moves.Add(str);
                        SendString(str);
                        ((SCV)myState.currentUnits.First(me => ((me is SCV) && !((SCV)me).onGas &&
                              !((SCV)me).building))).building = true;
                        //this is changed when the structure finishes.
                    }
                    else
                        throw new CantBuildException("no SCVs to help us do our work!");
                }
                else
                    throw new CantBuildException("Cant build a " + myStructure.name +
                        ", dont have all the reqs like maybe a" + myStructure.structure_Reqs.First());
            }
            else
                throw new CantBuildException("Cant build a " + myStructure.name + ", not enough min/gas");
        }

        public void Build(Unit myUnit)
        {
            if (canIBuildUnit(myUnit))
            {
                bool sitefound;
                ProducingStructure site;
                LookForTrainingSite(out sitefound, out site, myUnit);

                if (sitefound)
                {
                    if (site is ExtendableProducingStructure)
                        ((ExtendableProducingStructure)site).Build(myUnit);
                    else
                        site.Build(myUnit);

                    
                    myState.unit_Count += myUnit.population;
                    myState.minerals -= myUnit.minerals_Required;
                    myState.gas -= myUnit.gas_Required;
                    string str = "training a " + myUnit.name + " at " + myUnit.whereTrained + " with "
                        + myState.minerals + " min and " + myState.gas + " at time " + myState.totalTime;
                    SendString(str);
                    myState.Moves.Add(str);
                }
                else
                { throw new CantBuildException("cant train " + myUnit.name + " since there are no open " + myUnit.whereTrained); }
            }
            else
            { throw new CantBuildException("cant train " + myUnit.name + ", failed in structure/upgrade/mineral/cap reqs check"); }
        }

        public void Build(Addon myAddon)
        {
            if (myState.canIAfford(myAddon) &&
                myState.namesCurrentStructures.Contains(myAddon.where_Added) &&
                myState.meetReqs(myAddon.structure_Reqs))
            {
                //need to be done CC without an addon already.
                //we seperate these two (XPS and CC) out since not every PS has a myAddon class.
                IAddon site;
                bool sitefound;

                LookForAddonSite(out sitefound, out site, myAddon);
                if (sitefound)
                {
                    site.Build(myAddon);
                    myState.minerals -= myAddon.minerals_Required;
                    myState.gas -= myAddon.gas_Required;
                    str = "adding a " + myAddon.name + " at " + myAddon.where_Added +
                        " with " + myState.minerals + " min and " + myState.gas + " at time " + myState.totalTime;
                    myState.Moves.Add(str);
                    SendString(str);
                }
                else
                { throw new CantBuildException("Cant add a " + myAddon.name + " since there are no open " + myAddon.where_Added); }
            }
            else
            {
                throw new CantBuildException("cant add a " + myAddon.name + " to the " + myAddon.where_Added +
                "failed the min/gas/where_added/req check");
            }
        }

        public void Build(EnergyUnit myEUnit)
        {
            CommandCenter site;
            bool sitefound;
            LookForCastSite(out sitefound, out site, myEUnit);
            if (sitefound)
            {
                site.Cast(myEUnit);
                myState.currentUnits.Add(myEUnit);
                string str = "throwing down a" + myEUnit.name + " with " + myState.minerals +
                    " min and " + myState.gas + " at time " + myState.totalTime;
                SendString(str);
                myState.Moves.Add(str);
            }
            else
                throw new CantBuildException("cant cast a " + myEUnit.name + "no CCs with open orbitals with enough energy");
        }

        public void Hot_Swap(Structure_Name xPSBuilding, Structure_Name itsAddon,
            Structure_Name xPSBuilding2, Structure_Name itsAddon2)
        {
            try
            {
                //LiftoffFromAddon is already 2 seconds and so im makeing it 3
                LiftoffFromAddOn(xPSBuilding, itsAddon);
                LiftoffFromAddOn(xPSBuilding2, itsAddon2);
                LandonAddOn_Fast(xPSBuilding2, itsAddon);
                LandonAddOn_Fast(xPSBuilding, itsAddon2);
                Time_Step(3);
                string str = String.Format("lifted a {0} from a {1} and a {2} from a {3} and swaped",
    xPSBuilding, itsAddon, xPSBuilding2, itsAddon2);
                SendString(str);

            }
            catch (CantBuildException e)
            {
                SendString("this problem above occured in a hot swap: " + e.Message);
                throw new CantBuildException(e.Message);
            }
        }

        public void LandonAddOn(Structure_Name xPSBuilding, Structure_Name getAddon)
        {
            LandonAddOn_Fast(xPSBuilding, getAddon);
            Time_Step(2);
            string str = "Landing a" + xPSBuilding + " on a " + getAddon + " with " + myState.minerals + " min and " + myState.gas + " at time " + myState.totalTime;
            SendString(str);
            myState.Moves.Add(str);
        }

        public void LiftoffFromAddOn(Structure_Name xPSBuilding, Structure_Name itsAddon)
        {
            //a lifted building without an addon is treated the same as a regular building.
            //you can land on an addon no matter your previous state of addonness.

            //input check.
            var XPSs =
                from structure in myState.currentStructures
                where structure.name == xPSBuilding
                where structure is ExtendableProducingStructure
                where ((ExtendableProducingStructure)structure).myAddon != null
                where ((ExtendableProducingStructure)structure).myAddon.name == itsAddon
                where ((ExtendableProducingStructure)structure).myAddon.production_Time_Left == 0
                where !((ExtendableProducingStructure)structure).unitQueFull
                where !((ExtendableProducingStructure)structure).upgradeQueFull

                select structure;
            if (XPSs.Any())
            {
                myState.currentStructures.Add(((ExtendableProducingStructure)XPSs.First()).myAddon);
                ((ExtendableProducingStructure)XPSs.First()).myAddon = null;
                Time_Step(2);
                str = "Lifting a" + xPSBuilding + " from a " + itsAddon + " with " +
                    myState.minerals + " min and " + myState.gas + " at time " + myState.totalTime;
                SendString(str);
                myState.Moves.Add(str);
            }
            else
            { throw new CantBuildException("cant lift off a" + xPSBuilding + "; its not an XPS with that addon done"); }

        }

        public void PutOnGas()
        {
            var PossibleSCV =
               from units in myState.currentUnits
               where units.name == Units_Name.SCV
               where !((SCV)units).onGas
               select units;
            if (PossibleSCV.Any() && myState.namesCurrentStructures.Contains(Structure_Name.Refinery))
            {
                ((SCV)PossibleSCV.First()).onGas = true;
                str = "Putting an SCV on gas" + " with " + myState.minerals + " min and " + myState.gas +
                    " at time " + myState.totalTime;
                myState.Moves.Add(str);
                SendString(str);
            }
            else
                throw new CantBuildException("All existing SCVs are on gas already or we dont have a refinery");
        }

        public void PutOnMinerals()
        {
            var PossibleSCV =
               from units in myState.currentUnits
               where units.name == Units_Name.SCV
               where ((SCV)units).onGas
               select units;
            if (PossibleSCV.Any())
            {
                ((SCV)PossibleSCV.First()).onGas = false;
                myState.Moves.Add("Putting an SCV on minerals" + " with " + myState.minerals +
                    " min and " + myState.gas + " at time " + myState.totalTime);
            }
            else
                throw new CantBuildException("All existing SCVs are on minerals already");
        }
        #endregion


        #region protected checks

        protected void LandonAddOn_Fast(Structure_Name xPSBuilding, Structure_Name getAddon)
        {//this takes a building(possibly with an addon) and moved it onto an addon.

            bool sitefound;
            ExtendableProducingStructure theSite;
            CanILiftOff(out sitefound, out theSite, xPSBuilding);

            var AddonPos =
                from addonpos in myState.currentStructures
                where addonpos is Addon
                where addonpos.name == getAddon
                select addonpos;

            Addon theAddon = null;
            if (AddonPos.Any())
                theAddon = (Addon)AddonPos.First();
            if (sitefound && AddonPos.Any())
            {
                myState.currentStructures.Remove(theAddon);
                myState.currentStructures.Add(theSite.myAddon);
                if (theSite.myAddon != null)
                {
                    myState.currentStructures.Add(theSite.myAddon);
                    str = String.Format("Lifting from a {0} and landing on a {1}", theSite.myAddon.name, theAddon);
                    theSite.myAddon = null;

                }
                theSite.myAddon = theAddon;
                str = String.Format("Landing on a {0    }", theAddon);
                SendString(str);
            }
            else
                throw new CantBuildException("cant find available" + xPSBuilding + " or addon; " + theAddon.name);
        }

        private void CanILiftOff(out bool sitefound, out ExtendableProducingStructure site, Structure_Name xPSBuilding)
        {
            var Possible =
                from possible in myState.currentStructures
                where possible is ExtendableProducingStructure
                where possible.name == xPSBuilding
                where ((ExtendableProducingStructure)possible).myAddon != null
                && ((ExtendableProducingStructure)possible).myAddon.production_Time_Left == 0
                || ((ExtendableProducingStructure)possible).myAddon == null
                where !((ExtendableProducingStructure)possible).unitQueFull
                where !((ExtendableProducingStructure)possible).upgradeQueFull
                select possible;

            sitefound = Possible.Any();
            site = (ExtendableProducingStructure)Possible.First();

        }

        private void LookForTrainingSiteWithTech(out bool sitefound, out ExtendableProducingStructure site, Unit myUnit)
        {
            var PotentialProducingStructures =
                from ppsn in myState.currentStructures
                where ppsn is ExtendableProducingStructure
                where ((ppsn.production_Time_Left == 0) || (ppsn.production_Time_Left == null))
                where ppsn.name == myUnit.whereTrained
                where ((ExtendableProducingStructure)ppsn).myAddon != null
                where ((ExtendableProducingStructure)ppsn).myAddon.HasTechDone()
                where !((ProducingStructure)ppsn).unitQueFull
                select ppsn;
            sitefound = PotentialProducingStructures.Any();
            if (sitefound)
                site = (ExtendableProducingStructure)PotentialProducingStructures.First();
            else
                site = null;
        }

        protected void LookForCastSite(out bool sitefound, out CommandCenter site, EnergyUnit myEUnit)
        {
            var PossibleCCs =
                from cc in myState.currentStructures
                where cc is CommandCenter
                where ((CommandCenter)cc).hasOrbital()
                where (((OrbitalCommand)((CommandCenter)cc).myAddon).Energy >= myEUnit.energyRequired)
                select cc;
            sitefound = PossibleCCs.Any();

            if (sitefound)
                site = (CommandCenter)PossibleCCs.First();
            else
                site = null;
        }


        protected void LookForTrainingSite(out bool sitefound, out ProducingStructure site, Unit myUnit)
        {
            if (myUnit.needTech)
            {
                ExtendableProducingStructure site_w_tech;
                LookForTrainingSiteWithTech(out sitefound, out site_w_tech, myUnit);
                site = site_w_tech;
            }
            else
            {
                var PotentialProducingStructures =
                        from ppsn in myState.currentStructures
                        where ppsn is ProducingStructure
                        where ((ppsn.production_Time_Left == 0) || (ppsn.production_Time_Left == null))
                        where ppsn.name == myUnit.whereTrained
                        where !((ProducingStructure)ppsn).unitQueFull
                        select ppsn;

                sitefound = PotentialProducingStructures.Any();
                if (sitefound)
                    site = (ProducingStructure)PotentialProducingStructures.First();
                else
                    site = null;
            }
        }


        protected void LookForAddonSite(out bool sitefound, out IAddon site, Addon myAddon)
        {
            var PossibleAddon =
               from structure in myState.currentStructures
               where structure.name == myAddon.where_Added
               where structure is IAddon
               where structure is ProducingStructure
               where (structure.production_Time_Left == 0) || (structure.production_Time_Left == null)
               where (((IAddon)structure).myAddon == null)
               where !((ProducingStructure)structure).unitQueFull
               where !((ProducingStructure)structure).upgradeQueFull
               select structure;
            sitefound = PossibleAddon.Any();
            if (sitefound)
                site = (IAddon)PossibleAddon.First();
            else
                site = null;
        }

        protected bool canIBuildUnit(Unit myUnit)
        {
            return myState.canTheyEat(myUnit) &&
                  myState.canIAfford(myUnit) &&
                  myState.meetReqs(myUnit.structure_Reqs);
        }

        #endregion

        public void Time_Step(int seconds)
        {
            myState.Time_Step(seconds);
            foreach (Structure structure in myState.currentStructures)
            {
                structure.Time_Step(seconds, myState);
            }
        }

        public virtual void Display_Stats()
        {
            SendString("     ********************STATS******");
            foreach (Structure structure in myState.currentStructures.OrderBy(me => me.minerals_Required))
                if (structure.production_Time_Left > 0)
                {
                    str = String.Format("{0} isnt done building, it has {1} seconds left",
                        structure.name, structure.production_Time_Left);
                    SendString(str);
                }
                else
                {
                    SendString(structure.name + "has been succesfully built");
                    if (structure is ProducingStructure)
                        ((ProducingStructure)structure).Display_Stats();
                }


            foreach (Unit unit in myState.currentUnits.OrderBy(me => me.minerals_Required))
            {
                if (unit.production_Time_Left > 0)
                    SendString(unit.name + " isnt done training, it has" + unit.production_Time_Left + " seconds left ");
                else
                {
                    SendStringl(unit.name.ToString() + " ");
                }
            }

            foreach (Upgrade upgrade in myState.currentUpgrades.OrderBy(me => me.minerals_Required))
            {
                if (upgrade.production_Time_Left > 0)
                {
                    str = String.Format("{0} isnt done training, it has {1} seconds left", upgrade.name, upgrade.production_Time_Left);
                    SendStringl(str + " ");
                }
                else
                {
                    SendStringl(upgrade.name.ToString() + " ");
                }
            }
            myState.Display_Stats();
            str = String.Format("   **************   ");
            SendString(str);
        }

        protected bool DoINeedTech(Upgrade myUpgrade)
        {
            //if the upgrade is researched at an XPS then we need a tech lab.

            var Possibility =
            from search in State.XPS_Name
            where search == myUpgrade.whereResearched
            select search;
            return Possibility.Any();
        }

    }
}
