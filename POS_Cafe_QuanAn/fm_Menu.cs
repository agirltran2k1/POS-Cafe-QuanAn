using Newtonsoft.Json.Linq;
using POS_Cafe_QuanAn.ClassEntity;
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
    public partial class fm_Menu : Form
    {
        public JArray ReturnValue;
        Dictionary<string, object> dictObjmenu;
        private Dictionary<string, JArray> CategoryMenu;
        protected List<ItemMenuUC> ListMenu = new List<ItemMenuUC>();
        protected List<ButtonCategoryMenuUC> ListCategoryMenu = new List<ButtonCategoryMenuUC>();
        private dynamic config;

        public fm_Menu()
        {
            InitializeComponent();
            this.config = Global.loadJson("config.json");
        }
        private void fm_Menu_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            TopMost = false;


            
            //Load menu
            this.dictObjmenu = Global.loadJson("data/outlet.json").ToObject<Dictionary<string, object>>();

            //get all JSON
            this.CategoryMenu = this.initMenu(this.dictObjmenu);

            this.bindMenuList();
        }
        private Dictionary<string, JArray> initMenu(Dictionary<string, object> dictObj)
        {
            Dictionary<string, JArray> list = new Dictionary<string, JArray>();

            JArray products = (JArray)dictObj["products"];

       
            foreach (JObject item in products)
            {
           
                string cate_menu = item.GetValue("category").ToString();

                if(!list.ContainsKey(cate_menu))
                {
                    list[cate_menu] = new JArray();
                }

                list[cate_menu].Add(item);


            }
            return list;
        }
        private void bindMenuList()
        {
         

            List<ButtonCategoryMenuUC> cate_menus = new List<ButtonCategoryMenuUC>();

            
            foreach (KeyValuePair<string, JArray> item in CategoryMenu)
            {
                //get list table
                //JObject a = (JObject)item.Value;

                
                    tblLayoutMenu.Controls.Clear();
                    string cate_menu = item.Key;

                    

                    ButtonCategoryMenuUC cateMenu = new ButtonCategoryMenuUC();
                    cateMenu.lblCategoryMenu.Text = cate_menu;
                    cateMenu.Tag = cate_menu;
                    cateMenu.Click += CateMenu_Click;
                    tblLayoutCategoryMenu.Controls.Add(cateMenu);
                    cate_menus.Add(cateMenu);
                    ListCategoryMenu.Add(cateMenu);

                    foreach (JObject item1 in item.Value)
                    {
                        
                        ItemMenuUC itemMenu = new ItemMenuUC();
                        itemMenu.Tag = item1["id"];
                        itemMenu.lblName.Text = item1["title"].ToString();
                        itemMenu.lblPrice.Text = item1["price"].ToString();
                        itemMenu.Click += menu_item_Click;
                        tblLayoutMenu.Controls.Add(itemMenu);

                        this.ListMenu.Add(itemMenu);
                    }
                
            }
          
        }

        private void menu_item_Click(object sender, EventArgs e)
        {
            //
        }

        private void CateMenu_Click(object sender, EventArgs e)
        {
            ButtonCategoryMenuUC a = (ButtonCategoryMenuUC)sender;
            String category = a.Tag.ToString();
            a.BackColor = Helper.Convert(this.config.btn_main);
            a.lblCategoryMenu.ForeColor = Helper.Convert(this.config.btn_main_text);

            foreach (ButtonCategoryMenuUC i in this.ListCategoryMenu)
            {
                if (!i.Tag.ToString().Equals(category))
                {
                    i.BackColor = Color.FromArgb(105, 105, 105);
                    i.lblCategoryMenu.ForeColor = Helper.Convert(this.config.btn_main_text);
                }
            }

            JArray list = this.CategoryMenu.ContainsKey(category) ? this.CategoryMenu[category]: null;

            if (list != null)
            {
                tblLayoutMenu.Controls.Clear();

                foreach (JObject item1 in list)
                {
                    ItemMenuUC itemMenu = new ItemMenuUC();
                    itemMenu.Tag = item1["id"];
                    itemMenu.lblName.Text = item1["title"].ToString();
                    itemMenu.lblPrice.Text = item1["price"].ToString();
                    itemMenu.Click += menu_item_Click;
                    tblLayoutMenu.Controls.Add(itemMenu);
                    //add table to list global
                    //this.ListTable.Add(itemTable);
                }
            }
        }

        private void ItemMenu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void fm_Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.DialogResult = DialogResult.No;
            e.Cancel = false;
            this.Hide();
        }

        private void lblThemVaoGioHang_Click(object sender, EventArgs e)
        {

        }

        private void txtNhapMonAnCanTim_Enter(object sender, EventArgs e)
        {
            if (txtNhapMonAnCanTim.Text == "Nhập món ăn cần tìm...")
            {
                txtNhapMonAnCanTim.Text = "";
                txtNhapMonAnCanTim.ForeColor = Color.Black;
            }
        }

        private void txtNhapMonAnCanTim_Leave(object sender, EventArgs e)
        {
            if (txtNhapMonAnCanTim.Text == "")
            {
                txtNhapMonAnCanTim.Text = "Nhập món ăn cần tìm...";
                txtNhapMonAnCanTim.ForeColor = Color.Silver;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
            this.ReturnValue = null;
            tblLayoutCategoryMenu.Controls.Clear();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
