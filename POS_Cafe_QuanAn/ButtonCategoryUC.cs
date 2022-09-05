using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Cafe_QuanAn
{
    public partial class ButtonCategoryUC : UserControl
    {
        public ButtonCategoryUC()
        {
            InitializeComponent();
        }

        private void ButtonCategoryUC_Load(object sender1, EventArgs e1)
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
