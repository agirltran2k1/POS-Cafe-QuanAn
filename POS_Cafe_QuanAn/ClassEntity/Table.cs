using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Cafe_QuanAn.ClassEntity
{
    public class Table
    {
        private string id;
        private string name;
        private string id_region;

        public string Id_region { get => id_region; set => id_region = value; }
        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }

        /// <summary>
        /// List all tables find id tab
        /// </summary>
        /// <param name="dictObj"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JArray FindCate(Dictionary<string, object> dictObj,string id)
        {
         
            dynamic tables = dictObj["tables"];
 
            foreach (var item in tables)
            {
                //get list table
                JObject a = (JObject)item.Value;
                string id_ = a.GetValue("id").ToString();
                if (id_.Equals(id))
                {
                    return (JArray)a.GetValue("child");
                }

                
            }
            return null;

        }
    }
}
