using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Xml.Serialization;
using System.IO;

namespace StarcraftDemo4
{
    class Recorder
    {
        static public string consstr = "Data Source=.\\JASONSQLSERVER;Initial Catalog=SC2ResultsGames;Integrated Security=True";
        static public string datFileName = "movelist.dat";

        public static void SendToDB(State myState, out int Latest_GameId)
        {
            //create connection
            System.Data.SqlClient.SqlConnection objCon;
            objCon = new System.Data.SqlClient.SqlConnection(consstr);
            objCon.Open();

            Latest_GameId = (int)GetLatestId(objCon);

            //create command to insert into games
            System.Data.SqlClient.SqlCommand objCmd = SendToGames(objCon, Latest_GameId);

            //create command to insert into results (the 'saved game' gets inserted here)
            System.Data.SqlClient.SqlCommand objCmd_Result = SendToResults(myState, objCon, Latest_GameId);

            //create and commit transaction
            System.Data.SqlClient.SqlTransaction tx;
            tx = null;
            try
            {
                //send to Result table.
                tx = objCon.BeginTransaction();
                objCmd_Result.Transaction = tx;
                objCmd_Result.ExecuteNonQuery();
                if (int.Parse(objCmd_Result.Parameters["@RC"].Value.ToString()) < 0)
                    throw new Exception("not good RC in Result");
                //for each result send string of moves to Games table
                foreach (string move in myState.Moves)
                {
                    objCmd.Parameters["@Move"].Value = move;
                    objCmd.Transaction = tx;
                    objCmd.ExecuteNonQuery();
                    if (int.Parse(objCmd.Parameters["@RC"].Value.ToString()) < 0)
                        throw new Exception("not good RC in Game");
                    //MessageBox.Show("Return Value data: " + objCmd.Parameters["@RC"].Value.ToString());
                }
                tx.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tx.Rollback();
            }
            objCon.Close();
        }

        public static void SerializeMoves(List<Move> MyMoves)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            using (Stream fStream = new FileStream(datFileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, MyMoves);
            }
        }

        public static object DeserializeMoves(string filename)
        {
            BinaryFormatter bformat = new BinaryFormatter();
            using (Stream fStream = File.OpenRead(filename))
            {
                return bformat.Deserialize(fStream);
            }
        }

        public static int GetGameIdForBestGame(Units_Name myUnit)
        {
            if (myUnit != Units_Name.Marine)
                throw new CantBuildException("only works for marines so far");
            //create connection
            System.Data.SqlClient.SqlConnection objCon;
            objCon = new System.Data.SqlClient.SqlConnection(consstr);
            objCon.Open();

            //create command to insert into game
            System.Data.SqlClient.SqlCommand objCmd = GetCommand(objCon, "pFindMarineGameId");

            int GameId = (int)objCmd.ExecuteScalar();
            objCon.Close();
            return GameId;

        }

        public static string GetBinaryFileFromDB(int GameId)
        {
            FileStream fs;
            BinaryWriter bw;
            long retval;
            int bufferSize =7000;                   // Size of the BLOB buffer.
            byte[] outbyte = new byte[bufferSize];  // The BLOB byte[] buffer to be filled by GetBytes.

            System.Data.SqlClient.SqlConnection objCon = new System.Data.SqlClient.SqlConnection(consstr);
            objCon.Open();
            System.Data.SqlClient.SqlCommand ObjCmd;
            ObjCmd = new System.Data.SqlClient.SqlCommand(
                "Select MoveList from Results where GameId=" + GameId, objCon);
            System.Data.SqlClient.SqlDataReader objDR = ObjCmd.ExecuteReader(CommandBehavior.SequentialAccess);
            string filename = GameId + ".dat";
            while (objDR.Read())
            {
                fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);
                bw = new BinaryWriter(fs);
                retval = objDR.GetBytes(0, 0, outbyte, 0, bufferSize);

                bw.Write(outbyte, 0, (int)retval);
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            objDR.Close();
            objCon.Close();
            return filename;
        }

        #region private helper IO/DB functions

