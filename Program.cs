using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.SqlClient;

class SqlConnector
{
    public string ConnectionString { get; set; }

    public string UserName {  get; set; }

    public string Password { get; set; }

    public SqlConnection sqlConnection = new SqlConnection();
    
    public SqlConnector(string ConnectString, string User,string Pass) 
    {
        this.ConnectionString = ConnectString;
        this.UserName = User;
        this.Password = Pass;
    }

    public void RunSqlConnect() 
    {
        Console.WriteLine("Sql connection started!");
        Console.WriteLine("-----------------------");
        
        this.sqlConnection.ConnectionString = this.ConnectionString;
        this.sqlConnection.Open();
        RunCommand("SELECT * FROM Emps");
        this.sqlConnection.Close();

    }

    public void RunCommand(string Command) 
    {
        using (SqlCommand cmd = new SqlCommand(Command, this.sqlConnection)) 
        {
            using (SqlDataReader rdr = cmd.ExecuteReader()) 
            {
                while (rdr.Read()) 
                {
                    Console.WriteLine($"Name: {rdr["FirstName"]}");
                }
            }
        }
    }
}

class Program() 
{
    static void Main()
    {
        SqlConnector NewSql = new SqlConnector("data source=DESKTOP-V002898\\SQLEXPRESS;initial catalog=master;trusted_connection=true;Encrypt=False", "DESKTOP-V002898\\onixt","");

        Console.WriteLine("This program has started!");
        Console.WriteLine("------------------------");
        Console.WriteLine("------------------------");

        NewSql.RunSqlConnect();
    }
}