using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Cafe_QuanAn.ClassEntity
{
    public class Products
    {
        private string name;
        private string price;
        private PictureBox picture;
        private string category;

        public PictureBox Picture { get => picture; set => picture = value; }
        public string Price { get => price; set => price = value; }
        public string Name { get => name; set => name = value; }

        public string Category { get => category; set => category = value; }

        public static JArray FindCate(Dictionary<string, object> dictObj, string category)
        {
            JArray list = new JArray();
            JArray products = (JArray)dictObj["products"];

            foreach (JObject item in products)
            {
               

                    //get list menu
                    //JObject a = (JObject)item.Value;
                    string category_ = item.GetValue("category").ToString();
                    if (category_.Equals(category))
                    {
                    list.Add(item);

                    }
                
            }
            return list;

        }
    }
}
