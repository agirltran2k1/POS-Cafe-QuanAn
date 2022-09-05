using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Cafe_QuanAn
{
    public partial class ItemMenuUC : UserControl
    {
        public ItemMenuUC()
        {
            InitializeComponent();
        }
        private string name;
        public string Nameproduct
        {
            get { return name; }
            set { name = value; lblName.Text = value; }
        }

        private string price;
        public string Price
        {
            get { return price; }
            set { price = value; lblPrice.Text = value; }
        }

        private PictureBox picture;
        public PictureBox Picture
        {
            get { return picture; }
            set { picture = value; picProduct = value; }
        }

        private void ItemMenuUC_Load(object sender1, EventArgs e1)
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
