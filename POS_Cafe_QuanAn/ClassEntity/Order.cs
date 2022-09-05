using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Cafe_QuanAn
{
    class Order
    {

        Dictionary<string, JObject> products = new Dictionary<string, JObject>();
        public void BindProductAdd(JObject item, decimal quatity)
        {
           
                if(!products.ContainsKey(item["id"].ToString()))
                {
                    item["quality"] = quatity;
                    item["subtotal"] = item["price"].ToString();
                    products[item["id"].ToString()] = item;
                }
                else
                {
                JObject item1 = products[item["id"].ToString()];
                item["quality"] = ((decimal)item["quality"])+quatity;
                item["subtotal"] = (decimal)item["quality"]* (decimal)item["price"];
            }
            
        }

        public void BindProductReduce(JObject item, decimal quatity)
        {

            if (!products.ContainsKey(item["id"].ToString()))
            {
                item["quality"] = quatity;
                item["subtotal"] = item["price"].ToString();
                products[item["id"].ToString()] = item;
            }
            else
            {
                JObject item1 = products[item["id"].ToString()];
                item["quality"] = ((decimal)item["quality"]) - quatity;
                item["subtotal"] = (decimal)item["quality"] * (decimal)item["price"];
            }

        }

        public void BindProduct(JArray v)
        {
           foreach(JObject item in v)
            {
                this.BindProductAdd(item, (decimal)item["quality"]);
            }
        }
    }
}
