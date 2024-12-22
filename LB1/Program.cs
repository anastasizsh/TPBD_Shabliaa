using MySql.Data.MySqlClient;
using System;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "server=netflix-manya4560-9acc.j.aivencloud.com;port=21425;database=defaultdb;user=avnadmin;password=AVNS_H9XkQzsdnJUK3P_lk38;sslmode=Required";

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection successful!");

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("\n--- Menu ---");
                    Console.WriteLine("1. Display all tables");
                    Console.WriteLine("2. Display data from a specific table");
                    Console.WriteLine("3. Insert values into tables");
                    Console.WriteLine("4. Perform a JOIN query");
                    Console.WriteLine("5. Perform a filtering query");
                    Console.WriteLine("6. Perform an aggregate query");
                    Console.WriteLine("7. Exit");

                    Console.Write("\nSelect an option (1-7): ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            SelectAllTables(connection);
                            break;
                        case "2":
                            SelectFromTable(connection);
                            break;
                        case "3":
                            InsertValuesToTables(connection);
                            break;
                        case "4":
                            JoinQuery(connection);
                            break;
                        case "5":
                            FilterQuery(connection);
                            break;
                        case "6":
                            AggregateQuery(connection);
                            break;
                        case "7":
                            exit = true;
                            Console.WriteLine("Exiting program.");
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please select a number between 1 and 7.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static void SelectAllTables(MySqlConnection connection)
    {
        string[] queries = {
            "SELECT * FROM Categories;",
            "SELECT * FROM Products;",
            "SELECT * FROM Customers;",
            "SELECT * FROM Orders;",
            "SELECT * FROM OrderDetails;"
        };

        try
        {
            foreach (string query in queries)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                Console.WriteLine($"\n--- {cmd.CommandText} ---");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader.GetName(i) + "\t");
                }
                Console.WriteLine();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + "\t");
                    }
                    Console.WriteLine();
                }
                reader.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void SelectFromTable(MySqlConnection connection)
    {
        Console.WriteLine("Select a table to display data from (Categories, Products, Customers, Orders, OrderDetails): ");
        string table = Console.ReadLine();
        string query = $"SELECT * FROM {table};";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine($"\n--- {table} ---");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void InsertValuesToTables(MySqlConnection connection)
    {
        try
        {
            // Insert into Categories table
            Console.WriteLine("Inserting into Categories table...");
            string categoriesQuery = @"
                INSERT INTO Categories (Name) VALUES 
                ('Processors'), 
                ('Motherboards'), 
                ('RAM');";
            MySqlCommand categoriesCmd = new MySqlCommand(categoriesQuery, connection);
            int categoriesRowsAffected = categoriesCmd.ExecuteNonQuery();
            Console.WriteLine($"{categoriesRowsAffected} row(s) inserted into Categories.");

            // Insert into Products table
            Console.WriteLine("Inserting into Products table...");
            string productsQuery = @"
                INSERT INTO Products (Name, Price, CategoryId) VALUES 
                ('Intel Core i5', 200, 1), 
                ('MSI B450', 150, 2), 
                ('Kingston 16GB', 75, 3);";
            MySqlCommand productsCmd = new MySqlCommand(productsQuery, connection);
            int productsRowsAffected = productsCmd.ExecuteNonQuery();
            Console.WriteLine($"{productsRowsAffected} row(s) inserted into Products.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void JoinQuery(MySqlConnection connection)
    {
        string query = @"
            SELECT Orders.OrderId, Customers.FullName, Products.Name AS ProductName, OrderDetails.Quantity
            FROM Orders
            INNER JOIN Customers ON Orders.CustomerId = Customers.CustomerId
            INNER JOIN OrderDetails ON Orders.OrderId = OrderDetails.OrderId
            INNER JOIN Products ON OrderDetails.ProductId = Products.ProductId;";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- JOIN Query ---");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void FilterQuery(MySqlConnection connection)
    {
        string query = "SELECT * FROM Products WHERE Price > 100;";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n--- Filter Query ---");
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.Write(reader.GetName(i) + "\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i] + "\t");
                }
                Console.WriteLine();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void AggregateQuery(MySqlConnection connection)
    {
        string query = @"
            SELECT 
                COUNT(*) AS ProductCount, 
                AVG(Price) AS AveragePrice 
            FROM Products;";

        try
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Console.WriteLine($"\nTotal products: {reader["ProductCount"]}");
                    Console.WriteLine($"Average price: {reader["AveragePrice"]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
