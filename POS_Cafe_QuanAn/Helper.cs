using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Cafe_QuanAn
{
    public class Helper
    {
        public static Color Convert (object obj)
        {
            return System.Drawing.ColorTranslator.FromHtml(obj.ToString());
        }
    }
}
