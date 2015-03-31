using System;
using System.Configuration;
using System.Data.SqlClient;
using TS.Gambling.FaceBook;

/// <summary>
/// Summary description for DataBaseManager
/// </summary>
public class DataBaseManager
{
    public DataBaseManager()
    {
    }

    public class ResultResponse
    {
        public int errorCode;
        public string username;
        public int playerId;
        public double balance;
    }

    public static ResultResponse LoadPlayer(FacebookUser fbUser)
    {
        ResultResponse res = new ResultResponse();
        const string commantText = "EXEC LoadPlayer @UserId, @FirstName, @LastName, @Gender, @EMail, @BirthDate";
        string connectionString = ConfigurationManager.ConnectionStrings["GamblingConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(commantText, connection);
        command.Parameters.Add(new SqlParameter("UserId", fbUser.UserId));
        command.Parameters.Add(new SqlParameter("FirstName", fbUser.FirstName));
        command.Parameters.Add(new SqlParameter("LastName", fbUser.LastName));
        command.Parameters.Add(new SqlParameter("Gender", fbUser.Gender));
        command.Parameters.Add(new SqlParameter("EMail", fbUser.EMail));
        command.Parameters.Add(new SqlParameter("BirthDate", fbUser.BirthDate));

        SqlDataReader reader = null;
        connection.Open();
        try
        {
            reader = command.ExecuteReader();
            if (!reader.Read())
            {
                throw new Exception("Cannot Create Process Request !!");
            }
            res.errorCode = int.Parse(reader["ErrorCode"].ToString());
            res.playerId = int.Parse(reader["PlayerId"].ToString());
            res.username = reader["UserName"].ToString();
            res.balance = double.Parse(reader["Balance"].ToString());
            return res;
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            command.Dispose();
            connection.Close();
        }
    }

    public static void StartGame(int GameId, int FirstPlayerId, int SecondPlayerId, double Amount)
    {
        ResultResponse res = new ResultResponse();
        string commantText = "EXEC BuraStartGame @p_GameId, @p_FirstPlayerId, @p_SecondPlayerId, @p_Amount";
        string connectionString = ConfigurationManager.ConnectionStrings["GamblingConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(commantText, connection);
        command.Parameters.Add(new SqlParameter("p_GameId", GameId));
        command.Parameters.Add(new SqlParameter("p_FirstPlayerId", FirstPlayerId));
        command.Parameters.Add(new SqlParameter("p_SecondPlayerId", SecondPlayerId));
        command.Parameters.Add(new SqlParameter("p_Amount", Amount));

        connection.Open();
        try
        {
            command.ExecuteNonQuery();
        }
        finally
        {
            command.Dispose();
            connection.Close();
        }
    }

    public static void EndGame(int GameId, int winnerPlayerId, double Amount)
    {
        ResultResponse res = new ResultResponse();
        string commantText = "EXEC BuraEndGame @p_GameId, @p_WinnerPlayerId, @p_Amount";
        string connectionString = ConfigurationManager.ConnectionStrings["GamblingConnectionString"].ToString();
        SqlConnection connection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand(commantText, connection);
        command.Parameters.Add(new SqlParameter("p_GameId", GameId));
        command.Parameters.Add(new SqlParameter("p_WinnerPlayerId", winnerPlayerId));
        command.Parameters.Add(new SqlParameter("p_Amount", Amount));

        connection.Open();
        try
        {
            command.ExecuteNonQuery();
        }
        finally
        {
            command.Dispose();
            connection.Close();
        }
    }
}