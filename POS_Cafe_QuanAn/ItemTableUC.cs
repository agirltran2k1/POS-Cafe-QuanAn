using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_Cafe_QuanAn.ClassEntity;
namespace POS_Cafe_QuanAn
{
    public partial class ItemTableUC : UserControl
    {
        
        public ItemTableUC()
        {
            InitializeComponent();
            
        }

        private string id;
        public string Id 
        {
            get { return id; }
            set { id = value; lblTable.Text = value; }
        }

        private string name;
        public string NameTable
        {
            get { return name; }
            set { name = value;}
        }

        private string id_region;
        public string Id_region
        {
            get { return id_region; }
            set { id_region = value; }
        }

        private void ItemTable_Load(object sender1, EventArgs e1)
        {
            foreach (Control c in this.Controls)
            {
                c.Click += (sender, e) => { this.OnClick(e); };
                c.MouseUp += (sender, e) => { this.OnMouseUp(e); };
                c.MouseDown += (sender, e) => { this.OnMouseDown(e); };
                c.MouseMove += (sender, e) => { this.OnMouseMove(e); };
            }
        }
    }
}
