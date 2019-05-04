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
        public List<Product> deSerializeProductsToList(String json)
        {
            List<Product> products = new List<Product>();

            products = (List<Product>) JsonConvert.DeserializeObject<List<Product>>(json);

            return products;
        }
    }
}
