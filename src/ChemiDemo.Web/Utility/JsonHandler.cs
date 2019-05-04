using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChemiDemo.Web.Models;
using Newtonsoft.Json;

namespace ChemiDemo.Web.Utility
{
    public class JsonHandler
    {
        public static List<Product.Row> deSerializeProductsToList(String json)
        {
            Console.WriteLine("deserializer got this json ---------------------------- {0}", json);

            List<Product.Row> products = new List<Product.Row>();

            products = (List<Product.Row>) JsonConvert.DeserializeObject<List<Product.Row>>(json);

            Console.WriteLine("deserializer firstelement name ---------------------------- {0}", products[0].Name);

            return products;
        }
    }
}
