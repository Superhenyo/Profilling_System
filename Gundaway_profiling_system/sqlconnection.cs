using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

public class sqlconnection
{
    private static string connString;
    private static MySqlConnection dbConnection = new MySqlConnection();

    public DataTable ItemListTB = new DataTable();
    public DataTable ItemListDT = new DataTable();

    public sqlconnection()
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory + "connstring.txt";
        if (!File.Exists(filePath))
        {
            using (StreamWriter sw = File.CreateText(filePath))
            {
                sw.WriteLine("Server=localhost;Port=3306;User=root;Password=1234;Database=gundawaysystem");
            }
        }

        connString = File.ReadAllText(filePath);
        ConnectToDatabase();
    }

    public static DataTable ExecuteQuery(string query)
    {
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(query, dbConnection);
        DataTable dataTable = new DataTable();
        dataAdapter.Fill(dataTable);
        return dataTable;
    }
    public static void ConnectToDatabase()
    {
        if (dbConnection.State == ConnectionState.Closed)
        {
            dbConnection.ConnectionString = connString;
            try
            {
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                else
                {
                    Console.WriteLine("Error!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static void ExecuteCommand(string query)
    {
        MySqlCommand sqlCommand = new MySqlCommand();
        sqlCommand.CommandText = query;
        sqlCommand.Connection = dbConnection;
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.ExecuteNonQuery();
    }

    public static void ExecutePhotoCommand(string query, object photo)
    {
        MySqlCommand sqlCommand = new MySqlCommand();
        sqlCommand.CommandText = query;
        sqlCommand.Connection = dbConnection;
        sqlCommand.Parameters.AddWithValue("@photo", photo);
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.ExecuteNonQuery();
    }
}





