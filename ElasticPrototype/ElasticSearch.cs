using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Threading.Tasks; 
using Elasticsearch.Net;

namespace ElasticPrototype {
    public class ElasticSearch {
        public static void elastic() {
            //Connect to ElasticSearch
            var client = connect();

            //Index into ElasticSearch
            index(client);

            //Search ElasticSearch indexes
            search(client);
        }

        static ElasticLowLevelClient connect() {
            var uris = new[]
            {
                new Uri("http://localhost:9200"),
                new Uri("http://localhost:9201"),
                new Uri("http://localhost:9202"),
            };

            var connectionPool = new SniffingConnectionPool(uris);
            var settings = new ConnectionConfiguration(connectionPool)
                .RequestTimeout(TimeSpan.FromMinutes(2));
            var client = new ElasticLowLevelClient(settings);

            return client;
        }

        static void index(ElasticLowLevelClient client) {
            var person = new Person
            {
                FirstName = "Monique",
                LastName = "Wink James"
            };

            //Synchronous methods, returns IIndexResponse
            var indexResponse = client.Index<byte[]>("people", "person", "1", person);
            byte[] responseBytes = indexResponse.Body;
            Console.WriteLine("Index response: {0}", indexResponse);

            //Asynchronous method, returns Task<IIndexResponse>
            //var asyncIndexResponse = await client.IndexAsync<string>("people", "person", "1", person);
            //string responseString = asyncIndexResponse.Body;
            //Console.WriteLine("Index response: {0}", responseString);
        }

        static void search(ElasticLowLevelClient client) {
            var request = new {query = new {term = new {Name = "Monique"}}};
            var search = client.Search<string>("people", "person", request);

            var successful = search.Success;
            var responseJson = search.Body;

            Console.WriteLine("Search successful: {0}", successful);
            Console.WriteLine("Search Response: {0}",responseJson);
        }
    }
}