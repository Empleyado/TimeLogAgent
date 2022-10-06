using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using AutoUpdaterDotNET;
using Newtonsoft.Json;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Timelog.Agent.ViewModels;
using System.Net;
using Microsoft.Win32;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Timelog.Agent2;
using Timelog.Agent;
using System.Reflection;
using System.Diagnostics;
using System.Deployment.Application;

namespace Timelog.Agent
{
    public partial class AgentUI : Form
    {
        //private int counter = 5;
        public string strFilePath,strPass;
        public bool isRunning = false;
        public bool internetconnection,isConnected;
        public int delay;
        public string IP, url;
        string strProcessedPath;
        string strUnProcessedPath;
        string strErrorsPath;
        string networkstatus;
        DateTime time = DateTime.Now;
        System.Windows.Forms.Timer tickTimeclock = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer autRef = new System.Windows.Forms.Timer();

        public string curdirectory;
        public AgentUI()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;

        }
        private void btnMinimized_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            //this.FormBorderStyle = FormBorderStyle.None;
            TimeLog.Visible = true;
            this.Hide();
        }



        private void btnBrowse_Click(object sender, EventArgs e)
        {
            PasswordForm passForm = new PasswordForm();
            passForm.strPassword = strPass;
            passForm.strPath = strFilePath;
            passForm.Show();
            TimeLog.Visible = false;
            this.Hide();
           
            //FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
            //DialogResult result = openFileDialog.ShowDialog();
            //if (result == DialogResult.OK)
            //{
            //    string file = openFileDialog.SelectedPath.ToString();
            //    txtPath.Text = file;
            //}
        }

        private void btnPlayStop_MouseHover(object sender, EventArgs e)
        {
            btnPlayStop.BackColor = Color.LightGray;
        }

        private void btnPlayStop_MouseLeave(object sender, EventArgs e)
        {
            btnPlayStop.BackColor = Color.WhiteSmoke;
        }

        private void btnBrowse_MouseHover(object sender, EventArgs e)
        {
            btnBrowse.BackColor = Color.LightGray;
        }

        private void btnBrowse_MouseLeave(object sender, EventArgs e)
        {
            btnBrowse.BackColor = Color.WhiteSmoke;
        }

        private void AgentUI_Load(object sender, EventArgs e)
        {
      
            CreateApexFolder();

            Process aProcess = Process.GetCurrentProcess();
            string aProcName = aProcess.ProcessName;

            if (Process.GetProcessesByName(aProcName).Length > 1)
            {
                MessageBox.Show("Application is currently running.");
                Application.ExitThread();
            }
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            reg.SetValue("Empleyado", Application.ExecutablePath.ToString());
            reg.Close();
            


            var bmp = new Bitmap(System.Agent.Properties.Resources.play1);
            btnPlayStop.Image = bmp;
            int x, y;
            x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 5;
            y = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 25;
            this.Location = new Point(x, y);
            txtPath.Text = strFilePath;
            getFormLoad();
            
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;  // Turn on WS_EX_TOOLWINDOW
                return cp;
            }
        }

        private void AgentUI_SizeChanged(object sender, EventArgs e)
        {
            TimeLog.Visible = true;
        }

        private void btnMinimized_MouseHover(object sender, EventArgs e)
        {
            btnMinimized.BackColor = Color.LightGray;
        }
        private void btnMinimized_MouseLeave(object sender, EventArgs e)
        {
            btnMinimized.BackColor = Color.WhiteSmoke;
        }
        private void getFormLoad()
        {
            try
            {
                List<string> filePath = new List<string>();
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                curdirectory = Directory.GetCurrentDirectory();
                //MessageBox.Show(curdirectory);
                if (File.Exists(curdirectory + @"\Settings.txt"))
                {
                    StreamReader sr = new StreamReader(curdirectory + @"\Settings.txt");
                    string line = "*";
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] components = line.Split("*".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                        filePath.Add(components[0]);
                        filePath.Add(components[1]);
                    }
                    if (filePath.Count() != 0)
                    {
                        strFilePath = filePath[0];
                        strPass = filePath[1];
                        strProcessedPath = @"C:\empleyado\Logs\Processed";
                        strUnProcessedPath = strFilePath;
                        strErrorsPath = @"C:\empleyado\Logs\Errors";
                        txtPath.Text = strFilePath;
                        sr.Close();
                        if (Directory.Exists(strProcessedPath) && Directory.Exists(strUnProcessedPath) && Directory.Exists(strErrorsPath))
                        {
                            folderCounter();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        sr.Close();
                        MessageBox.Show("Please put file path");
                    }
                }
                else
                {
                    string directory = curdirectory + @"\Settings.txt";
                    StreamWriter file = new StreamWriter(directory);
                    file.Write(@"C:\apex\datas*Password_711*");
                    txtPath.Text = @"C:\apex\datas";
                    file.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void btnPlayStop_Click(object sender, EventArgs e)
        {
            if (isRunning == false)
            {
                networkstatus = "with connection";
                autoRefresh();
                btnBrowse.Enabled = false;
                //savePath();
                if (Directory.Exists(strProcessedPath) && Directory.Exists(strUnProcessedPath) && Directory.Exists(strErrorsPath))
                {
                    folderCounter();
                }
                cpbStatus.Style = ProgressBarStyle.Marquee;
                cpbStatus.Text = "Running";
                cpbStatus.ProgressColor = Color.FromArgb(93, 157, 203);
                cpbStatus.Value = 25;
                var bmp = new Bitmap(System.Agent.Properties.Resources.stop_button);
                btnPlayStop.Image = bmp;
                isRunning = true;
            }
            else
            {
                autoRefresh();
                btnBrowse.Enabled = true;
                cpbStatus.Style = ProgressBarStyle.Blocks;
                cpbStatus.Text = "Stopped";
                cpbStatus.ProgressColor = Color.FromArgb(204, 0, 0);
                isRunning = false;
                var bmp = new Bitmap(System.Agent.Properties.Resources.play1);
                btnPlayStop.Image = bmp;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            if (this.WindowState == FormWindowState.Normal)
            {
                this.Show();
                TimeLog.Visible = false;
                
            }
        }
        private void autoRefresh()
        {
            //tickTimeclock.Interval = 100;
            tickTimeclock.Enabled = true;
            autRef.Interval = (5 * 1000);
            autRef.Tick += new EventHandler(this.auto_refresh);
            autRef.Start();
        }
        private void auto_refresh(object sender, EventArgs e)
        {
            //counter--;
            if (isRunning == true && networkstatus == "with connection")
            {
                CheckForInternetConnection();
                PasswordForm passForm = new PasswordForm();
                
                //if (passForm.strNewPath !="")
                //{
                //    txtPath.Text = passForm.strNewPath;
                //}
                //if (isConnected == true)
                //{
                //    getFormLoad();
                //    tickTimeclock.Enabled = true;
                //    cpbStatus.Text = "Running";

                //    if (Directory.Exists(strProcessedPath) && Directory.Exists(strUnProcessedPath) && Directory.Exists(strErrorsPath))
                //    {
                //        folderCounter();
                //    }
                //}
            }
            else if (isRunning == true && networkstatus == "no connection")
            {
                CheckForInternetConnection();
                cpbStatus.Text = "No Connection";
                networkstatus = "with connection";
                consoleApplication();
                //if(counter <= 0)
                //{
                //    consoleApplication();
                //    counter = 5;
                //} 
                //tickTimeclock.Enabled = false;
            }

            else
            {
                CheckForInternetConnection();
                networkstatus = "with connection";
                //tickTimeclock.Enabled = false;
                consoleApplication();
            }
        }
        private void savePath()
        {
            string curdirectory = Directory.GetCurrentDirectory();
            string directory = curdirectory + "\\Settings.txt";
            StreamWriter file = new StreamWriter(directory);
            file.Write(txtPath.Text + "*");
            file.Close();
        }

        private void folderCounter()
        {
            var cntProcessedPath = Directory.GetFiles(strProcessedPath, "*CLK*.*", SearchOption.AllDirectories).Length;
            var cntUnProcessedPath = Directory.GetFiles(strUnProcessedPath, "*CLK*.*", SearchOption.AllDirectories).Length;
            var cntErrorsPath = Directory.GetFiles(strErrorsPath, "*", SearchOption.AllDirectories).Length;
            lblProcessed.Text = cntProcessedPath.ToString();
            lblUnprocessed.Text = cntUnProcessedPath.ToString();
            lblError.Text = cntErrorsPath.ToString();
        }
        //private void checkUpdates()
        //{
        //    AutoUpdater.ShowSkipButton = false;
        //    AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
        //    AutoUpdater.ParseUpdateInfoEvent += AutoUpdaterOnParseUpdateInfoEvent;
        //    AutoUpdater.Start("https://rbsoft.org/updates/AutoUpdaterTest.json");
        //}
        //private void AutoUpdater_ApplicationExitEvent()
        //{
        //    Text = @"Closing application...";
        //    Thread.Sleep(5000);
        //    Application.Exit();
        //}
        //private void AutoUpdaterOnParseUpdateInfoEvent(ParseUpdateInfoEventArgs args)
        //{
        //    dynamic json = JsonConvert.DeserializeObject(args.RemoteData);
        //    args.UpdateInfo = new UpdateInfoEventArgs
        //    {
        //        CurrentVersion = json.version,
        //        ChangelogURL = json.changelog,
        //        Mandatory = json.mandatory,
        //        DownloadURL = json.url
        //    };
        //}
        //private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        //{
        //    if (args != null)
        //    {
        //        if (args.IsUpdateAvailable)
        //        {
        //            DialogResult dialogResult;
        //            if (args.Mandatory)
        //            {
        //                dialogResult =
        //                    MessageBox.Show(
        //                        $@"There is new version {args.CurrentVersion} available. You are using version {
        //                                args.InstalledVersion
        //                            }. This is required update. Press Ok to begin updating the application.",
        //                        @"Update Available",
        //                        MessageBoxButtons.OK,
        //                        MessageBoxIcon.Information);
        //            }
        //            else
        //            {
        //                dialogResult =
        //                    MessageBox.Show(
        //                        $@"There is new version {args.CurrentVersion} available. You are using version {
        //                                args.InstalledVersion
        //                            }. Do you want to update the application now?", @"Update Available",
        //                        MessageBoxButtons.YesNo,
        //                        MessageBoxIcon.Information);
        //            }


        //            if (dialogResult.Equals(DialogResult.Yes) || dialogResult.Equals(DialogResult.OK))
        //            {
        //                try
        //                {
        //                    //You can use Download Update dialog used by AutoUpdater.NET to download the update.

        //                    if (AutoUpdater.DownloadUpdate())
        //                    {
        //                        Application.Exit();
        //                    }
        //                }
        //                catch (Exception exception)
        //                {
        //                    MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButtons.OK,
        //                        MessageBoxIcon.Error);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show(@"There is no update available. Please try again later.", @"Update Unavailable",
        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(
        //            @"There is a problem reaching update server. Please check your internet connection and try again later.",
        //            @"Update Check Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void cpbStatus_Click(object sender, EventArgs e)
        {

        }
        private void consoleApplication()
        {
            try
            {
                Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                string curdirectory = Directory.GetCurrentDirectory();
                var builder = new ConfigurationBuilder()
               .SetBasePath(curdirectory)
               .AddJsonFile("appsettings.json");
                //MessageBox.Show(curdirectory);


                IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();


                if (!Directory.Exists(@"C:\empleyado\Logs\Unprocessed"))
                {

                    Directory.CreateDirectory(@"C:\empleyado\Logs\Unprocessed");
                }

                if (!Directory.Exists(@"C:\empleyado\Logs\Processed"))
                {
                    Directory.CreateDirectory(@"C:\empleyado\Logs\Processed");
                }

                if (!Directory.Exists(@"C:\empleyado\Logs\Errors"))
                {
                    Directory.CreateDirectory(@"C:\empleyado\Logs\Errors");
                }
                if (!Directory.Exists(@"C:\empleyado\Logs\Duplicates"))
                {
                    Directory.CreateDirectory(@"C:\empleyado\Logs\Duplicates");
                }
                while (true)
                {
                    var unprocessed = new DirectoryInfo(strFilePath);
                    // try CLK ONLY
                    var unprocessedFiles = unprocessed.GetFiles("*CLK*.*", SearchOption.AllDirectories);

                    var processed = new DirectoryInfo(@"c:\empleyado\Logs\Processed");
                    var processedFiles = processed.GetFiles();
                    var FileComparer = new FileCompare();

                    var commonFiles = unprocessedFiles.Intersect(processedFiles, FileComparer);
                    foreach (var file in commonFiles)
                    {
                        var today = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                        today = today.Replace(":", "").Replace(":", "");
                        today = today.Replace("/", "").Replace("/", "");
                        today = today.Replace(" ", "").Replace(" ", "");
                        File.Move(file.FullName, @"c:\empleyado\Logs\Duplicates\" + file.Name + today);
                    }

                    var uniqueFiles = unprocessedFiles.Except(processedFiles, FileComparer);

                    var logs = new List<Log>();
                    if (uniqueFiles.Count() <= 0)
                    {
                        cpbStatus.Text = "Waiting";
                        break;
                    }
                    else

                        foreach (var file in uniqueFiles)
                        {
                            try
                            {
                                var AddTenantAndDevice = new List<ViewRawLogs>();
                                var logList = new List<LogList>();
                                //cpbStatus.Text = "New files found..";
                                //cpbStatus.Text = "Reading files found..";
                                var contents = File.ReadAllLines(file.FullName);
                                foreach (var line in contents)
                                {
                                    var cleanLine = line.Replace(" ", "").Replace(" ", "");
                                    cleanLine = line.Replace(")", "").Replace(" ", "");
                                    cleanLine = line.Replace("(", "").Replace(" ", "");
                                    cleanLine = line.Replace("-", "").Replace(" ", "");
                                    cleanLine = line.Replace("|", "").Replace(" ", "");

                                    if (cleanLine.Length != 33)
                                    {
                                    }
                                    else
                                    {

                                        var year = Int32.Parse(cleanLine.Substring(0, 4));
                                        var month = Int32.Parse(cleanLine.Substring(4, 2));
                                        var day = Int32.Parse(cleanLine.Substring(6, 2));
                                        var hour = Int32.Parse(cleanLine.Substring(19, 2));
                                        var minute = Int32.Parse(cleanLine.Substring(21, 2));
                                        var second = Int32.Parse(cleanLine.Substring(23, 2));

                                        var AddTenantAndDevice2 = new ViewRawLogs()
                                        {
                                            TenantId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                                            //TenantId = Guid.Parse(config["AppSettings:TenantId"]),
                                            DeviceCode = cleanLine.Substring(8, 4)
                                        };
                                        AddTenantAndDevice.Add(AddTenantAndDevice2);

                                        //var test = new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.FromHours(8));

                                        var logList2 = new LogList()
                                        {
                                            EnrollNumber = cleanLine.Substring(12, 7),
                                            LogTime = new DateTimeOffset(year, month, day, hour, minute, second, TimeSpan.FromHours(8))
                                        };

                                        logList.Add(logList2);

                                    }
                                }

                                var postData = new ViewRawLogs
                                {
                                    TenantId = AddTenantAndDevice.Select(x => x.TenantId).FirstOrDefault(),
                                    DeviceCode = AddTenantAndDevice.Select(x => x.DeviceCode).FirstOrDefault(),
                                    LogList = logList
                                };

                                //TODO: Call Api to submit logs
                                //#region Send Logs to Api
                                var json = JsonConvert.SerializeObject(postData);
                                var wc = new WebClient();
                                wc.Headers["Content-Type"] = "application/json";
                                string ClientBase = url + "//" + config["AppSettings:ClientBase"];
                                var responseData = wc.UploadString(ClientBase, "POST", json);
                                var today = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                                today = today.Replace(":", "").Replace(":", "");
                                today = today.Replace("/", "").Replace("/", "");
                                today = today.Replace(" ", "").Replace(" ", "");

                                File.Move(file.FullName, @"c:\empleyado\Logs\Processed\" + file.Name + today);
                                //#endregion
                            }
                            catch (FormatException ex)
                            {

                                if (ex.Message != null)
                                {
                                    File.Move(file.FullName, @"c:\empleyado\Logs\Errors\" + file.Name);
                                }
                            }
                        }

                }
            }
            catch (WebException ex)
            {

                if (ex.Message != null)
                    //== "Unable to connect to the remote server" || ex.Message == "The remote name could not be resolved: 'dev-processrawlogs.empleyado.com'"
                    
                {
                    CheckForInternetConnection();
                    networkstatus = "no connection";
                    autoRefresh();
                    
                }
            }


        }
        private class FileCompare : IEqualityComparer<FileInfo>
        {
            public bool Equals(FileInfo f1, FileInfo f2)
            {
                return f1.Name == f2.Name &&
                       f1.Length == f2.Length;
            }

            // Return a hash that reflects the comparison criteria. According to the   
            // rules for IEqualityComparer<T>, if Equals is true, then the hash codes must  
            // also be equal. Because equality as defined here is a simple value equality, not  
            // reference identity, it is possible that two or more objects will produce the same  
            // hash code.  
            public int GetHashCode(FileInfo fi)
            {
                var s = $"{fi.Name}{fi.Length}";
                return s.GetHashCode();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.ShowInTaskbar = false;
        }


        private void CreateApexFolder()
        {

            if (!Directory.Exists(@"C:\apex\datas"))
            {
                Directory.CreateDirectory(@"C:\apex\datas");

            }

            if (!Directory.Exists(@"C:\apex\datas\IPSettings.txt"))
                File.WriteAllText(Path.Combine(@"C:\apex\datas", "IPSettings.txt"), "processrawlogs.empleyado.com");


        }
        public DataTable ConvertToDataTableDevices(string filePath, int numberOfColumns)
        {

            DataTable tbl = new DataTable();

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            string[] lines = System.IO.File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                var cols = line.Split('|');

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < numberOfColumns; cIndex++)
                {
                    dr[cIndex] = cols[cIndex];
                }

                tbl.Rows.Add(dr);
            }

            return tbl;
        }
        public void CheckForInternetConnection()
        {
          

            DataTable getIp = new DataTable();
            string domain = curdirectory;
            string getData;
            getData = @"C:\apex\datas\IPSettings.txt";

            getIp = ConvertToDataTableDevices(getData, 1);


            foreach (DataRow row in getIp.Rows)
            {
                url = row[0].ToString();
               
            }

            if (delay <= 0)
            {
                try
                {
                                    
                    Ping p = new Ping();
                    PingReply r;
                    string s;
                    s = url;
                    r = p.Send(s);

                    if (r.Status == IPStatus.Success)
                    {
                        //MessageBox.Show("connection established");
                        isConnected = true;
                        if (isConnected == true)
                        {
                            getFormLoad();
                            tickTimeclock.Enabled = true;
                            cpbStatus.Text = "Running";

                            if (Directory.Exists(strProcessedPath) && Directory.Exists(strUnProcessedPath) && Directory.Exists(strErrorsPath))
                            {
                                folderCounter();
                            }
                            consoleApplication();
                            getFormLoad();
                        }
                        delay = 5;
                    }
                    else
                    {
                        delay = 5;
                        cpbStatus.Text = "No Connection";
                        isConnected = false;
                        CheckForInternetConnection();
                    }
                }
                catch (Exception ex)
                {
                    delay = 5;
                    isConnected = false;
                    CheckForInternetConnection();
                }
            }
            else
            {
                delay--;
                getFormLoad();
            }
        }
        
    }
}
