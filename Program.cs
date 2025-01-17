using System.ComponentModel;
using System.Reflection.PortableExecutable;
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

    public void AddEmployee(string EmployeeFirstName, string EmployeeLastName) 
    {
        int AssignNumber = FindNumberOfEmployees() + 1;

        string cmd = $"INSERT INTO Emps(EmployeeID,FirstName,LastName) VALUES(@EmployeeID1,@FirstName,@LastName);";

        this.sqlConnection.Open();
        using (SqlCommand cmdd = new SqlCommand(cmd, this.sqlConnection)) 
        {
            cmdd.Parameters.AddWithValue("@EmployeeID1", AssignNumber);
            cmdd.Parameters.AddWithValue("@FirstName", EmployeeFirstName);
            cmdd.Parameters.AddWithValue("@LastName", EmployeeLastName);
            Console.WriteLine("--------------------------------------");

            int RowsAffected = cmdd.ExecuteNonQuery();
            Console.WriteLine($"{RowsAffected} Rows Affected:");

        }
        this.sqlConnection.Close();
    }
    public void DeleteEmployee() 
    {
       //continue here
    }
    public void UpdateEmployeeInfo() 
    {
        //continue here
    }

    public void PrintTable() 
    {
        using (SqlConnection conn = new SqlConnection(this.ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SELECT EmployeeID,FirstName,LastName FROM Emps", conn))
            {
                conn.Open();
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    Console.WriteLine("EmployeeID|FirstName|LastName");
                    while (read.Read())
                    {
                        Console.WriteLine($"{read["EmployeeID"]}         |{read["FirstName"]}     |{read["LastName"]}");
                    }
                }
            }
            conn.Close();
        }
    }

    public int FindNumberOfEmployees() 
    {
        List<int> NumberOfEmps = new List<int>();

        string comm = "SELECT EmployeeID FROM Emps";
        this.sqlConnection.Open();
        using (SqlCommand cmd = new SqlCommand(comm,this.sqlConnection)) 
        {
            using (SqlDataReader rdr = cmd.ExecuteReader()) 
            {
                while (rdr.Read()) 
                {
                    NumberOfEmps.Add(Convert.ToInt32(rdr["EmployeeID"]));
                }
            }
        }
        this.sqlConnection.Close();

        if (NumberOfEmps.Count == 0) 
        {
            return 0;
        }
        return NumberOfEmps.Last();
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
        NewSql.AddEmployee("John","Doe");
        NewSql.AddEmployee("Jane", "Doe");
        NewSql.AddEmployee("Janet", "Jackson");
        NewSql.PrintTable();

    }
}