using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POS_Cafe_QuanAn.ClassEntity;
namespace POS_Cafe_QuanAn
{
    public partial class fm_Main : Form
    {
        public JObject outlet;
        
        public fm_Main()
        {
            InitializeComponent();


            this.config = Global.loadJson("config.json");
        }

        Dictionary<string, object> dictObjmenu;

        protected List<ItemTableUC> ListTable = new List<ItemTableUC>();

        protected List<ButtonCategoryUC> ListTabTable;
        private dynamic config;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = false;
            
            txtNgayBan.CustomFormat = "dd/MM/yyyy HH:mm:ss";
           

            //Load tab Ban
           this.dictObjmenu = Global.loadJson("data/outlet.json").ToObject<Dictionary<string, object>>();

            //get all JSON
           this.ListTabTable = this.bindTableList(this.dictObjmenu);


            //Load tab menu
            this.loadMenu(this.dictObjmenu);
           
           


        }

        private void loadMenu(Dictionary<string, object> dictObj)
        {
            //get list product
            dynamic products = dictObj["products"];
            //set layout for menu 
            tblLayoutMenu.ColumnCount = 3;
            tblLayoutMenu.AutoSize = true;
            ItemMenuUC[] itemMenus = new ItemMenuUC[products.Count];
            for (int i = 0; i < products.Count; i++)
            {
                Products products1 = new Products();
                itemMenus[i] = new ItemMenuUC();
                products1 = products[i].ToObject<Products>();
                products1.Name = products[i].title;
                itemMenus[i].Nameproduct = products1.Name;
                itemMenus[i].Price = products1.Price;
                itemMenus[i].Picture = products1.Picture;
                tblLayoutMenu.Controls.Add(itemMenus[i]);
            }

        }

        private List<ButtonCategoryUC> bindTableList(Dictionary<string, object> dictObj)
        {
            dynamic tables = dictObj["tables"];

            this.tblTab.AutoSize = true;
            this.tblTab.AutoScroll = true;

            List<ButtonCategoryUC> tabs = new List<ButtonCategoryUC>();


            //set layout for tables
            this.tblLayoutPhong.ColumnCount = 4;         
            this.tblLayoutPhong.AutoSize = true;
            foreach (var item in tables)
            {
                //get list table
                JObject a = (JObject)item.Value;
                string tab = a.GetValue("name").ToString();

                ButtonCategoryUC tabcate = new ButtonCategoryUC();
                tabcate.lbl_text.Text = tab;
                tabcate.Tag = a.GetValue("id").ToString();
                tabcate.Click += Tabcate_Click;
                tabcate.BackColor = Color.FromArgb(105, 105, 105);
                tabcate.lbl_text.ForeColor = Helper.Convert(this.config.btn_main_text);
                tblTab.Controls.Add(tabcate);
                tabs.Add(tabcate);
                /////


                JArray list = (JArray)a.GetValue("child");

                //add to table layout

                foreach (JObject item1 in list)
                {
                    ItemTableUC itemTable = new ItemTableUC();
                    
                    
                    itemTable.Tag = item1["id"];
                    itemTable.Id = item1["name"].ToString();
                    itemTable.Click += table_item_Click;
                    itemTable.DoubleClick += ItemTable_DoubleClick;
                    tblLayoutPhong.Controls.Add(itemTable);
                    //add table to list global
                    this.ListTable.Add(itemTable);
                }


            }
            return tabs;
        }

