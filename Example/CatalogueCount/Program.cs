using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace CatalogueCount
{
    public class salescatalogues
    {
        string SqlId { get; set; }
        string IncId { get; set; }
        string Code { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(uri);
            ElasticClient client = new ElasticClient(settings);
            settings.DefaultIndex("salescatalogues");

            if (client.IndexExists("salescatalogues").Exists)
            {
                var response = client.Search<salescatalogues>();
                var result = response.Documents.ToList();

 

            }

            Console.Read();
        }
    }
}