        private static System.Data.SqlClient.SqlCommand SendToResults(State myState, System.Data.SqlClient.SqlConnection objCon, int Latest_GameId)
        {
            System.Data.SqlClient.SqlCommand objCmd_Result;
            objCmd_Result = new System.Data.SqlClient.SqlCommand();
            objCmd_Result.Connection = objCon;
            objCmd_Result.CommandType = CommandType.StoredProcedure;
            objCmd_Result.CommandText = "pInsResult";

            #region parameters for Result
            string[] state_args = {"@GameID", "@Minerals", "@Gas","@Marines","@Marauder", "@SCV", "@Time",
                                  "@Refinery", "@CommandCenter", "@Barracks", 
                                  "@TechLab", "@Reactor", "@SupplyDepot"};
            System.Data.SqlClient.SqlParameter[] objResult = new System.Data.SqlClient.SqlParameter[state_args.Length];
            
            for (int j = 0; j < state_args.Length; j++)
            {
                objResult[j] = new System.Data.SqlClient.SqlParameter();
                objResult[j].Direction = ParameterDirection.Input;
                objResult[j].DbType = DbType.Int32;
                objResult[j].ParameterName = state_args[j];
            }
            objResult[0].Value = Latest_GameId;
            objResult[1].Value = myState.minerals;
            objResult[2].Value = myState.gas;
            objResult[3].Value = myState.Total<Marine>();
            objResult[4].Value = myState.Total<Marauder>();
            objResult[5].Value = myState.Total<SCV>();
            objResult[6].Value = myState.totalTime;
            objResult[7].Value = myState.Total<Refinery>();
            objResult[8].Value = myState.Total<CommandCenter>();
            objResult[9].Value = myState.Total<Barracks>();
            objResult[10].Value = myState.Total<TechLab>();
            objResult[11].Value = myState.Total<Reactor>();
            objResult[12].Value = myState.Total<SupplyDepot>();

            for (int j = 0; j < state_args.Length; j++)
            {
                objCmd_Result.Parameters.Add(objResult[j]);
            }

            //insert MoveList parameter
            byte[] binfile = GetFile(datFileName);
            System.Data.SqlClient.SqlParameter objML = new System.Data.SqlClient.SqlParameter();
            objML.Direction = ParameterDirection.Input;
            objML.SqlDbType = SqlDbType.VarBinary;
            objML.ParameterName = "@MoveList";
            objML.Size = binfile.Length;
            objML.Value = binfile;
            objCmd_Result.Parameters.Add(objML);

            System.Data.SqlClient.SqlParameter objRCR = new System.Data.SqlClient.SqlParameter();
            objRCR.Direction = ParameterDirection.ReturnValue;
            objRCR.DbType = DbType.Int32;
            objRCR.ParameterName = "@RC";
            objCmd_Result.Parameters.Add(objRCR);

            #endregion
            return objCmd_Result;
        }

        private static System.Data.SqlClient.SqlCommand SendToGames(System.Data.SqlClient.SqlConnection objCon, int Latest_GameId)
        {

            System.Data.SqlClient.SqlCommand objCmd;
            objCmd = new System.Data.SqlClient.SqlCommand();
            objCmd.Connection = objCon;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "pInsGame";

            #region parameters for Game
            System.Data.SqlClient.SqlParameter objE1 = new System.Data.SqlClient.SqlParameter();
            objE1.Direction = ParameterDirection.Input;
            objE1.DbType = DbType.Int32;
            objE1.ParameterName = "@GameID";
            objE1.Value = Latest_GameId;

            System.Data.SqlClient.SqlParameter objE2 = new System.Data.SqlClient.SqlParameter();
            objE2.Direction = ParameterDirection.Input;
            objE2.DbType = DbType.String;
            objE2.ParameterName = "@Move";
            objE2.Value = "";

            System.Data.SqlClient.SqlParameter objRC = new System.Data.SqlClient.SqlParameter();
            objRC.Direction = ParameterDirection.ReturnValue;
            objRC.DbType = DbType.Int32;
            objRC.ParameterName = "@RC";
            #endregion
            //add to commands
            objCmd.Parameters.Add(objRC);
            objCmd.Parameters.Add(objE1);
            objCmd.Parameters.Add(objE2);
            return objCmd;

        }

        private static System.Data.SqlClient.SqlCommand GetCommand(System.Data.SqlClient.SqlConnection objCon, string commandString)
        {
            System.Data.SqlClient.SqlCommand objCmd;
            objCmd = new System.Data.SqlClient.SqlCommand();
            objCmd.Connection = objCon;
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = commandString;
            return objCmd;
        }

        private static byte[] GetFile(string filePath)
        {
            byte[] binfile;
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(stream);

                binfile = reader.ReadBytes((int)stream.Length);

                reader.Close();
            }
            Console.WriteLine("{0} is the length of the binary file being converted to byte []", binfile.Length);
            return binfile;
        }

        private static Object GetLatestId(System.Data.SqlClient.SqlConnection objCon)
        {
            System.Data.SqlClient.SqlCommand objCmd_R;
            objCmd_R = new System.Data.SqlClient.SqlCommand();
            objCmd_R.Connection = objCon;
            objCmd_R.CommandType = CommandType.Text;
            objCmd_R.CommandText = "select MAX(GameId) from Results";

            Object Latest_GameId = objCmd_R.ExecuteScalar();
            if (Latest_GameId.ToString() == "")//null doesnt work here. there is a difference.
                Latest_GameId = 1;
            else if (Latest_GameId is int)
                Latest_GameId = (int)Latest_GameId + 1;
            return Latest_GameId;
        }
        #endregion

    }
}
