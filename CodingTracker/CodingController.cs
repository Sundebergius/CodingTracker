using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Data.Sqlite;

internal class CodingController
{
    string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

    internal List<Coding> Get()
    {
        List<Coding> tableData = new List<Coding>();
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();
                tableCmd.CommandText = "SELECT * FROM coding";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tableData.Add(
                                new Coding
                                {
                                    Id = reader.GetInt32(0),
                                    Date = reader.GetString(1),
                                    Duration = reader.GetString(2)
                                });
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n\nNo rows found.\n\n");
                    }
                }
            }
            Console.WriteLine("\n\n");
        }

        TableVisualisation.ShowTable(tableData);

        return tableData;
    }

    internal Coding GetById(int id)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            using (var tableCmd = connection.CreateCommand())
            {
                connection.Open();

                tableCmd.CommandText = $"SELECT * FROM coding Where Id = '{id}'";

                using (var reader = tableCmd.ExecuteReader())
                {
                    Coding coding = new();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        coding.Id = reader.GetInt32(0);
                        coding.Date = reader.GetString(1);
                        coding.Duration = reader.GetString(2);
                    }

                    Console.WriteLine("\n\n");

                    return coding;
                };
            }
        }
    }

    internal void Post(Coding coding)
    {

    }

    internal void Update(Coding coding)
    {
        
    }
}