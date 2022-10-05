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
using Timelog.Agent;
using Timelog.Agent2;
namespace Timelog.Agent2
{
    public partial class PasswordForm : Form
    {
        public string strPassword ;
        public string strPath;
        public string strNewPath, curdirectory;
        public bool isChangedPassword;
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void PasswordForm_Load(object sender, EventArgs e)
        {
            int x, y;
            x = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 15;
            y = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 200;
            this.Location = new Point(x, y);
            this.ShowInTaskbar = false;
            if (strPassword== null && strPath == null)
            {
                strPassword = "Password_711";
                strPath = @"C:\apex\datas";
             }
        }
        private void txtPass_Click(object sender, EventArgs e)
        {
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (isChangedPassword == true && txtPass.Text!= "password"&&txtNewPass.Text !="new password")
            {
                if(strPassword == txtPass.Text)
                {
                    if(txtNewPass.Text == txtConfirmPass.Text)
                    {
                        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                        curdirectory = Directory.GetCurrentDirectory();
                        string directory = curdirectory + @"\Settings.txt";
                        StreamWriter file = new StreamWriter(directory);
                        file.Write(strPath + "*" + txtNewPass.Text + "*");
                        MessageBox.Show("Password changed successfully");
                        file.Close();
                        AgentUI agentForm = new AgentUI();
                        agentForm.Show();
                        this.Close();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("The password and confirmation password do not match.");
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect password");
                }
            }
            isChangedPassword = true;
            txtConfirmPass.Visible = true;
            txtNewPass.Visible = true;
            this.Size = new Size(352, 177);
        }

        
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtPass.Text == strPassword)
            {
                FolderBrowserDialog openFileDialog = new FolderBrowserDialog();
                DialogResult result = openFileDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string files = openFileDialog.SelectedPath.ToString();
                    AgentUI uiForm = new AgentUI();
                    Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
                    curdirectory = Directory.GetCurrentDirectory();
                    string directory = curdirectory + @"\Settings.txt";
                    StreamWriter file = new StreamWriter(directory);
                    file.Write(files+"*"+strPassword+"*");
                    file.Close();
                    uiForm.Show();
                    this.Close();
                }
                
            }
            else
            {
                MessageBox.Show("Incorrect Password");
            }
        }
        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            if (txtConfirmPass.Text == "" || txtConfirmPass.Text == "confirm password")
            {
                txtConfirmPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtConfirmPass.UseSystemPasswordChar = true;
            }
        }
        private void txtConfirmPass_Leave(object sender, EventArgs e)
        {
            if (txtConfirmPass.Text == "")
            {
                txtConfirmPass.UseSystemPasswordChar = false;
                txtConfirmPass.Text = "confirm password";
            }
        }
        private void txtConfirmPass_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtConfirmPass.Text == "confirm password")
            {
                txtConfirmPass.Text = "";
                txtConfirmPass.UseSystemPasswordChar = true;
            }
        }
        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            if (txtNewPass.Text == "" || txtNewPass.Text == "new password")
            {
                txtNewPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtNewPass.UseSystemPasswordChar = true;
            }
        }
        private void txtNewPass_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtNewPass.Text == "new password")
            {
                txtNewPass.Text = "";
                txtNewPass.UseSystemPasswordChar = true;
            }
        }

        private void txtNewPass_Leave(object sender, EventArgs e)
        {
            if (txtNewPass.Text == "")
            {
                txtNewPass.UseSystemPasswordChar = false;
                txtNewPass.Text = "new password";
            }
        }
        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            if (txtPass.Text == "" || txtPass.Text == "password")
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
               
            }
        }
        private void txtPass_MouseClick(object sender, MouseEventArgs e)
        {
            if(txtPass.Text == "password")
            {
                txtPass.Text = "";
                txtPass.UseSystemPasswordChar = true;
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AgentUI agentForm = new AgentUI();
            agentForm.Show();
            this.Close();
            this.Dispose();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            label1.Cursor = Cursors.Hand;
            
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Underline);
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.Font = new Font(label1.Font.Name, label1.Font.SizeInPoints, FontStyle.Regular);
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.UseSystemPasswordChar = false;
                txtPass.Text = "password";
            }
        }

    }
}
