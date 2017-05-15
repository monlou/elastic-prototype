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

            //Index into ElasticSearch
            var person = new Person
            {
                FirstName = "Monique",
                LastName = "Wink James1"
            };
            //Synchronous methods, returns IIndexResponse
            var indexResponse = client.Index<byte[]>("person", "1", person);
            byte[] responseBytes = indexResponse.Body;
            Console.WriteLine("Index response: {0}", indexResponse);
            //Asynchronous method, returns Task<IIndexResponse>
            //var asyncIndexResponse = await client.IndexAsync<string>("people", "person", "1", person);
            //string responseString = asyncIndexResponse.Body;
            //Console.WriteLine("Index response: {0}", responseString);

            //Search ElasticSearch indexes
            var request = new {query = new {match = new {field = "firstName", query = "Martijn"}}};
            var search = client.Search<string>("person");
            var successful = search.Success;
            var responseJson = search.Body;

            Console.WriteLine("Search successful: {0}", successful);
            Console.WriteLine("Search Response: {0}",responseJson);
        }
    }
}