        //hien menu
        private void ItemTable_DoubleClick(object sender, EventArgs e)
        {
            ItemTableUC a = (ItemTableUC)sender;
            if (a.Tag != null && orders.ContainsKey(a.Tag.ToString()))
            {
                String id = a.Tag.ToString();
                fm_Menu.Text = a.Id + " - THỰC ĐƠN ";
                DialogResult result = fm_Menu.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Object v = fm_Menu.ReturnValue;            //values preserved after close
                    this.lblBan.Text = a.Id;
                }

            }
        }

        public fm_Menu fm_Menu = new fm_Menu();
        Dictionary<string, object> orders = new Dictionary<string, object>();

        //item table click
        private void table_item_Click(object sender, EventArgs e)
        {
            ItemTableUC a = (ItemTableUC)sender;
            if (a.Tag != null && !orders.ContainsKey(a.Tag.ToString()))
            {
                String id = a.Tag.ToString();
                //fm_Menu.Text = a.Id + " - THỰC ĐƠN ";
                fm_Menu.lblGoiMonTai.Text = "GỌI MÓN TẠI BÀN " + a.Id;
                fm_Menu.ReturnValue = null;
               DialogResult result =  fm_Menu.ShowDialog();
                if (result == DialogResult.OK)
                {
                    JArray v = fm_Menu.ReturnValue;            //values preserved after close
                    if (v != null)
                    {
                        a.BackColor = Helper.Convert(this.config.btn_main);
                        a.lblTable.BackColor = Helper.Convert(this.config.btn_main);
                        a.lblTable.ForeColor = Helper.Convert(this.config.btn_main_text);
                        Order order = new Order();
                        order.BindProduct(v);
                        orders.Add(a.Tag.ToString(), order);
                        this.lblBan.Text = a.Id;
                    }                  
                }

            }
        }

        //tab table control click
        private void Tabcate_Click(object sender, EventArgs e)
        {
            
            ButtonCategoryUC a = (ButtonCategoryUC)sender;
            String id = a.Tag.ToString();
            a.BackColor = Helper.Convert(this.config.btn_main);
            a.lbl_text.ForeColor = Helper.Convert(this.config.btn_main_text);
            foreach (ButtonCategoryUC i in this.ListTabTable)
            {
                if (!i.Tag.ToString().Equals(id))
                {
                    i.BackColor = Color.FromArgb(105, 105, 105);
                    i.lbl_text.ForeColor = Helper.Convert(this.config.btn_main_text);
                }
            }
           
            JArray list = Table.FindCate(this.dictObjmenu, id);

            if (list != null)
            {
                tblLayoutPhong.Controls.Clear();
                
                foreach (JObject item1 in list)
                {
                    ItemTableUC itemTable = new ItemTableUC();


                    itemTable.Tag = item1["id"];
                    itemTable.Id = item1["name"].ToString();
                    itemTable.Click += table_item_Click;
                    tblLayoutPhong.Controls.Add(itemTable);
                    //add table to list global
                    //this.ListTable.Add(itemTable);
                }
            }
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

       

        private void txtTimKhachHang_Enter(object sender, EventArgs e)
        {
            if (txtTimKhachHang.Text == "Tìm khách hàng...")
            {
                txtTimKhachHang.Text = "";
                txtTimKhachHang.ForeColor = Color.Black;
            }
        }

        private void txtTimKhachHang_Leave(object sender, EventArgs e)
        {
            if (txtTimKhachHang.Text == "")
            {
                txtTimKhachHang.Text = "Tìm khách hàng...";
                txtTimKhachHang.ForeColor = Color.Silver;
            }
        }

        private void txtTuDongTaoMa_Enter(object sender, EventArgs e)
        {
            if (txtTuDongTaoMa.Text == "Tự động tạo mã (Đơn hàng)")
            {
                txtTuDongTaoMa.Text = "";
                txtTuDongTaoMa.ForeColor = Color.Black;
            }
        }

        private void txtTuDongTaoMa_Leave(object sender, EventArgs e)
        {
            if (txtTuDongTaoMa.Text == "")
            {
                txtTuDongTaoMa.Text = "Tự động tạo mã (Đơn hàng)";
                txtTuDongTaoMa.ForeColor = Color.Silver;
            }
        }

        private void txtGhiChu_Enter(object sender, EventArgs e)
        {
            if (txtGhiChu.Text == "Ghi chú")
            {
                txtGhiChu.Text = "";
                txtGhiChu.ForeColor = Color.Black;
            }
        }

        private void txtGhiChu_Leave(object sender, EventArgs e)
        {
            if (txtGhiChu.Text == "")
            {
                txtGhiChu.Text = "Ghi chú";
                txtGhiChu.ForeColor = Color.Silver;
            }
        }

        private void lblDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblGoiMangVe_Click(object sender, EventArgs e)
        {
            fm_Menu.ReturnValue = null;
            DialogResult result = fm_Menu.ShowDialog();
        }
    }
}
