using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using Npgsql;

namespace ElasticPrototype {
    public class PostgresDatabase {
        public static void postgres() {
            // Connect to a PostgreSQL database
            NpgsqlConnection connection = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres; " +
                                                               "Password=pwd;Database=Monique;");
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM art", connection);
            Int64 count = (Int64)command.ExecuteScalar();
            Console.WriteLine("Number of entries: {0}\n", count);

            NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM art", connection);
            NpgsqlDataReader dr = query.ExecuteReader();
            Console.Write("Entries: \n");
            while (dr.Read()) {
                Console.Write("{0}\t{1}\t{2}\n", dr[0], dr[1], dr[2]);
            }
            Console.WriteLine();
            connection.Close();
        }
    }
}