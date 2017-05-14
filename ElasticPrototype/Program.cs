using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using Npgsql;
using Elasticsearch.Net;

namespace ElasticPrototype {

    internal class Program {

        public static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            // Connect to a PostgreSQL database
            NpgsqlConnection connection = new NpgsqlConnection("Server=127.0.0.1;User Id=postgres; " +
            "Password=pwd;Database=Monique;");
            connection.Open();

            queries(connection);
            elastic();

            connection.Close();
        }

        public static void queries(NpgsqlConnection connection) {
            NpgsqlCommand command = new NpgsqlCommand("SELECT COUNT(*) FROM art", connection);
            Int64 count = (Int64)command.ExecuteScalar();
            Console.Write("{0}\n", count);

            NpgsqlCommand query = new NpgsqlCommand("SELECT * FROM art", connection);
            NpgsqlDataReader dr = query.ExecuteReader();
            while (dr.Read()) {
                Console.Write("{0}\t{1}\t{2}\n", dr[0], dr[1], dr[2]);
            }
        }

            public static void elastic() {
                var settings = new ConnectionConfiguration(new Uri("http://example.com:9200"))
                .RequestTimeout(TimeSpan.FromMinutes(2));
                var client = new ElasticLowLevelClient(settings);

                var requestBody = new { query = new { term = new { Name = "banana" } } };
            //var result = client.Search<string>("fruit", requestBody);
        }
    }
}