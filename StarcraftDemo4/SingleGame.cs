using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using StarcraftDemo4.Services;


namespace StarcraftDemo4
{

    public partial class SingleGame
    {
        public static string str;
        public List<Move> MovesPlayed = new List<Move>();
        private GameRecorder gameRecorder;
        public static void SendString(string str)
        {
            if (State.verbose)
                Console.WriteLine(str);
        }

        #region games
        public static void PlayGame(Player Jason)
        {

            string input = "";
            while ((input != null) && (input != "q"))
            {
                Console.Write("move:");
                input = Console.ReadLine();
                int result;
                if (Int32.TryParse(input, out result))
                {
                    try
                    {
                        Jason.Time_Step(result);
                        Jason.Display_Stats();
                        input = "initial";
                    }
                    catch (CantBuildException e)
                    { SendString(e.Message); }
                }
                switch (input)
                {
                    case ("bb"):
                        try
                        {
                            Jason.Build(new Barracks());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("bc"):
                        try
                        {
                            Jason.Build(new CommandCenter());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("1s"):
                        try
                        {
                            Jason.Build(new SCV());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("be"):
                        try
                        {
                            Jason.Build(new EngineeringBay());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("br"):
                        try
                        {
                            Jason.Build(new Refinery());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("bs"):
                        try
                        {
                            Jason.Build(new SupplyDepot());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("vf"):
                        try
                        {
                            Jason.Build(new Factory());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("vs"):
                        try
                        {
                            Jason.Build(new Starport());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("2a"):
                        try
                        {
                            Jason.Build(new Marine());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("2d"):
                        try
                        {
                            Jason.Build(new Marauder());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("2x"):
                        try
                        {
                            Jason.Build(new TechLab(Structure_Name.Barracks));
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("2c"):
                        try
                        {
                            Jason.Build(new Reactor(Structure_Name.Barracks));
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("2\tx"):
                        try
                        {
                            Jason.Build(new TechLab(Structure_Name.Factory));
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("1b"):
                        try
                        {
                            Jason.Build(new OrbitalCommand(Structure_Name.Command_Center));
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("1e"):
                        try
                        {
                            Jason.Build(new Mule());
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("2\tc"):
                        try
                        {
                            Jason.Build(new Reactor(Structure_Name.Factory));
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("move gas"):
                        try
                        {
                            Jason.PutOnGas();
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;
                    case ("Lift"):
                        try
                        {
                            Jason.LiftoffFromAddOn(Structure_Name.Barracks, Structure_Name.Tech_Lab);
                            Jason.Display_Stats();
                        }
                        catch (CantBuildException e)
                        { SendString(e.Message); }
                        break;

                    case ("initial"):
                        break;
                    default:
                        SendString("no move recognized");
                        //input = "na";
                        break;
                }
            }
        }

        public State ReplayGame(AutoPlayer person, List<Move> Moves)
        {
            gameRecorder = new GameRecorder();
            gameRecorder.StartNewGame();
            
            foreach (Move mv in Moves)
            {
                MakeMove(person, mv);
                MovesPlayed.Add(mv);
                
                // Record the move to database
                gameRecorder.RecordGameStep(mv, person.myState);
            }
            
            // Finish recording the game
            gameRecorder.FinishGame(person.myState);
            gameRecorder.Dispose();
            
            return person.myState;
        }

        public State PlayAutoGame(AutoPlayer person)
        {
            gameRecorder = new GameRecorder();
            gameRecorder.StartNewGame();
            
            //MovesPlayed = new List<Move>();
            Random random = new Random();
            while (person.myState.totalTime < AutoPlayer.THRESH)
            {
                //Choose thisMove
                Move[] Array_of_Moves = person.PossibleMoves();
                int randomNumber = random.Next(0, Array_of_Moves.Length);
                Move thisMove = Array_of_Moves[randomNumber];
                MovesPlayed.Add(thisMove);
                Array_of_Moves = null;
                if (person.UseEnergyUpOnMule())
                {
                    thisMove = new Move(new Mule(), "EnergyUnit");
                }
                SendString(" my move is " + thisMove.str);
                // Make thismove
                MakeMove(person, thisMove);
                
                // Record the move to database
                gameRecorder.RecordGameStep(thisMove, person.myState);
            }
            
            // Finish recording the game
            gameRecorder.FinishGame(person.myState);
            gameRecorder.Dispose();
            
            return person.myState;
        }

        private static void MakeMove(AutoPlayer person, Move thisMove)
        {
            switch (thisMove.str)
            {
                case ("Addon"):
                    person.ABuild((Addon)thisMove.obj);
                    break;
                case ("Unit"):
                    person.ABuild((Unit)thisMove.obj);
                    break;
                case ("Structure"):
                    person.ABuild((Structure)thisMove.obj);
                    break;
                case ("Upgrade"):
                    person.ABuild((Upgrade)thisMove.obj);
                    break;
                case ("EnergyUnit"):
                    person.ABuild((EnergyUnit)thisMove.obj);
                    break;
                case ("LiftoffFrom"):
                    person.ALiftoffFromAddOn(((Structure)thisMove.obj).name, ((Structure)thisMove.obj2).name);
                    break;
                case ("Landon"):
                    person.ALandonAddOn(((Structure)thisMove.obj).name, ((Structure)thisMove.obj2).name);
                    break;
                case ("MoveGas"):
                    person.APutOnGas();
                    break;
                case ("MoveMineral"):
                    person.APutOnMin();
                    break;
                default:
                    break;
            }
            str = String.Format(" done with move. Minerals: {0} Time: {1} Supply: {2}/{3}", person.myState.minerals, person.myState.totalTime, person.myState.unit_Count, person.myState.unit_Cap);
            SendString(str);
        }
        #endregion
    }
}
