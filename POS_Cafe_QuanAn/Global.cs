using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Cafe_QuanAn
{
    public class Global
    {
        public static dynamic loadJson(string file)
        {
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/" + file))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject(json);
            }
        }
    }
}
