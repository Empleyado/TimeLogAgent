namespace Timelog.Agent2
{
    partial class PasswordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtConfirmPass = new System.Windows.Forms.TextBox();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.Location = new System.Drawing.Point(271, 88);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(69, 30);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Log in";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // txtPass
            // 
            this.txtPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtPass.Location = new System.Drawing.Point(12, 47);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(331, 23);
            this.txtPass.TabIndex = 1;
            this.txtPass.Text = "password";
            this.txtPass.Click += new System.EventHandler(this.txtPass_Click);
            this.txtPass.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtPass_MouseClick);
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            this.txtPass.Leave += new System.EventHandler(this.txtPass_Leave);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(165, 88);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 4;
            this.button2.Text = "Change password";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(157)))), ((int)(((byte)(203)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-8, -4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(369, 33);
            this.panel1.TabIndex = 3;
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtConfirmPass.Location = new System.Drawing.Point(12, 105);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.Size = new System.Drawing.Size(331, 23);
            this.txtConfirmPass.TabIndex = 3;
            this.txtConfirmPass.Text = "confirm password";
            this.txtConfirmPass.Visible = false;
            this.txtConfirmPass.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtConfirmPass_MouseClick);
            this.txtConfirmPass.TextChanged += new System.EventHandler(this.txtConfirmPass_TextChanged);
            this.txtConfirmPass.Leave += new System.EventHandler(this.txtConfirmPass_Leave);
            // 
            // txtNewPass
            // 
            this.txtNewPass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtNewPass.Location = new System.Drawing.Point(12, 76);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(331, 23);
            this.txtNewPass.TabIndex = 2;
            this.txtNewPass.Text = "new password";
            this.txtNewPass.Visible = false;
            this.txtNewPass.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtNewPass_MouseClick);
            this.txtNewPass.TextChanged += new System.EventHandler(this.txtNewPass_TextChanged);
            this.txtNewPass.Leave += new System.EventHandler(this.txtNewPass_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(318, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "close";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.label1_MouseLeave);
            this.label1.MouseHover += new System.EventHandler(this.label1_MouseHover);
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(352, 126);
            this.Controls.Add(this.txtNewPass);
            this.Controls.Add(this.txtConfirmPass);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PasswordForm";
            this.Text = "PasswordForm";
            this.Load += new System.EventHandler(this.PasswordForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtConfirmPass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.Label label1;
    }
}