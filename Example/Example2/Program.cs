using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElasticSearchExample;

namespace Example2
{
    class Program
    {
        static void Main(string[] args)
        {
            Elasticsearch objSearch = new Elasticsearch();

            objSearch.AddNewIndex(new City() { ID = 1, Name = "delhi", State = "delhi", Country = "India", Population = "9.879 million" });
            objSearch.AddNewIndex(new City() { ID = 2, Name = "mumbai", State = "Maharashtra", Country = "India", Population = "11.98 million" });

            var result = objSearch.GetResult();
        }
    }
}
