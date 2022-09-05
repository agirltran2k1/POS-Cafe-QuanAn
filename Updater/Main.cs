using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Updater
{
    public partial class Main : Form
    {
        string root;
        dynamic config;
        public void loadJson()
        {
            using (StreamReader r = new StreamReader(AppDomain.CurrentDomain.BaseDirectory+"/checker.json"))
            {
                string json = r.ReadToEnd();
                this.config = JsonConvert.DeserializeObject(json);
            }
        }

        public int printerInstalled(string n)
        {
            var printerQuery = new ManagementObjectSearcher("SELECT * from Win32_Printer");
            foreach (var printer in printerQuery.Get())
            {
                var name = printer.GetPropertyValue("Name");
                var status = printer.GetPropertyValue("Status");
                var isDefault = printer.GetPropertyValue("Default");
                var isNetworkPrinter = printer.GetPropertyValue("Network");

                Console.WriteLine("{0} (Status: {1}, Default: {2}, Network: {3}",
                            name, status, isDefault, isNetworkPrinter);

                if (n == name)
                {
                    return 1;
                }
            }
            return 0;
        }
        public Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12;

            InitializeComponent();
            this.root = AppDomain.CurrentDomain.BaseDirectory + "/download/";
            ; System.IO.Directory.CreateDirectory(this.root);
            ;
            this.loadJson();


            int mm50 = printerInstalled("XP50");
            int mm80 = printerInstalled("XP80");

            if (mm50 == 0 && mm80 == 0)
            {
                    DialogResult confirmResult = MessageBox.Show("Bạn có muốn cài đặt driver máy in không?", "Cài đặt driver", MessageBoxButtons.YesNo);

                    if (confirmResult == DialogResult.Yes)
                    {
                       System.Diagnostics.Process.Start((string)this.config.driver);
                    } 
            }


            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {


                startDownload(args[1]);
            }
            else
            {
                this.init();
               
            }
            string version_text = this.config.version;
            this.version_txt.Text = "Phiên bản " + version_txt;
        }

        private async void init()
        {
            using (var httpClient = new HttpClient())
            {
                string url = this.config.url;
                var json = await httpClient.GetStringAsync(url);

                // Now parse with JSON.Net
                dynamic a = JsonConvert.DeserializeObject(json);
                if (a.version > this.config.version)
                {
                    this.config.version = a.version;
                    string u = a.download;
                    startDownload(u);
                }
                else
                {
                    this.open();
                }
                
            }
        }
 

        private bool isCompleted = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void startDownload(string url)
        {
            string file = this.root + "backup.zip";
            try
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);

                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadFileAsync(new Uri(url), file);
            } catch (Exception e)
            {
                label2.Text = e.Message;
            }
            
        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            label2.Text = "Đang tải " + e.BytesReceived + " bytes từ " + e.TotalBytesToReceive + " bytes";
            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string file = this.root + "backup.zip";
                this.isCompleted = true;
                if (File.Exists(file))
                {
                    label2.Text = "Tải về thành công";
              
                    label2.Text = "Đang giải nén.";
                    //check file
                    string extractionPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LinkPos");
                    string extractionPathTemp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LinkPosTemp");
                    if (!System.IO.Directory.Exists(extractionPath))
                    {
                        Directory.CreateDirectory(extractionPath);
                    }
                    if (System.IO.Directory.Exists(extractionPathTemp))
                    {
                        Directory.Delete(extractionPathTemp, true);
                        Directory.CreateDirectory(extractionPathTemp);
                    }
                    else
                    {
                        Directory.CreateDirectory(extractionPathTemp);
                    }
                    ZipFile.ExtractToDirectory(file, extractionPathTemp);
                    Directory.Delete(extractionPath, true);
                    System.IO.Directory.Move(extractionPathTemp, extractionPath);
                    label2.Text = ("Cập nhật thành công");
                    this.btn_cancel.Text = "Đóng";

                    //now save
                    this.save();

                    this.open();
                }
                else
                {
                    label2.Text = "Error updating";
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           

        }

        private void save()
        {
            this.config.modified_date = DateTime.Now;
            string json = JsonConvert.SerializeObject(this.config);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "/checker.json", json);
        }

        private void Main_Leave(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.isCompleted==false)
            {
                e.Cancel = true;

                btn_cancel_Click(sender, e);
            }
           
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            string file = this.root + "backup.zip";
            try
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            catch (IOException)
            { 
            }
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (this.isCompleted == false)
            {
                DialogResult dlgresult = MessageBox.Show("Bạn có muốn đóng ứng dụng ngay lập tức?",
                             "Cảnh báo!",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Information);
                if (dlgresult == DialogResult.Yes)
                {

                    this.open();
                }
            }
            else
            {
                this.open();
            }
        }

        private void open()
        {
            string file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LinkPos", "LinkPos.exe");
            if (File.Exists(file))
            {
                System.Diagnostics.Process.Start(file);
            }
             
            Process currentProcess = Process.GetCurrentProcess();

            currentProcess.Kill();
        }
    }   

}