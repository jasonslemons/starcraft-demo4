using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace StarcraftDemo4
{
    [Serializable]
    public struct Move
    {
        public Move(object _obj, string _str)
        {
            obj = _obj;
            str = _str;
            obj2 = null;
        }
        public object obj, obj2;
        public string str;
    }
    public class AutoPlayer : Player
    {
        public const int THRESH = 300;//time limit
        public int RefineryCounter = 0;
        public int LiftLandCounter = 0;
        public int GoToGasMinCounter = 0;
        public int GoToGasMinCounter_Max()
        {
            return myState.currentStructures.Count(me => me is Refinery && me.production_Time_Left == 0);
        }
        public const int LiftLandCounter_Max = 3;
        public const int RefineryCounter_Max = 2;


        public bool UseEnergyUpOnMule()
        {
            var Possible =
                from pcc in myState.currentStructures
                where pcc is CommandCenter
                where ((CommandCenter)pcc).myAddon != null
                where ((CommandCenter)pcc).hasOrbital()
                where ((OrbitalCommand)((CommandCenter)pcc).myAddon).Energy >= 50
                select pcc;

            if (Possible.Any())
                return true;
            else
                return false;
        }

        #region AMoves
        public void ABuild(Structure myStructure)
        {
            if (myStructure is Refinery)
                RefineryCounter++;
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    Build(myStructure);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(1);
                    done = false;
                }
            }
        }

        public void ABuild(Upgrade myUpgrade)
        {
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    Build(myUpgrade);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(1);
                    done = false;
                }
            }
        }

        public void ABuild(Unit myUnit)
        {
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    Build(myUnit);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(1);
                    done = false;
                }
            }
        }

        public void ABuild(Addon myAddon)
        {
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    Build(myAddon);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(1);
                    done = false;
                }
            }
        }

        public void ABuild(EnergyUnit myEnergyUnit)
        {
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    Build(myEnergyUnit);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(1);
                    done = false;
                }
            }
        }

        public void AHot_Swap(Structure_Name xPSBuilding, Structure_Name itsAddon,
            Structure_Name xPSBuilding2, Structure_Name itsAddon2)
        {
            bool done = false;
            while (done == false && myState.totalTime <= 170)
            {
                try
                {
                    Hot_Swap(xPSBuilding, itsAddon, xPSBuilding2, itsAddon2);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(myState.totalTime);
                    done = true;
                    throw new CantBuildException("cant hotswap these. its not going to change. im giving up");

                }
            }
        }

        public void ALandonAddOn(Structure_Name xPSBuilding, Structure_Name getAddon)
        {
            bool done = false;
            LiftLandCounter++;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    LandonAddOn(xPSBuilding, getAddon);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(myState.totalTime);
                    done = true;
                    throw new CantBuildException("cant Land this on this. its not going to change. im giving up");
                }
            }
        }

        public void ALiftoffFromAddOn(Structure_Name xPSBuilding, Structure_Name itsAddon)
        {
            bool done = false;
            LiftLandCounter++;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    LiftoffFromAddOn(xPSBuilding, itsAddon);
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(myState.totalTime);
                    done = true;
                    throw new CantBuildException("cant Land this on this. its not going to change. im giving up");
                }
            }
        }

        public void APutOnGas()
        {
            GoToGasMinCounter++;
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    PutOnGas();
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(myState.totalTime);
                    done = true;
                    throw new CantBuildException("cant put on gas. its not going to change. im giving up");
                }
            }
        }

        public void APutOnMin()
        {
            GoToGasMinCounter++;
            bool done = false;
            while (done == false && myState.totalTime <= THRESH)
            {
                try
                {
                    PutOnMinerals();
                    done = true;
                }
                catch (CantBuildException)
                {
                    Time_Step(myState.totalTime);
                    done = true;
                    throw new CantBuildException("cant put on minerals. its not going to change. im giving up");
                }
            }
        }

        #endregion

        //
        //hacked together. want to redo this.
        //this collects things you can build or do based on the state.
        //

        #region PossibleMove and Helpers
        public Move[] PossibleMoves()
        {
            Move[] Moves = new Move[0];
            int j = 0;
            foreach (object obj in myState.Mostbuild)
            {

                if (obj is EnergyUnit)
                {
                    ifEnergyUnit(obj, ref j, ref Moves);
                    continue;
                }
                else if (obj is Addon)
                {
                    ifAddon(obj, ref j, ref Moves);
                    continue;
                }
                else if (obj is Structure)
                {
                    ifStructure(obj, ref j, ref Moves);
                    continue;
                }
                else if (obj is Unit)
                {
                    ifUnit(obj, ref j, ref Moves);
                    continue;
                }
                else if (obj is Upgrade)
                {
                    ifUpgrade(obj, ref j, ref Moves);
                    continue;
                }
            }
            //Lifoff and Landon
            LiftoffLandon(ref j, ref Moves);
            //move to gas
            MoveOnGasMinerals(ref j, ref Moves);

            return Moves;
        }

        //this adds the move to Move[]; helper function of PossibleMoves().
        private void updateMoves(object obj, ref int j, ref Move[] Moves, string typ, string msg)
        {
            //handles all adds but liftoff and landon
            Array.Resize(ref Moves, Moves.Length + 1);
            Moves[j] = new Move(obj, typ);
            j++;
        }

        //helper funtions of PossibleMoves()
        private void MoveOnGasMinerals(ref int j, ref Move[] Moves)
        {
            if ((myState.SCVTotal() != 0) &&
                GoToGasMinCounter < GoToGasMinCounter_Max()  &&
                myState.currentStructures.Any(me => (me is Refinery) && me.production_Time_Left==0))
            {
                //can put on gas
                updateMoves(new Object(), ref j, ref Moves, "MoveGas", " can put on gas 'move gas'");
            }
            //just for now, once you put a guy on gas hes stuck.
            //if ((myState.SCVTotalGas() != 0) &&
            //    myState.namesCurrentStructures.Contains(Structure_Name.Refinery) &&
            //    GoToGasMinCounter < GoToGasMinCounter_Max)
            //{
            //    //can put on minerals
            //    updateMoves(new Object(), ref j, ref Moves, "MoveMineral", " can put on minerals 'move minerals'");
            //}
        }

        private void LiftoffLandon(ref int j, ref Move[] Moves)
        {
            var XPSaddon =
                from xps in myState.currentStructures
                where xps is ExtendableProducingStructure
                where ((ExtendableProducingStructure)xps).myAddon != null
                where ((ExtendableProducingStructure)xps).myAddon.production_Time_Left == 0
                where !((ExtendableProducingStructure)xps).unitQueFull
                where !((ExtendableProducingStructure)xps).upgradeQueFull
                select xps;

            var IdleAddon =
                from add in myState.currentStructures
                where add is Addon
                where add.production_Time_Left == 0
                select add;

            if (XPSaddon.Any() && (LiftLandCounter < LiftLandCounter_Max))
            {
                ExtendableProducingStructure objx = (ExtendableProducingStructure)XPSaddon.First();
                Addon objaddon = ((ExtendableProducingStructure)XPSaddon.First()).myAddon;
                str = String.Format("Lift {0} from {1}", objx.name, objaddon.name);
                SendString(str);
                //can move LiftoffFromAddon(XPSaddon[i], XPSaddon[i].myaddon)
                Array.Resize(ref Moves, Moves.Length + 1);
                Moves[j] = new Move(objx, "LiftoffFrom");
                Moves[j].obj2 = objaddon;
                j++;
                if (IdleAddon.Any())
                {
                    Addon idleaddon = (Addon)IdleAddon.First();
                    Array.Resize(ref Moves, Moves.Length + 1);
                    Moves[j] = new Move(objx, "Landon");
                    Moves[j].obj2 = idleaddon;
                    str = String.Format("Land {0} on {1}", XPSaddon.First().name, IdleAddon.First().name);
                    SendString(str);
                    j++;
                }
            }
        }

        private void ifUpgrade(object obj, ref int j, ref Move[] Moves)
        {
            Upgrade myUpgrade = (Upgrade)obj;

            if (myState.PmeetReqs(myUpgrade.structure_Reqs) &&
                myState.isGettingGasAnIssue<Upgrade>(myUpgrade) &&
                myState.meetReqs(myUpgrade.upgrade_Reqs))
            {
                if (DoINeedTech(myUpgrade))
                {
                    var PotentialProducingStructures =
                    from ppsn in myState.currentStructures
                    where ppsn is ExtendableProducingStructure
                    where ((ExtendableProducingStructure)ppsn).myAddon != null
                    where ((ExtendableProducingStructure)ppsn).myAddon.name == Structure_Name.Tech_Lab
                    where ppsn.name == myUpgrade.whereResearched
                    select ppsn;
                    //there is a struct available            
                    if (PotentialProducingStructures.Any())
                    {//can upgrade
                        updateMoves(obj, ref j, ref Moves, "Upgrade", "can move" + myUpgrade.name);
                    }
                }
                else
                {
                    var PotentialProducingStructures =
                    from ppsn in myState.currentStructures
                    where ppsn is ProducingStructure
                    where ppsn.name == myUpgrade.whereResearched
                    select ppsn;
                    //there is a struct available
                    if (PotentialProducingStructures.Any())
                    {//can upgrade
                        updateMoves(obj, ref j, ref Moves, "Upgrade", "can move" + myUpgrade.name);
                    }
                }
            }
        }

        protected bool LookForTrainingSiteWithTechAuto(Unit myUnit)
        {
            var PotentialProducingStructures =
                from ppsn in myState.currentStructures
                where ppsn is ExtendableProducingStructure
                where ppsn.name == myUnit.whereTrained
                where ((ExtendableProducingStructure)ppsn).myAddon != null
                where ((ExtendableProducingStructure)ppsn).myAddon.name == Structure_Name.Tech_Lab
                where !((ProducingStructure)ppsn).unitQueFull
                select ppsn;

            return PotentialProducingStructures.Any();
        }

        protected bool LookForTrainingSiteAuto(Unit myUnit)
        {
            if (myUnit.needTech)
            {
                return LookForTrainingSiteWithTechAuto(myUnit);
            }
            else
            {
                var PotentialProducingStructures =
                        from ppsn in myState.currentStructures
                        where ppsn is ProducingStructure
                        where ppsn.name == myUnit.whereTrained
                        select ppsn;

                return PotentialProducingStructures.Any();
            }
        }

        private void ifUnit(object obj, ref int j, ref Move[] Moves)
        {
            Unit myUnit = (Unit)obj;
            if (myState.PmeetReqs(myUnit.structure_Reqs) &&
                myState.isGettingGasAnIssue<Unit>(myUnit) &&
                myState.canTheyEat(myUnit) || !myState.canTheyEat(myUnit) &&
                myState.currentStructures.Any(me => me is SupplyDepot && me.production_Time_Left!=0))
            {
                if (LookForTrainingSiteAuto(myUnit))
                {//can build
                    updateMoves(obj, ref j, ref Moves, "Unit", "can move" + myUnit.name);
                }
            }
        }

        private void ifStructure(object obj, ref int j, ref Move[] Moves)
        {
            if ((RefineryCounter >= RefineryCounter_Max) && (obj is Refinery))
                return;

            int margin =2*(4*myState.Total < ExtendableProducingStructure >() +myState.Total<CommandCenter>());
            if (obj is SupplyDepot && (myState.unit_Count + margin < myState.unit_Cap))
                return;

            if (myState.PmeetReqs(((Structure)obj).structure_Reqs))
            {
                if (myState.namesCurrentUnits.Contains(Units_Name.SCV) &&
                    myState.isGettingGasAnIssue<Structure>((Structure)obj))
                {//can build
                    updateMoves(obj, ref j, ref Moves, "Structure", "can move" + ((Structure)obj).name);
                }
            }
        }

        private void ifAddon(object obj, ref int j, ref Move[] Moves)
        {
            Addon myAddon = (Addon)obj;

            if (myState.namesCurrentStructures.Contains(myAddon.where_Added) &&
                myState.PmeetReqs(myAddon.structure_Reqs) && //donet have to be done, just producing
                myState.isGettingGasAnIssue<Structure>(myAddon))
            {
                //need to be done CC without an addon already.
                IAddon site;
                bool sitefound;

                LookForAddonSite(out sitefound, out site, myAddon);
                if (sitefound)
                {//can build
                    updateMoves(obj, ref j, ref Moves, "Addon", "can move" + myAddon.where_Added + "to" + myAddon.name);
                }
            }
        }

        private void ifEnergyUnit(object obj, ref int j, ref Move[] Moves)
        {
            CommandCenter site;
            bool sitefound;
            LookForCastSite(out sitefound, out site, (EnergyUnit)obj);
            if (sitefound)
            {//can build eunit
                updateMoves(obj, ref j, ref Moves, "EnergyUnit", "can move" + ((EnergyUnit)obj).name);
            }
        }

        #endregion
    }
}