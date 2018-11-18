using System;
using System.Collections.Generic;
using System.Linq;
using Nest;

namespace Example
{
    public class QuotationLine
    {
        public int Id { get; set; }
        public string quotationLineText { get; set; }
        public double unitaryLinePrice { get; set; }
    }

    class Program
    {
        private static Uri EsNode;
        private static ConnectionSettings EsConfig;
        private static ElasticClient EsClient;
        static void Main(string[] args)
        {
            EsNode = new Uri("http://localhost:9200/");
            EsConfig = new ConnectionSettings(EsNode);
            EsClient = new ElasticClient(EsConfig);

            //var settings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 1 };

            //var indexConfig = new IndexState
            //{
            //    Settings = settings
            //};

            //if (!EsClient.IndexExists("QuotationLine").Exists)
            //{
            //    EsClient.CreateIndex("QuotationLine", c => c
            //    .InitializeUsing(indexConfig)
            //     .Mappings(m => m.Map<QuotationLine>(mp => mp.AutoMap())));
            //}

            CreateIndex();
            InsertData();

            Console.ReadLine();            
        }

        static void CreateIndex()
        {
            var settings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 2 };

            var indexConfig = new IndexState
            {
                Settings = settings
            };

            if (!EsClient.IndexExists("QuotationLine").Exists)
            {
                EsClient.CreateIndex("QuotationLine", c => c
                .InitializeUsing(indexConfig)
                .Mappings(m => m.Map<QuotationLine>(mp => mp.AutoMap())));
            }
        }

        static void InsertData()
        {
            List<QuotationLine> testList = new List<QuotationLine>();
            for(int i=0; i<100; i++)
            {
                testList.Add(new QuotationLine { Id = i, quotationLineText = "CI" + i, unitaryLinePrice = 1.3 * (i + 1) });
            }

            foreach (var obj in testList.Select((value, counter) => new { counter, value }))
            {
                EsClient.Index(obj.value, i => i
                    .Index("employee")
                    .Type("myEmployee")
                    .Id(obj.counter)
                    //.Refresh()
                    );
            }

        }
    }
}