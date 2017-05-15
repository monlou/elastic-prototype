using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using Npgsql;
using Elasticsearch.Net;

using static ElasticPrototype.PostgresDatabase;
using static ElasticPrototype.ElasticSearch;

namespace ElasticPrototype {

    internal class Program {

        public static void Main(string[] args) {
            Console.WriteLine("Hello World! \n");

            //Connect to PostgreSQL
            Console.WriteLine("Connecting to the PostgreSQL Database...\n");
            postgres();

            //Connect to ElasticSearch
            Console.WriteLine("Connecting to the ElasticSeach Database...\n");
            elastic();
        }
    }
}